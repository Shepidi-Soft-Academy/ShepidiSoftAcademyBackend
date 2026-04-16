using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShepidiSoft.Domain.Entities;

namespace ShepidiSoft.Persistence.Projects;

public sealed class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(300);

        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(5000);

        builder.Property(x => x.Technologies)
            .HasMaxLength(1000); // ".NET 8, React, Docker, PostgreSQL"

        builder.Property(x => x.ImageUrl)
            .HasMaxLength(1000);

        builder.Property(x => x.ProjectUrl)
            .HasMaxLength(1000);

        builder.Property(x => x.IsFeatured)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(x => x.CompletionDate);

        builder.Property(x => x.Created)
            .IsRequired();

        builder.Property(x => x.Updated);

        // Relationship: Project -> ProjectImages (One-to-Many)
        builder.HasMany(x => x.Images)
            .WithOne(i => i.Project)
            .HasForeignKey(i => i.ProjectId)
            .OnDelete(DeleteBehavior.Cascade); // Project silinince Image'lar da silinir

        // Index'ler
        builder.HasIndex(x => x.IsFeatured);
        builder.HasIndex(x => x.CompletionDate);

        // Composite index - Öne çıkan projeleri tarihe göre
        builder.HasIndex(x => new { x.IsFeatured, x.CompletionDate });

        // Composite index - Öne çıkan projeleri oluşturulma tarihine göre
        builder.HasIndex(x => new { x.IsFeatured, x.Created });
    }
}