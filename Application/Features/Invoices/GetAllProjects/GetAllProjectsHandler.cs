using Application.ExternalDeps.TimeApi;

namespace Application.Features.Invoices.GetAllProjects;

public class GetAllProjectsHandler
{
    private readonly ITimeApi _timeApi;

    public GetAllProjectsHandler(
        ITimeApi timeApi
    )
    {
        _timeApi = timeApi;
    }

    public async Task<GetAllProjectsResponse> HandleAsync()
    {
        var projectsResponse = await _timeApi.GetAllProjectsAsync();

        return new GetAllProjectsResponse
        {
            Projects = projectsResponse.Projects
        };
    }
}
