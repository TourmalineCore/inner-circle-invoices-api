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

        var employeesEntriesByProjectAndPeriodResponse = new GetEmployeesEntriesByProjectAndPeriodResponse
        {
            EmployeesTrackedTaskHours = timeEmployeesEntries.EmployeesTrackedTaskHours
            .Select(
                x => new EmployeesTrackedTaskHoursDto
                {
                    EmployeeId = x.EmployeeId,
                    Name = EmployeeMapper.MapToEmployeeDto(x.EmployeeId, employeesList)!.FullName,
                    TrackedHours = x.TrackedHours
                }
                ).ToList()

        };

        return employeesEntriesByProjectAndPeriodResponse;
    }
}
