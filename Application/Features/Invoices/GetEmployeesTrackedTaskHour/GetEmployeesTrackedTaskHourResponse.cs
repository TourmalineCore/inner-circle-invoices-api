namespace Application.Features.Invoices.GetEmployeesTrackedTaskHour;

public class GetEmployeesTrackedTaskHoursResponse
{
    public required List<EmployeesTrackedTaskHoursDto> EmployeesTrackedTaskHours { get; set; }
}

public class EmployeesTrackedTaskHoursDto
{
    public required long EmployeeId { get; set; }

    public required string FullName { get; set; }

    public required double TrackedHours { get; set; }
}
