using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TaskForge.Api.DTOs;
using TaskForge.Domain.Entities;
using TaskForge.Domain.Interfaces.Services;

namespace TaskForge.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrganizationController : ControllerBase
{
    private readonly IOrganizationService _service;
    private readonly ILogger<OrganizationController> _logger;
    private readonly IMapper _mapper;
    public OrganizationController(
        IOrganizationService service,
        ILogger<OrganizationController> logger,
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
        var entities = await _service.GetAllAsync();
        var response = _mapper.Map<IEnumerable<OrganizationResponseDto>>(entities);
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<OrganizationResponseDto>> GetById(Guid Id)
    {
        var entities = await _service.GetByIdAsync(Id);
        var response = _mapper.Map<OrganizationResponseDto>(entities);
        return Ok(response);
    }

    [HttpGet("withTaskGroups/{id}")]
    public async Task<ActionResult<IEnumerable<OrganizationResponseDto>>> GetWithTaskGroups(Guid id)
    {
        var entities = await _service.GetWithTaskGroups(id);
        var response = _mapper.Map<OrganizationResponseDto>(entities);
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<OrganizationResponseDto>> Create(CreateOrganizationDto dto)
    {
        var entity = _mapper.Map<Organization>(dto);
        var createdEntity = await _service.AddAsync(entity);
        var newEntity = _mapper.Map<OrganizationResponseDto>(createdEntity);
        return Ok(newEntity);
    }

    [HttpPut]
    public async Task<ActionResult<OrganizationResponseDto>> Update(UpdateOrganizationDto dto)
    {
        var entity = _mapper.Map<Organization>(dto);
        var createdEntity = await _service.UpdateAsync(entity);
        var newEntity = _mapper.Map<OrganizationResponseDto>(createdEntity);
        return Ok(newEntity);
    }
}