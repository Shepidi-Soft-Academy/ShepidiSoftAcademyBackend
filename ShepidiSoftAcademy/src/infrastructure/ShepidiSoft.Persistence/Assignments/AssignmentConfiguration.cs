using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShepidiSoft.Domain.Entities;

namespace ShepidiSoft.Persistence.Assignments;

public sealed class AssignmentConfiguration : IEntityTypeConfiguration<Assignment>
{
    public void Configure(EntityTypeBuilder<Assignment> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(300);

        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(3000);

        builder.Property(x => x.DueDate);

        builder.Property(x => x.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(x => x.CourseId)
            .IsRequired();

        builder.Property(x => x.Created)
            .IsRequired();

        builder.Property(x => x.Updated);

        // Assignment -> Course (Many-to-One)
        builder.HasOne(x => x.Course)
            .WithMany() 
            .HasForeignKey(x => x.CourseId)
            .OnDelete(DeleteBehavior.Cascade); // Course silinince Assignment'lar da silinir

        // Relationship: Assignment -> AssignmentSubmissions (One-to-Many)
        builder.HasMany(x => x.Submissions)
            .WithOne(s => s.Assignment)
            .HasForeignKey(s => s.AssignmentId)
            .OnDelete(DeleteBehavior.Cascade); // Assignment silinince Submission'lar da silinir

        // Index'ler
        builder.HasIndex(x => x.CourseId);
        builder.HasIndex(x => x.DueDate);
        builder.HasIndex(x => x.IsActive);

        builder.HasIndex(x => new { x.CourseId, x.IsActive, x.DueDate });  // Composite index - Bir dersin aktif ödevlerini teslim tarihine göre

    }
}