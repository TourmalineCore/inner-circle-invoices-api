namespace Application.ExternalDeps.TimeApi;

public interface ITimeApi
{
    Task<TimeGetProjectsResponse> GetAllProjects();

    Task<TimeGetEmployeesEntriesResponse> GetAllEmployeesEntries(long projectId, string month, string year);
}