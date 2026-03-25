using System.ComponentModel.DataAnnotations;
using Application.ExternalDeps.TimeApi;
using Application.Features.Invoices.GetEmployeesEntriesByProjectAndPeriod;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TourmalineCore.AspNetCore.JwtAuthentication.Core.Filters;

namespace Api.Features.Invoices;

[Authorize]
[ApiController]
[Route("api/invoices")]
public class InvoicesController : ControllerBase
{
    [EndpointSummary("Get all projects")]
    [RequiresPermission(UserClaimsProvider.CanViewInvoices)]
    [HttpGet("projects")]
    public async Task<TimeGetProjectsResponse> GetAllProjectsAsync(
        [FromServices] ITimeApi timeApi
    )
    {
        var projectsResponse = await timeApi.GetAllProjects();
        return new TimeGetProjectsResponse
        {
            Projects = projectsResponse.Projects
        };
    }

    [EndpointSummary("Get employee projects by period")]
    [RequiresPermission(UserClaimsProvider.CanViewInvoices)]
    [HttpGet("employees-entries-by-project-and-period")]
    public async Task<GetEmployeesEntriesByProjectAndPeriodResponse> GetAllEmployeesEntriesByProjectAndPeriod(
        [Required][FromQuery] long projectId,
        [Required][FromQuery] string month,
        [Required][FromQuery] string year,
        [FromServices] GetEmployeesEntriesByProjectAndPeriodHandler getEmployeesEntriesByProjectAndPeriodHandler
    )
    {
        return await getEmployeesEntriesByProjectAndPeriodHandler.HandleAsync(projectId, month, year);
    }
}
