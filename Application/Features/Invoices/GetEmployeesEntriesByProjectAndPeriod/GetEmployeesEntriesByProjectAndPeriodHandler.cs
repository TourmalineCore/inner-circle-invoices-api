using Application.ExternalDeps.EmployeesApi;
using Application.ExternalDeps.TimeApi;

namespace Application.Features.Invoices.GetEmployeesEntriesByProjectAndPeriod;

public class GetEmployeesEntriesByProjectAndPeriodHandler
{
    private readonly IEmployeesApi _employeesApi;
    private readonly ITimeApi _timeApi;

    public GetEmployeesEntriesByProjectAndPeriodHandler(
        IEmployeesApi employeesApi,
        ITimeApi timeApi
    )
    {
        _employeesApi = employeesApi;
        _timeApi = timeApi;
    }

    public async Task<GetEmployeesEntriesByProjectAndPeriodResponse> HandleAsync(
        long projectId,
        string month,
        string year
    )
    {
        var timeEmployeesEntries = await _timeApi.GetAllEmployeesEntries(projectId, month, year);

        var employeesList = await _employeesApi.GetAllEmployeesAsync();

        var employeesEntriesByProjectAndPeriodResponse = new GetEmployeesEntriesByProjectAndPeriodResponse
        {
            EmployeesTrackedTaskHours = timeEmployeesEntries.EmployeesTrackedTaskHours
                .Select(x => new EmployeesTrackedTaskHoursDto
                {
                    EmployeeId = x.EmployeeId,
                    Name = EmployeeMapper.MapToEmployeeDto(x.EmployeeId, employeesList)!.FullName,
                    TrackedHours = x.TrackedHours
                })
                .ToList()
        };

        return employeesEntriesByProjectAndPeriodResponse;
    }
}
