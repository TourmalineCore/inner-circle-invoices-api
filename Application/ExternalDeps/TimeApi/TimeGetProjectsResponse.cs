namespace Application.ExternalDeps.TimeApi;

public class TimeGetProjectsResponse
{
  public required List<TimeProjectsDto> Projects { get; set; }

  public class TimeProjectsDto
  {
    public long id { get; set; }

    public required string name { get; set; }
  }
}
