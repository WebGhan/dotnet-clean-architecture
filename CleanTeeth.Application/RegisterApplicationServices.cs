using CleanTeeth.Application.Features.Dentists.Commands.CreateDentist;
using CleanTeeth.Application.Utilities;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace CleanTeeth.Application;

public static class RegisterApplicationServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddTransient<IMediator, SimpleMediator>();

        // 注册 FluentValidation 验证服务
        services.AddValidatorsFromAssemblyContaining<CreateDentistCommandValidator>();

        services.Scan(scan => scan.FromAssembliesOf(typeof(RegisterApplicationServices))
            .AddClasses(c => c.AssignableTo(typeof(IRequestHandler<>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime()
            .AddClasses(c => c.AssignableTo(typeof(IRequestHandler<,>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        // services.AddScoped<IRequestHandler<CreateDentalOfficeCommand, Guid>, CreateDentalOfficeCommandHandler>();
        // services
        //     .AddScoped<IRequestHandler<GetDentalOfficeDetailQuery, DentalOfficeDetailDto>,
        //         GetDentalOfficeDetailQueryHandler>();
        //
        // services
        //     .AddScoped<IRequestHandler<GetDentalOfficesListQuery, List<DentalOfficesListDto>>,
        //         GetDentalOfficesListQueryHandler>();
        //
        // services.AddScoped<IRequestHandler<DeleteDentalOfficeCommand>, DeleteDentalOfficeCommandHandler>();

        return services;
    }
}