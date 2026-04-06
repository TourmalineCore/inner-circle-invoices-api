using Application.ExternalDeps.TimeApi;
using Microsoft.Extensions.Options;

namespace Api.ExternalDeps.TimeApi;

public class TimeApi : ITimeApi
{
    private readonly ExternalDepsUrls _externalDepsUrls;

    private readonly ExternalApiHttpClient _externalApiHttpClient;

    public TimeApi(
        IOptions<ExternalDepsUrls> externalDepsUrls,
        ExternalApiHttpClient externalApiHttpClient
    )
    {
        _externalDepsUrls = externalDepsUrls.Value;
        _externalApiHttpClient = externalApiHttpClient;
    }

    public async Task<TimeGetAllProjectsResponse> GetAllProjectsAsync()
    {
        var link = $"{_externalDepsUrls.TimeApiRootUrl}/internal/projects";

        var projects = await _externalApiHttpClient.GetAsync<TimeGetAllProjectsResponse>(link);

        return projects!;
    }

    public async Task<TimeGetEmployeesTrackedTaskHoursResponse> GetEmployeesTrackedTaskHoursAsync(
        long projectId,
        int year,
        int month
    )
    {
        var startDate = DateOnly.Parse($"{year}-{month}-01");

        var lastDayOfMonth = DateTime.DaysInMonth(year, month);

        var endDate = DateOnly.Parse($"{year}-{month}-{lastDayOfMonth}");

        var formattedStartDate = startDate.ToString("yyyy-MM-dd");

        var formattedEndDate = endDate.ToString("yyyy-MM-dd");

        var link = $"{_externalDepsUrls.TimeApiRootUrl}/internal/projects/tracked-task-hours?projectId={projectId}&startDate={formattedStartDate}&endDate={formattedEndDate}";

        var employeesTrackedTaskHoursEntries = await _externalApiHttpClient.GetAsync<TimeGetEmployeesTrackedTaskHoursResponse>(link);

        return employeesTrackedTaskHoursEntries!;
    }
}
