using AutoMapper;
using ServiceContracts.Task.Commands;
using ServiceContracts.Task.Models;
using TaskManager.Domain.Entities;

namespace TaskManager.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<TaskDbModel, TaskVm>();
        CreateMap<CreateTaskCommand, TaskDbModel>();
        CreateMap<EditTaskCommand, TaskDbModel>();
    }
}