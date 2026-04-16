using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShepidiSoft.Domain.Entities;

namespace ShepidiSoft.Persistence.ProjectImages;

public sealed class ProjectImageConfiguration : IEntityTypeConfiguration<ProjectImage>
{
    public void Configure(EntityTypeBuilder<ProjectImage> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.ImageUrl)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(x => x.IsMain)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(x => x.OrderIndex)
            .IsRequired()
            .HasDefaultValue(0);

        builder.Property(x => x.Description)
            .HasMaxLength(500);

        builder.Property(x => x.ProjectId)
            .IsRequired();

        builder.Property(x => x.Created)
            .IsRequired();

        builder.Property(x => x.Updated);

        // Relationship: ProjectImage -> Project (Many-to-One)
        builder.HasOne(x => x.Project)
            .WithMany(p => p.Images)
            .HasForeignKey(x => x.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);

        // Index'ler
        builder.HasIndex(x => x.ProjectId);

        // Composite index - Bir projenin görselleri sıraya göre
        builder.HasIndex(x => new { x.ProjectId, x.OrderIndex });

        // Composite index - Bir projenin ana görseli
        builder.HasIndex(x => new { x.ProjectId, x.IsMain });
    }
}