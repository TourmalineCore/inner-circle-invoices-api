using Application.ExternalDeps.TimeApi;

namespace Application.Features.Invoices.GetEmployeesEntriesByProjectAndPeriod;

public class GetEmployeesEntriesByProjectAndPeriodQuery
{
    private readonly ITimeApi _timeApi;

    public GetEmployeesEntriesByProjectAndPeriodQuery(
        ITimeApi timeApi,
        IClaimsProvider claimsProvider
    )
    {
        _timeApi = timeApi;
    }

    public async Task<TimeGetEmployeesEntriesResponse> GetEmployeesEntriesByProjectAndPeriodAsync<TEntity>(
        long projectId,
        string month,
        string year
    )
    {
        var employeesEntries = await _timeApi.GetAllEmployeesEntries(projectId, month, year);

        return employeesEntries;
    }
}
