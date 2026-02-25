using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TaskForge.Api.DTOs;
using TaskForge.Domain.Entities;
using TaskForge.Domain.Interfaces.Services;

namespace TaskForge.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskGroupController : ControllerBase
{
    private readonly ITaskGroupService _service;
    private readonly ILogger<TaskGroupController> _logger;
    private readonly IMapper _mapper;
    public TaskGroupController(
        ITaskGroupService service,
        ILogger<TaskGroupController> logger,
        IMapper mapper
    )
    {
        _service = service;
        _logger = logger;
        _mapper = mapper;   
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TaskGroupResponseDto>>> GetAll()
    {
        var entities = await _service.GetAllAsync();
        var response = _mapper.Map<IEnumerable<TaskGroupResponseDto>>(entities);
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TaskGroupResponseDto>> GetById(Guid Id)
    {
        var entities = await _service.GetByIdAsync(Id);
        var response = _mapper.Map<TaskGroupResponseDto>(entities);
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<TaskGroupResponseDto>> Create(CreateTaskGroupDto dto)
    {
        var entity = _mapper.Map<TaskGroup>(dto);
        var createdEntity = await _service.AddAsync(entity);
        var newEntity = _mapper.Map<TaskGroupResponseDto>(createdEntity);
        return Ok(newEntity);
    }

    [HttpPut]
    public async Task<ActionResult<TaskGroupResponseDto>> Update(UpdateTaskGroupDto dto)
    {
        var entity = _mapper.Map<TaskGroup>(dto);
        var createdEntity = await _service.UpdateAsync(entity);
        var newEntity = _mapper.Map<TaskGroupResponseDto>(createdEntity);
        return Ok(newEntity);
    }
}