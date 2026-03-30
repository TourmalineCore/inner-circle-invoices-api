using System.ComponentModel.DataAnnotations;
using Application.Features.Invoices.GetAllProjects;
using Application.Features.Invoices.GetEmployeesTrackedTaskHour;
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

    [EndpointSummary("Get employees tracked task hours")]
    [RequiresPermission(UserClaimsProvider.CanViewInvoices)]
    [HttpGet("employees-tracked-task-hours")]
    public Task<GetEmployeesTrackedTaskHoursResponse> GetEmployeesTrackedTaskHoursAsync(
        [Required][FromQuery] long projectId,
        [Required][FromQuery] string month,
        [Required][FromQuery] string year,
        [FromServices] GetEmployeesTrackedTaskHoursHandler getEmployeesTrackedTaskHoursHandler
    )
    {
        return getEmployeesTrackedTaskHoursHandler.HandleAsync(projectId, month, year);
    }
}
