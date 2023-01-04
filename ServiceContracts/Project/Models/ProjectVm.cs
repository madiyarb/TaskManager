using ProjectManager.Domain.Complementary;

namespace ServiceContracts.Project.Models;

public class ProjectVm
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public StateEnums State { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime LastModifiedDate { get; set; }
    public DateTime? CompletionDate { get; set; }
}