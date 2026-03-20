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

    public async Task<TimeGetProjectsResponse> GetAllProjects()
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
        var projects = await httpClient.GetFromJsonAsync<TimeGetProjectsResponse>(link);

        return projects!;
    }

    public async Task<TimeGetEmployeesEntriesResponse> GetAllEmployeesEntries(long projectId, string month, string year)
    {

        var startDate = DateTime.Parse($"{year}-{month}-01T00:00:00");

        var endDate = startDate.AddMonths(1);

        var formattedStartDate = startDate.ToString("yyyy-MM-ddTHH:mm:ss");

        var formattedEndDate = endDate.ToString("yyyy-MM-ddTHH:mm:ss");

        var link = $"{_externalDepsUrls.TimeApiRootUrl}/internal/employees-entries-by-project?projectId={projectId}&startDate={formattedStartDate}&endDate={formattedEndDate}";

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

        var employeesEntries = await httpClient.GetFromJsonAsync<TimeGetEmployeesEntriesResponse>(link);

        return employeesEntries!;
    }

}
