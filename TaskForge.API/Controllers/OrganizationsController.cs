using Microsoft.AspNetCore.Mvc;
using TaskForge.Api.DTOs;
using TaskForge.Domain.Interfaces.Services;

namespace TaskForge.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrganizationsController : ControllerBase
{
    private readonly IOrganizationService _service;
    private readonly ILogger<OrganizationsController> _logger;

    public OrganizationsController(
        IOrganizationService service,
        ILogger<OrganizationsController> logger
    )
    {
        _service = service;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrganizationResponseDto>>> GetAll()
    {
        try
        {
            var organizations = await _service.GetAllAsync();
            return Ok(organizations);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving organizations");
            return StatusCode(500, new { message = "An error occurred while retrieving organizations" });
        }
    }
}