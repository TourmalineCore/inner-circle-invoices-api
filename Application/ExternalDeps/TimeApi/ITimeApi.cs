namespace Application.ExternalDeps.TimeApi;

public interface ITimeApi
{
    Task<TimeGetProjectsResponse> GetAllProjects();

    Task<TimeGetEmployeesTrackedTaskHoursResponse> GetEmployeesTrackedTaskHours(long projectId, string month, string year);
}
