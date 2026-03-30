using System.ComponentModel.DataAnnotations;
using Application.ExternalDeps.TimeApi;
using Application.Features.Invoices.GetAllProjects;
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
    public Task<GetAllProjectsResponse> GetAllProjectsAsync(
        [FromServices] GetAllProjectsHandler getAllProjectsHandler
    )
    {
        return getAllProjectsHandler.HandleAsync();
    }

    [EndpointSummary("Get employee projects by period")]
    [RequiresPermission(UserClaimsProvider.CanViewInvoices)]
    [HttpGet("employees-entries-by-project-and-period")]
    public Task<GetEmployeesEntriesByProjectAndPeriodResponse> GetAllEmployeesEntriesByProjectAndPeriod(
        [Required][FromQuery] long projectId,
        [Required][FromQuery] string month,
        [Required][FromQuery] string year,
        [FromServices] GetEmployeesEntriesByProjectAndPeriodHandler getEmployeesEntriesByProjectAndPeriodHandler
    )
    {
        return getEmployeesEntriesByProjectAndPeriodHandler.HandleAsync(projectId, month, year);
    }
}
