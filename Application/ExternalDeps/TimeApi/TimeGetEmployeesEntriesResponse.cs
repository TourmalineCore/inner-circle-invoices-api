namespace Application.ExternalDeps.TimeApi;

public class TimeGetEmployeesEntriesResponse
{
    public required List<TimeEmployeesEntriesDto> EmployeesTrackedTaskHours { get; set; }
}

public class TimeEmployeesEntriesDto
{
    public required long EmployeeId { get; set; }
    public required double TrackedHours { get; set; }
}
