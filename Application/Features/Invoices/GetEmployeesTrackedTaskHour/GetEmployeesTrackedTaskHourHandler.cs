using Application.ExternalDeps.EmployeesApi;
using Application.ExternalDeps.TimeApi;

namespace Application.Features.Invoices.GetEmployeesTrackedTaskHour;

public class GetEmployeesTrackedTaskHoursHandler
{
    private readonly IEmployeesApi _employeesApi;
    private readonly ITimeApi _timeApi;

    public GetEmployeesTrackedTaskHoursHandler(
        IEmployeesApi employeesApi,
        ITimeApi timeApi
    )
    {
        _employeesApi = employeesApi;
        _timeApi = timeApi;
    }

    public async Task<GetEmployeesTrackedTaskHoursResponse> HandleAsync(
        long projectId,
        string month,
        string year
    )
    {
        var timeEmployeesEntries = await _timeApi.GetAllEmployeesEntries(projectId, month, year);

        var employeesList = await _employeesApi.GetAllEmployeesAsync();

        var employeesTrackedTaskHoursResponse = new GetEmployeesTrackedTaskHoursResponse
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

        return employeesTrackedTaskHoursResponse;
    }
}
