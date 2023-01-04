using CommonRepository.Abstractions;
using ProjectManager.Domain.Entities;

namespace ProjectManager.Application.Contracts;

public interface IProjectRepository : IBaseRepository<ProjectDbModel>
{
    
}