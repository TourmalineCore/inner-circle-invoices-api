namespace Application.ExternalDeps.TimeApi;

public class TimeGetEmployeesTrackedTaskHoursResponse
{
    public required List<TimeEmployeesTrackedTaskHoursDto> EmployeesTrackedTaskHours { get; set; }
}

public class TimeEmployeesTrackedTaskHoursDto
{
    public required long EmployeeId { get; set; }

    public required double TrackedHours { get; set; }
}
