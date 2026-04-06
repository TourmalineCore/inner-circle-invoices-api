using Application.ExternalDeps.EmployeesApi;
using Microsoft.Extensions.Options;

namespace Api.ExternalDeps.EmployeesApi;

public class EmployeesApi : IEmployeesApi
{
    private readonly ExternalDepsUrls _externalDepsUrls;

    private readonly ExternalApiHttpClient _externalApiHttpClient;

    public EmployeesApi(
        IOptions<ExternalDepsUrls> externalDepsUrls,
        ExternalApiHttpClient externalApiHttpClient
    )
    {
        _externalDepsUrls = externalDepsUrls.Value;
        _externalApiHttpClient = externalApiHttpClient;
    }

    public async Task<EmployeesResponse> GetAllEmployeesAsync()
    {
        var link = $"{_externalDepsUrls.EmployeesApiRootUrl}/internal/get-employees";

        var employeesDtos = await _externalApiHttpClient.GetAsync<List<EmployeeDto>>(link);

        return new EmployeesResponse
        {
            Employees = employeesDtos!
        };
    }
}
