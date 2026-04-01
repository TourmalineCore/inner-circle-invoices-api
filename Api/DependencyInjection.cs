using Api.ExternalDeps.EmployeesApi;
using Api.ExternalDeps.TimeApi;
using Application;
using Application.ExternalDeps.EmployeesApi;
using Application.ExternalDeps.TimeApi;
using Application.Features.Invoices.GetAllProjects;
using Application.Features.Invoices.GetEmployeesTrackedTaskHour;

namespace Api;

public static class DependencyInjection
{

    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        // https://stackoverflow.com/a/37373557
        services.AddHttpContextAccessor();

        services.AddScoped<IClaimsProvider, HttpContextClaimsProvider>();

        services.Configure<ExternalDepsUrls>(configuration.GetSection(nameof(ExternalDepsUrls)));

        services.AddTransient<ITimeApi, TimeApi>();
        services.AddTransient<IEmployeesApi, EmployeesApi>();

        services.AddTransient<GetEmployeesTrackedTaskHoursHandler>();
        services.AddTransient<GetAllProjectsHandler>();
    }
}
