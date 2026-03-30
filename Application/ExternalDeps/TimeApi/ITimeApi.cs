namespace Application.ExternalDeps.TimeApi;

public interface ITimeApi
{
    Task<TimeGetAllProjectsResponse> GetAllProjectsAsync();

    Task<TimeGetEmployeesTrackedTaskHoursResponse> GetEmployeesTrackedTaskHoursAsync(long projectId, int month, int year);
}
