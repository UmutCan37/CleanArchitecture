using CleanArchitecture.Application.Behaviors;
using CleanArchitecture.Application.Service;
using CleanArchitecture.Domain.Repositories;
using CleanArchitecture.Persistence;
using CleanArchitecture.Persistence.Context;
using CleanArchitecture.Persistence.Repositories;
using CleanArchitecture.Persistence.Services;
using CleanArchitecture.WebApi.Middleware;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ICarService, CarService>();
builder.Services.AddTransient<ExceptionMiddleware>();

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ICarRepository, CarRepository>();
builder.Services.AddAutoMapper(typeof(CleanArchitecture.Persistence.AssemblyReference).Assembly);
builder.Services.AddDbContext<AppDbContext>(
    opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"))

    );
builder.Services.AddControllers().AddApplicationPart(typeof(CleanArchitecture.Presentation.AssemblyReference).Assembly);
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CleanArchitecture.Application.AssemblyReference).Assembly));

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
builder.Services.AddValidatorsFromAssembly(typeof(CleanArchitecture.Application.AssemblyReference).Assembly);
builder.Services.AddSwaggerGen();
var app = builder.Build();
app.UseMiddlewareExtensions();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
