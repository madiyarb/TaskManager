namespace ServiceContracts.Task.Models;

public class AllTasksVm
{
    public List<TaskVm> Tasks { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public string? Filter { get; set; }
}