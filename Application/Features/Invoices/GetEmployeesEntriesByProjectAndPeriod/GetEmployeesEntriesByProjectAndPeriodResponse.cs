namespace Application.Features.Invoices.GetEmployeesEntriesByProjectAndPeriod;

public class GetEmployeesEntriesByProjectAndPeriodResponse
{
    public required List<EmployeesTrackedTaskHoursDto> EmployeesTrackedTaskHours { get; set; }
}

public class EmployeesTrackedTaskHoursDto
{
    public required long EmployeeId { get; set; }

    public required string Name { get; set; }

    public required double TrackedHours { get; set; }
}
