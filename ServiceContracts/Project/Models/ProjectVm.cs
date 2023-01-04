using ProjectManager.Domain.Complementary;

namespace ServiceContracts.Project.Models;

public class ProjectVm
{
    public string Name { get; set; }
    public string Description { get; set; }
    public StateEnums State { get; set; }
}