using CommonRepository;
using Microsoft.EntityFrameworkCore;
using ServiceContracts.Project.Queries;
using TaskManager.Application.Contracts;
using TaskManager.Domain.Complementary;
using TaskManager.Domain.Entities;
using TaskManager.Infrastucture.Persistance;

namespace TaskManager.Infrastucture.Repositories;

public class TaskRepository  : BaseRepository<TaskDbModel, TaskDbContext>, ITaskRepository
{
    private readonly TaskDbContext _dbContext;
    public TaskRepository(TaskDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    protected override IQueryable<TaskDbModel> FilterByString(IQueryable<TaskDbModel> query, string? filterString) =>
        string.IsNullOrEmpty(filterString)
            ? query
            : query.Where(v => v.Name.ToLower().Contains(filterString.ToLower())
                               || v.TaskState.ToString().ToLower().Contains(filterString.ToLower())
                               || v.Description.ToLower().Contains(filterString.ToLower())
                               || v.Priority.ToString() == filterString);

    public Task<bool> CheckForNotCompletedFromProject(int id)
    {
       return _dbContext.Tasks.Where(t=>t.ProjectId == id)
           .AnyAsync(t=>t.TaskState == TaskStateEnums.InProgress || t.TaskState == TaskStateEnums.InProgress);
    }
}