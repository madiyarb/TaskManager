using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.Domain.Entities;

namespace TaskManager.Infrastucture.Persistance.DbMap;

public class TaskDbMap : IEntityTypeConfiguration<TaskDbModel>
{
    public void Configure(EntityTypeBuilder<TaskDbModel> builder)
    {
        builder.Property(p => p.CreatedDate).HasColumnType("TIMESTAMP").HasDefaultValueSql("now()").IsRequired();
        builder.Property(p => p.LastModifiedDate).HasColumnType("TIMESTAMP").HasDefaultValueSql("now()").IsRequired();
        builder.Property(p => p.CompletionDate).HasColumnType("TIMESTAMP").HasDefaultValueSql("NULL");
        builder.Property(p => p.Name).HasColumnType("VARCHAR(100)").IsRequired();
        builder.Property(p => p.ProjectId).HasColumnType("INTEGER").IsRequired();
        builder.Property(p => p.Priority).HasColumnType("INTEGER").IsRequired();
        builder.Property(p => p.Description).HasColumnType("VARCHAR(500)").HasDefaultValue("").IsRequired();
    }
}