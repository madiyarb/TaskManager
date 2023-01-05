using Microsoft.EntityFrameworkCore;
using TaskManager.Domain.Entities;
using TaskManager.Infrastucture.Persistance.DbMap;

namespace TaskManager.Infrastucture.Persistance;

public class TaskDbContext : DbContext
{
    public DbSet<TaskDbModel> Tasks { get; set; }

    public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfiguration(new TaskDbMap());
    }
}