
using DataLawyer.Application.Mappings;
using DataLawyer.Application.Processes.Commands;
using DataLawyer.Application.Users.Commands;
using DataLawyer.Application.Validators;
using DataLawyer.Domain.Interfaces;
using DataLawyer.Infrastructure.Context;
using DataLawyer.Infrastructure.CrawlerFunction;
using DataLawyer.Infrastructure.Repository;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataLawyer.CrossCutting;

public static class DependencyInjectionAPI
{
    public static IServiceCollection AddInfrastructureAPI(this IServiceCollection services, IConfiguration configuration)
    {
        var host = configuration["DBHOST"] ?? "localhost";
        var port = configuration["DBPORT"] ?? "3306";
        var password = configuration["DBPASSWORD"] ?? "D4taL4wy3r@";

        string mySqlConnection = $"server={host};userid=root;"
        + $"pwd={password};port={port};database=DataLawyerDB";

        services.AddDbContext<AppDbContext>(options =>
        options.UseMySql(mySqlConnection, ServerVersion.AutoDetect(mySqlConnection)));

        services.AddFluentValidationAutoValidation();
        services.AddTransient<IValidator<UserCreateCommand>, UserCreateCommandValidator>();
        services.AddTransient<IValidator<UserLoginCommand>, UserLoginCommandValidator>();
        services.AddTransient<IValidator<ProcessCreateCommand>, ProcessCreateCommandValidator>();
        services.AddTransient<IValidator<ProcessUpdateCommand>, ProcessUpdateCommandValidator>();

        services.AddAutoMapper(typeof(MappingProfile));

        services.AddScoped<IGeneralPersistence, GeneralPersistence>();
        services.AddScoped<IUser, UserPersistence>();
        services.AddScoped<IProcessCrawler, ProcessCrawler>();
        services.AddScoped<IProcess, ProcessPersistence>();
        services.AddScoped<IMovement, MovementPersistence>();
        services.AddScoped<IArea, AreaPersistence>();
        services.AddTransient<AppDbContext>();

        var myHandlers = AppDomain.CurrentDomain.Load("DataLawyer.Application");
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(myHandlers));

        return services;
    }
}
