using AutoMapper;
using TaskForge.Api.DTOs;
using Task = TaskForge.Domain.Entities.Task;

namespace TaskForge.Api.Profiles
{
    public class TaskProfile : Profile
    {
        public TaskProfile()
        {
            CreateMap<Task, TaskResponseDto>();
            CreateMap<CreateTaskDto, Task>();
            CreateMap<UpdateTaskDto, Task>();
        }
    }
}
