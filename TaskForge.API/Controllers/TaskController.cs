using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TaskForge.Api.DTOs;
using TaskForge.Domain.Entities;
using TaskForge.Domain.Interfaces.Services;
using Task = TaskForge.Domain.Entities.Task;

namespace TaskForge.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskController : ControllerBase
{
    private readonly ITaskService _service;
    private readonly ILogger<TaskController> _logger;
    private readonly IMapper _mapper;
    public TaskController(
        ITaskService service,
        ILogger<TaskController> logger,
        IMapper mapper
    )
    {
        _service = service;
        _logger = logger;
        _mapper = mapper;   
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TaskResponseDto>>> GetAll()
    {
        var entities = await _service.GetAllAsync();
        var response = _mapper.Map<IEnumerable<TaskResponseDto>>(entities);
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TaskResponseDto>> GetById(Guid Id)
    {
        var entities = await _service.GetByIdAsync(Id);
        var response = _mapper.Map<TaskResponseDto>(entities);
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<TaskResponseDto>> Create(CreateTaskDto dto)
    {
        var entity = _mapper.Map<Task>(dto);
        var createdEntity = await _service.AddAsync(entity);
        var newEntity = _mapper.Map<TaskResponseDto>(createdEntity);
        return Ok(newEntity);
    }

    [HttpPut]
    public async Task<ActionResult<TaskResponseDto>> Update(UpdateTaskDto dto)
    {
        var entity = _mapper.Map<Task>(dto);
        var createdEntity = await _service.UpdateAsync(entity);
        var newEntity = _mapper.Map<TaskResponseDto>(createdEntity);
        return Ok(newEntity);
    }
}