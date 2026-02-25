using AutoMapper;
using TaskForge.Api.DTOs;
using TaskForge.Domain.Entities;

namespace TaskForge.Api.Profiles
{
    public class TaskGroupProfile : Profile
    {
        public TaskGroupProfile()
        {
            CreateMap<TaskGroup, TaskGroupResponseDto>();
            CreateMap<CreateTaskGroupDto, TaskGroup>();
            CreateMap<UpdateTaskGroupDto, TaskGroup>();
        }
    }
}
