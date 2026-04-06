using CleanTeeth.API.Jobs;
using CleanTeeth.API.Middlewares;
using CleanTeeth.Application;
using CleanTeeth.Infrastructure;
using CleanTeeth.Persistence;
using CleanTeeth.Security;
using CleanTeeth.Security.Models;
using Microsoft.AspNetCore.Mvc.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add(new AuthorizeFilter("isAdmin"));
});
builder.Services.AddPersistenceServices();
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddSecurityServices();

builder.Services.AddHostedService<AppointmentsReminderJob>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapIdentityApi<User>();

// 自定义控制器错误处理中间件
app.UseCustomExceptionHandler();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();