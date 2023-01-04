using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectManager.Domain.Entities;

namespace ProjectManager.Infrastructure.Persistance.DbMap;

public class ProjectDbMap : IEntityTypeConfiguration<ProjectDbModel>
{
    public void Configure(EntityTypeBuilder<ProjectDbModel> builder)
    {
        builder.Property(p => p.CreatedDate).HasColumnType("TIMESTAMP").HasDefaultValueSql("now()").IsRequired();
        builder.Property(p => p.LastModifiedDate).HasColumnType("TIMESTAMP").HasDefaultValueSql("now()").IsRequired();
        builder.Property(p => p.CompletionDate).HasColumnType("TIMESTAMP").HasDefaultValueSql("NULL");
        builder.Property(p => p.Name).HasColumnType("VARCHAR(100)").IsRequired();
        builder.Property(p => p.Description).HasColumnType("VARCHAR(500)").HasDefaultValue("").IsRequired();
    }
}