using CommonRepository;
using ProjectManager.Application.Contracts;
using ProjectManager.Domain.Entities;
using ProjectManager.Infrastructure.Persistance;

namespace ProjectManager.Infrastructure.Repositories;

public class ProjectRepository : BaseRepository<ProjectDbModel, ProjectDbContext>, IProjectRepository
{
    public ProjectRepository(ProjectDbContext dbContext) : base(dbContext)
    {
    }

    protected override IQueryable<ProjectDbModel> FilterByString(IQueryable<ProjectDbModel> query, string? filterString)
    {
        return string.IsNullOrEmpty(filterString)
            ? query
            : query.Where(v => v.Name.ToLower().Contains(filterString.ToLower())
                               || v.State.ToString().ToLower().Contains(filterString.ToLower())
                               || v.Description.ToLower().Contains(filterString.ToLower())
            );
    }
}