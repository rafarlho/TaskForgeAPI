using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskForge.Api.DTOs;
using TaskForge.Infrastructure.Data;

namespace TaskForge.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrganizationsController : ControllerBase
{
    private readonly TaskForgeDbContext _context;
    private readonly ILogger<OrganizationsController> _logger;

    public OrganizationsController(
        TaskForgeDbContext context,
        ILogger<OrganizationsController> logger
    )
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrganizationResponseDto>>> GetAll()
    {
        var organizations = await _context.Organizaions.ToListAsync();
        return Ok(organizations);
    }
}