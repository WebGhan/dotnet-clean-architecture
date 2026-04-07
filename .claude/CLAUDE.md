# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

CleanTeeth 是一个基于 .NET 8 的牙科预约管理系统，采用整洁架构（Clean Architecture）和领域驱动设计（DDD）。解决方案使用 PostgreSQL 数据库和 Entity Framework Core，实现了自定义 Mediator 模式的 CQRS，并集成了 ASP.NET Core Identity 进行身份验证。

## Architecture

解决方案遵循整洁架构，具有清晰的依赖方向：

1. **Domain Layer** (`CleanTeeth.Domain`) - 核心业务实体、值对象、领域异常和枚举
   - `Entities/` - 领域实体（`Appointment`, `Patient`, `Dentist`, `DentalOffice`）
   - `ValueObjects/` - 值对象，如 `Email`, `TimeInterval`
   - `Common/` - 基类，如用于审计跟踪的 `Auditable`
   - `Enums/` - 领域枚举，如 `AppointmentStatus`
   - `Exceptions/` - 领域异常，如 `BusinessRuleException`

2. **Application Layer** (`CleanTeeth.Application`) - 用例、契约和应用服务
   - `Features/` - 按领域组织的 CQRS 实现（Patients, Appointments, Dentists, DentalOffices）
     - `Commands/` - 命令处理器和验证器
     - `Queries/` - 查询处理器和 DTOs
   - `Contracts/` - 仓储、安全和持久化的接口
   - `Utilities/` - 自定义 Mediator 实现（`IMediator`, `SimpleMediator`）和分页工具（`PagedResult<T>`, `PagedFilterDto`）
   - `Exceptions/` - 应用层异常

3. **Infrastructure Layer** (`CleanTeeth.Infrastructure`) - 外部服务实现
   - `Notifications/` - 邮件通知服务

4. **Persistence Layer** (`CleanTeeth.Persistence`) - 数据访问实现
   - `Repositories/` - 各实体的仓储实现
   - `Configurations/` - EF Core 实体配置
   - `UnitsOfWork/` - Unit of Work 模式实现
   - `Migrations/` - 数据库迁移

5. **Security Layer** (`CleanTeeth.Security`) - 身份验证和授权
   - `Services/` - 用户服务实现
   - `Models/` - 身份模型（`User`）
   - `Migrations/` - 身份数据库迁移

6. **Presentation Layer** (`API/CleanTeeth.API`) - ASP.NET Core Web API
   - `Controllers/` - 按实体组织的 API 端点
   - `Dtos/` - 请求/响应 DTOs
   - `Middlewares/` - 自定义中间件，如 `ErrorHandlingMiddleware`
   - `Jobs/` - 后台作业（如 `AppointmentsReminderJob`）

7. **Test Layer** (`CleanTeeth.Test`) - 单元测试和集成测试

## Key Design Patterns

- **CQRS**: 命令和查询在 `Features/` 目录中分离
- **Repository Pattern**: 通用 `IRepository<T>` 接口和实体特定的仓储实现
- **Pagination Pattern**: 统一的分页实现，包含 `PagedFilterDto` 基类、`PagedResult<T>` 响应容器和仓储中的 `GetFiltered()`/`GetFilteredCount()` 方法
- **Unit of Work**: `IUnitOfWork` 用于事务管理
- **Mediator Pattern**: 自定义 `SimpleMediator` 用于解耦请求和处理器
- **Domain Events**: 实体可以触发领域事件（部分实现）
- **Value Objects**: 不可变对象，如带有验证逻辑的 `Email`
- **Audit Trail**: `Auditable` 基类跟踪创建/修改元数据

## Development Commands

### Build and Run
```bash
# 恢复依赖
dotnet restore CleanTeeth.sln

# 构建解决方案
dotnet build CleanTeeth.sln

# 运行 API（从 API/CleanTeeth.API 目录）
dotnet run --project API/CleanTeeth.API

# 使用特定环境运行
dotnet run --project API/CleanTeeth.API --environment Development
```

### Database Operations
```bash
# 安装 EF Core 工具（如果尚未安装）
dotnet tool install --global dotnet-ef

# 创建迁移（从 CleanTeeth.Persistence 目录）
dotnet ef migrations add MigrationName --startup-project ../API/CleanTeeth.API

# 更新数据库
dotnet ef database update --startup-project ../API/CleanTeeth.API

# 应用安全数据库的迁移（从 CleanTeeth.Security 目录）
dotnet ef database update --startup-project ../API/CleanTeeth.API
```

### Testing
```bash
# 运行所有测试
dotnet test CleanTeeth.sln

# 运行特定测试项目
dotnet test CleanTeeth.Test/CleanTeeth.Test.csproj

# 运行测试并收集覆盖率（需要 coverlet）
dotnet test --collect:"XPlat Code Coverage"
```

### Development Workflow
1. **数据库设置**: 确保 PostgreSQL 在 `localhost:54322` 上运行（如 `appsettings.Development.json` 中配置）
2. **应用迁移**: 为主数据库和安全数据库运行 EF Core 迁移
3. **运行 API**: 从 API 项目启动 API：`dotnet run`
4. **API 文档**: 通过 `https://localhost:5001/swagger`（或配置的端口）访问 Swagger UI
5. **身份端点**: 身份 API 端点通过 `app.MapIdentityApi<User>()` 自动注册

## Configuration

### Connection Strings
- **主数据库**: 在 `appsettings.Development.json` 中配置为 `CleanTeethConnectionString`
- **安全数据库**: 使用相同的连接字符串但独立的 `CleanTeethSecurityDbContext`
- **邮件**: SMTP 配置在 `appsettings.json` 的 `EMAIL_CONFIGURATIONS` 下

### Dependency Injection
各层的服务通过扩展方法注册：
- `AddApplicationServices()` - 注册 Mediator、验证器和请求处理器
- `AddPersistenceServices()` - 注册 DbContext、仓储和 Unit of Work
- `AddInfrastructureServices()` - 注册通知服务
- `AddSecurityServices()` - 注册 Identity 服务和身份验证

## Code Organization Conventions

1. **功能文件夹**: 每个领域功能（Patients, Appointments 等）在 `Features/` 下有独立的文件夹
2. **命令/查询分离**: 每个功能包含 `Commands/` 和 `Queries/` 子目录
3. **处理器命名**: 命令/查询处理器遵循 `{CommandName}CommandHandler` 模式
4. **DTO 位置**: 应用层 DTOs 在功能文件夹中，API DTOs 在 `API/CleanTeeth.API/Dtos/` 中
5. **验证**: 命令验证器与命令在同一文件夹中，使用 FluentValidation
6. **分页实现**:
   - 分页参数: 所有分页查询的 Filter DTO 继承自 `PagedFilterDto` (`CleanTeeth.Application/Utilities/Common/`)
   - 分页响应: 列表查询返回 `PagedResult<T>`，包含分页元数据（总记录数、当前页码、每页大小）
   - 仓储方法: 实体仓储实现 `GetFiltered()`（分页数据）和 `GetFilteredCount()`（过滤后总数）方法

## Important Notes

1. **自定义 Mediator**: 使用 `SimpleMediator` 而非 MediatR 库
2. **审计字段**: 所有继承自 `Auditable` 的实体自动填充审计字段
3. **错误处理**: 自定义 `ErrorHandlingMiddleware` 处理领域和验证异常
4. **后台作业**: `AppointmentsReminderJob` 作为托管服务运行
5. **分页系统**: 统一的分页实现支持所有实体（Patients, Dentists, Appointments, DentalOffices）
   - **分页参数**: `PagedFilterDto` 基类提供 `Page` (默认1) 和 `PageSize` (默认10) 属性
   - **分页响应**: `PagedResult<T>` 包含 `Items` (当前页数据)、`TotalCount` (过滤后总数)、`Page` (当前页码)、`PageSize` (每页大小)
   - **仓储实现**: 每个实体仓储提供 `GetFiltered()` (应用过滤和分页) 和 `GetFilteredCount()` (应用过滤计算总数) 方法
   - **一致性**: 所有列表查询端点支持 `?Page=1&PageSize=10&Name=...` 等过滤和分页参数

## Testing Strategy

- **单元测试**: 专注于领域实体和值对象
- **集成测试**: 测试仓储实现和数据库交互
- **测试数据**: 使用 NSubstitute 模拟依赖
- **测试结构**: 在测试项目中按 `Application/` 和 `Domain/` 目录组织

## Security

- **身份验证**: 使用承载令牌的 ASP.NET Core Identity
- **授权**: 基于策略，需要 `isAdmin` 声明的 "isAdmin" 策略
- **用户上下文**: `IUserService` 为审计跟踪提供当前用户信息
- **密码管理**: 内置 Identity 密码哈希和验证