using Application.ExternalDeps.EmployeesApi;
using Microsoft.Extensions.Options;

namespace Api.ExternalDeps.EmployeesApi;

public class EmployeesApi : IEmployeesApi
{
    private readonly ExternalDepsUrls _externalDepsUrls;
    private readonly AuthenticatedHttpClient _authenticatedHttpClient;

    public EmployeesApi(
        IOptions<ExternalDepsUrls> externalDepsUrls,
        AuthenticatedHttpClient authenticatedHttpClient
    )
    {
        _externalDepsUrls = externalDepsUrls.Value;
        _authenticatedHttpClient = authenticatedHttpClient;
    }

    public async Task<EmployeesResponse> GetAllEmployeesAsync()
    {
        var link = $"{_externalDepsUrls.EmployeesApiRootUrl}/internal/get-employees";

        var employeesDtos = await _authenticatedHttpClient.GetAsync<List<EmployeeDto>>(link);

        return new EmployeesResponse
        {
            Employees = employeesDtos!
        };
    }
}
