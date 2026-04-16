using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShepidiSoft.Domain.Entities;

namespace ShepidiSoft.Persistence.Instructors;

public sealed class InstructorConfiguration : IEntityTypeConfiguration<Instructor>
{
    public void Configure(EntityTypeBuilder<Instructor> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.UserId)
            .IsRequired()
            .HasMaxLength(450); 


        builder.Property(x => x.Bio)
            .IsRequired()
            .HasMaxLength(3000);

        builder.Property(x => x.Expertise)
            .IsRequired()
            .HasMaxLength(1000); 

        builder.Property(x => x.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(x => x.Created)
            .IsRequired();

        builder.Property(x => x.Updated);

        // Relationship: Instructor -> Courses (One-to-Many)
        builder.HasMany(x => x.Courses)
            .WithOne(c => c.Instructor)
            .HasForeignKey(c => c.InstructorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.UserId)
            .IsUnique();

        // Index'ler
        builder.HasIndex(x => x.IsActive);

        builder.HasIndex(x => new { x.IsActive, x.Created });
    }
}