using TaskManager.Domain.Complementary;

namespace ServiceContracts.Task.Models;

public class TaskVm
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int ProjectId { get; set; }
    public int Priority { get; set; }
    public TaskStateEnums TaskState { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime LastModifiedDate { get; set; }
    public DateTime? CompletionDate { get; set; }
}