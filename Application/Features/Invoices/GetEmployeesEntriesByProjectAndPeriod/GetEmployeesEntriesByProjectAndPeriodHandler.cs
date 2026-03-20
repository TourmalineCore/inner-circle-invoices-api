using Application.ExternalDeps.EmployeesApi;
using Application.ExternalDeps.TimeApi;

namespace Application.Features.Invoices.GetEmployeesEntriesByProjectAndPeriod;

public class GetEmployeesEntriesByProjectAndPeriodHandler
{
    private readonly GetEmployeesEntriesByProjectAndPeriodQuery _getEmployeesEntriesByProjectAndPeriodQuery;
    private readonly IEmployeesApi _employeesApi;

    public GetEmployeesEntriesByProjectAndPeriodHandler(
        IEmployeesApi employeesApi,
        GetEmployeesEntriesByProjectAndPeriodQuery getEmployeesEntriesByProjectAndPeriodQuery
    )
    {
        _employeesApi = employeesApi;
        _getEmployeesEntriesByProjectAndPeriodQuery = getEmployeesEntriesByProjectAndPeriodQuery;
    }

    public async Task<GetEmployeesEntriesByProjectAndPeriodResponse> HandleAsync(
        long projectId,
        string month,
        string year
    )
    {
        var timeEmployeesEntries = await _getEmployeesEntriesByProjectAndPeriodQuery.GetEmployeesEntriesByProjectAndPeriodAsync<TimeGetEmployeesEntriesResponse>(projectId, month, year);

        var employeesList = await _employeesApi.GetAllEmployeesAsync();

        var employeesById = employeesList.Employees.ToDictionary(x => x.Id, x=> x.FullName);

        var employeesEntriesByProjectAndPeriodResponse = new GetEmployeesEntriesByProjectAndPeriodResponse
        {
            EmployeesEntries = timeEmployeesEntries.EmployeesEntries
            .Select(
                x => new EmployeesEntriesDto
                {
                    Id = x.employeeId,
                    Name = employeesById.Single(c => c.Key == x.employeeId).Value,
                    TrackedHours = (x.endTime - x.startTime).TotalHours
                }
                )
            .GroupBy(x => x.Id)
            .Select(v => new EmployeesEntriesDto 
            {
                Id = v.Key,
                Name = v.First().Name,
                TrackedHours = v.Sum(x => x.TrackedHours)
            }
            ).ToList()

        };

        return employeesEntriesByProjectAndPeriodResponse;
    }
}
