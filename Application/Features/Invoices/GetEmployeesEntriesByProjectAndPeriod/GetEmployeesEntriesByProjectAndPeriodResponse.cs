namespace Application.Features.Invoices.GetEmployeesEntriesByProjectAndPeriod;

public class GetEmployeesEntriesByProjectAndPeriodResponse
{
    public required List<EmployeesEntriesDto> EmployeesEntries { get; set; }

}

public class EmployeesEntriesDto
{
    public required long Id { get; set; }
    public required string Name { get; set; }
    public required double TrackedHours { get; set; }
}
