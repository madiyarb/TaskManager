using CommonRepository.Models;
using TaskManager.Domain.Complementary;

namespace TaskManager.Domain.Entities;

public class TaskDbModel : BaseRepositoryEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int ProjectId { get; set; }
    public int Priority { get; set; }
    public TaskStateEnums TaskState { get; set; }
}