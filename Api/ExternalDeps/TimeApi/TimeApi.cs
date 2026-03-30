using Application.ExternalDeps.TimeApi;
using Microsoft.Extensions.Options;
using TourmalineCore.AspNetCore.JwtAuthentication.Core.Options;

namespace Api.ExternalDeps.TimeApi;

public class TimeApi : ITimeApi
{
    private readonly ExternalDepsUrls _externalDepsUrls;
    private readonly AuthenticationOptions _authenticationOptions;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public TimeApi(
        IOptions<ExternalDepsUrls> externalDepsUrls,
        IOptions<AuthenticationOptions> authenticationOptions,
        IHttpContextAccessor httpContextAccessor
    )
    {
        _externalDepsUrls = externalDepsUrls.Value;
        _authenticationOptions = authenticationOptions.Value;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<TimeGetAllProjectsResponse> GetAllProjectsAsync()
    {
        var link = $"{_externalDepsUrls.TimeApiRootUrl}/internal/projects";

        var headerName = _authenticationOptions.IsDebugTokenEnabled
          ? "X-DEBUG-TOKEN"
          : "Authorization";

        var token = _httpContextAccessor
          .HttpContext!
          .Request
          .Headers[headerName]
          .ToString();

        // ToDo improve work with HttpClient
        // https://learn.microsoft.com/en-us/dotnet/fundamentals/networking/http/httpclient-guidelines
        using var httpClient = new HttpClient()!;

        httpClient.DefaultRequestHeaders.Add(headerName, token);
        var projects = await httpClient.GetFromJsonAsync<TimeGetAllProjectsResponse>(link);

        return projects!;
    }

    public async Task<TimeGetEmployeesTrackedTaskHoursResponse> GetEmployeesTrackedTaskHoursAsync(long projectId, string month, string year)
    {
        var startDate = DateOnly.Parse($"{year}-{month}-01");

        var lastDayOfMonth = DateTime.DaysInMonth(int.Parse(year), int.Parse(month));

        var endDate = DateOnly.Parse($"{year}-{month}-{lastDayOfMonth}");

        var formattedStartDate = startDate.ToString("yyyy-MM-dd");

        var formattedEndDate = endDate.ToString("yyyy-MM-dd");

        var link = $"{_externalDepsUrls.TimeApiRootUrl}/internal/projects/tracked-task-hours?projectId={projectId}&startDate={formattedStartDate}&endDate={formattedEndDate}";

        var headerName = _authenticationOptions.IsDebugTokenEnabled
          ? "X-DEBUG-TOKEN"
          : "Authorization";

        var token = _httpContextAccessor
          .HttpContext!
          .Request
          .Headers[headerName]
          .ToString();

        // ToDo improve work with HttpClient
        // https://learn.microsoft.com/en-us/dotnet/fundamentals/networking/http/httpclient-guidelines
        using var httpClient = new HttpClient()!;

        httpClient.DefaultRequestHeaders.Add(headerName, token);

        var employeesTrackedTaskHoursEntries = await httpClient.GetFromJsonAsync<TimeGetEmployeesTrackedTaskHoursResponse>(link);

        return employeesTrackedTaskHoursEntries!;
    }
}
