using Microsoft.Extensions.Options;
using TourmalineCore.AspNetCore.JwtAuthentication.Core.Options;

public class AuthenticatedHttpClient
{
    private readonly AuthenticationOptions _authenticationOptions;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthenticatedHttpClient(
        IOptions<AuthenticationOptions> authenticationOptions,
        IHttpContextAccessor httpContextAccessor
    )
    {
        _authenticationOptions = authenticationOptions.Value;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<TValue?> GetAsync<TValue>(string link)
    {
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
        using var httpClient = new HttpClient();

        httpClient.DefaultRequestHeaders.Add(headerName, token);

        return await httpClient.GetFromJsonAsync<TValue>(link);
    }
}