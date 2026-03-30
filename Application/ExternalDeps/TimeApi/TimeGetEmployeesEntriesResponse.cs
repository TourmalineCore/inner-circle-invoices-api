namespace Application.ExternalDeps.TimeApi;

public class TimeGetEmployeesTrackedTaskHoursResponse
{
    public required List<TimeEmployeesTrackedTaskHourDto> EmployeesTrackedTaskHours { get; set; }
}

public class TimeEmployeesTrackedTaskHourDto
{
    public required long EmployeeId { get; set; }

    public required double TrackedHours { get; set; }
}
