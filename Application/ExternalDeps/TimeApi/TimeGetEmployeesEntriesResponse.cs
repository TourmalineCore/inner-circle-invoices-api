namespace Application.ExternalDeps.TimeApi;

public class TimeGetEmployeesEntriesResponse
{
  public required List<TimeEmployeesEntriesDto> EmployeesEntries { get; set; }

  public class TimeEmployeesEntriesDto
  {
    public long employeeId { get; set; }

    public required DateTime startTime { get; set; }
    public required DateTime endTime { get; set; }
  }
}
