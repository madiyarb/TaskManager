using CommonRepository.Models;
using ProjectManager.Domain.Complementary;

namespace ProjectManager.Domain.Entities;

public class ProjectDbModel : BaseRepositoryEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public StateEnums State { get; set; }
}