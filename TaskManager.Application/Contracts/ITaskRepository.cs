using CommonRepository.Abstractions;
using TaskManager.Domain.Entities;

namespace TaskManager.Application.Contracts;

public interface ITaskRepository : IBaseRepository<TaskDbModel>
{
    
}