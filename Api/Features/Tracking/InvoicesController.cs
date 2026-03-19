using Application.ExternalDeps.TimeApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TourmalineCore.AspNetCore.JwtAuthentication.Core.Filters;

namespace Api.Features.Tracking;

[Authorize]
[ApiController]
[Route("api/invoices")]
public class TrackingController : ControllerBase
{

    [EndpointSummary("Get all projects")]
    [RequiresPermission(UserClaimsProvider.CanViewInvoices)]
    [HttpGet("projects")]
    public async Task<TimeGetProjectsResponse> GetAllProjectsAsync(
        [FromServices] ITimeApi timeApi
    )
    {
        var projects = await timeApi.GetAllProjects();
        return new TimeGetProjectsResponse
        {
            Projects = projects.Projects
        };
    }
}
