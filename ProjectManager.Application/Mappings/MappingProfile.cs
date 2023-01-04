using AutoMapper;
using ProjectManager.Domain.Entities;
using ServiceContracts.Project.Commands;
using ServiceContracts.Project.Models;

namespace ProjectManager.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ProjectDbModel, ProjectVm>();
        CreateMap<CreateProjectCommand, ProjectDbModel>();
    }
}