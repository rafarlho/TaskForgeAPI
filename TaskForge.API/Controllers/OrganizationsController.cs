using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TaskForge.Api.DTOs;
using TaskForge.Domain.Entities;
using TaskForge.Domain.Interfaces.Services;

namespace TaskForge.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrganizationsController : ControllerBase
{
    private readonly IOrganizationService _service;
    private readonly ILogger<OrganizationsController> _logger;
    private readonly IMapper _mapper;
    public OrganizationsController(
        IOrganizationService service,
        ILogger<OrganizationsController> logger,
        IMapper mapper
    )
    {
        _service = service;
        _logger = logger;
        _mapper = mapper;   
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrganizationResponseDto>>> GetAll()
    {
        try
        {
            var entities = await _service.GetAllAsync();
            var response = _mapper.Map<IEnumerable<OrganizationResponseDto>>(entities);
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving organizations");
            return StatusCode(500, new { message = "An error occurred while retrieving organizations" });
        }
    }

    [HttpPost]
    public async Task<ActionResult<OrganizationResponseDto>> Create(CreateOrganizationDto dto)
    {
        try
        {
            var entity = _mapper.Map<Organization>(dto);
            var createdEntity = await _service.AddAsync(entity);
            var newEntity = _mapper.Map<OrganizationResponseDto>(createdEntity);
            return Ok(newEntity);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creation organization");
            return StatusCode(500, new { message = "An error occurred while creating organization " + dto.Name });
        }
    }
}