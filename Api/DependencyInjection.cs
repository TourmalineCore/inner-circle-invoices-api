using Api.ExternalDeps.TimeApi;
using Application;
using Application.ExternalDeps.TimeApi;

namespace Api;

public static class DependencyInjection
{

    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        // https://stackoverflow.com/a/37373557
        services.AddHttpContextAccessor();

        services.AddScoped<IClaimsProvider, HttpContextClaimsProvider>();

        //services.AddTransient<CreateTaskEntryHandler>();

        services.Configure<ExternalDepsUrls>(configuration.GetSection(nameof(ExternalDepsUrls)));

        services.AddTransient<ITimeApi, TimeApi>();
    }
}
