using CommonRepository;
using TaskManager.Application.Contracts;
using TaskManager.Domain.Entities;
using TaskManager.Infrastucture.Persistance;

namespace TaskManager.Infrastucture.Repositories;

public class TaskRepository  : BaseRepository<TaskDbModel, TaskDbContext>, ITaskRepository
{
    public TaskRepository(TaskDbContext dbContext) : base(dbContext)
    {
    }

    protected override IQueryable<TaskDbModel> FilterByString(IQueryable<TaskDbModel> query, string? filterString) =>
        string.IsNullOrEmpty(filterString)
            ? query
            : query.Where(v => v.Name.ToLower().Contains(filterString.ToLower())
                               || v.TaskState.ToString().ToLower().Contains(filterString.ToLower())
                               || v.Description.ToLower().Contains(filterString.ToLower())
                               || v.Priority.ToString() == filterString);
}