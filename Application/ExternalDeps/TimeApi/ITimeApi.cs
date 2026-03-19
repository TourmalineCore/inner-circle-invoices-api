namespace Application.ExternalDeps.TimeApi;

public interface ITimeApi
{
  Task<TimeGetProjectsResponse> GetAllProjects();
}
