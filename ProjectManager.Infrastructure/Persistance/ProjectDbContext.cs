using Microsoft.EntityFrameworkCore;
using ProjectManager.Domain.Entities;
using ProjectManager.Infrastructure.Persistance.DbMap;

namespace ProjectManager.Infrastructure.Persistance;

public class ProjectDbContext : DbContext
{
    public DbSet<ProjectDbModel> Projects { get; set; }

    public ProjectDbContext(DbContextOptions<ProjectDbContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfiguration(new ProjectDbMap());
    }
}