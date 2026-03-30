namespace Application.ExternalDeps.TimeApi;

public interface ITimeApi
{
    Task<TimeGetAllProjectsResponse> GetAllProjects();

    Task<TimeGetEmployeesTrackedTaskHoursResponse> GetEmployeesTrackedTaskHours(long projectId, string month, string year);
}
