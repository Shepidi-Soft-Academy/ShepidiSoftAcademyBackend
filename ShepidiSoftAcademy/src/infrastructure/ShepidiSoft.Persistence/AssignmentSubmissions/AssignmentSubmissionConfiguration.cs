using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShepidiSoft.Domain.Entities;

namespace ShepidiSoft.Persistence.AssignmentSubmissions;

public sealed class AssignmentSubmissionConfiguration : IEntityTypeConfiguration<AssignmentSubmission>
{
    public void Configure(EntityTypeBuilder<AssignmentSubmission> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.SubmissionContent)
            .IsRequired()
            .HasMaxLength(5000);

        builder.Property(x => x.SubmissionDate)
            .IsRequired();

        builder.Property(x => x.InstructorFeedback)
            .HasMaxLength(2000);

        builder.Property(x => x.Grade)
            .HasPrecision(5, 2); // 100.00 e.g

        builder.Property(x => x.AssignmentId)
            .IsRequired();

        builder.Property(x => x.StudentId)
            .IsRequired();

        builder.Property(x => x.Created)
            .IsRequired();

        builder.Property(x => x.Updated);

        // Relationship: AssignmentSubmission -> Assignment (Many-to-One)
        builder.HasOne(x => x.Assignment)
            .WithMany(a => a.Submissions)
            .HasForeignKey(x => x.AssignmentId)
            .OnDelete(DeleteBehavior.Cascade); 

        // Relationship: AssignmentSubmission -> Student (Many-to-One)
        builder.HasOne(x => x.Student)
            .WithMany() 
            .HasForeignKey(x => x.StudentId)
            .OnDelete(DeleteBehavior.Restrict); // Student silinemez eğer Submission'ı varsa

        // Unique Constraint: Bir öğrenci aynı ödeve sadece 1 kez gönderi yapabilir
        builder.HasIndex(x => new { x.AssignmentId, x.StudentId })
            .IsUnique();

        // Index'ler
        builder.HasIndex(x => x.AssignmentId);
        builder.HasIndex(x => x.StudentId);
        builder.HasIndex(x => x.SubmissionDate);

        
        builder.HasIndex(x => new { x.AssignmentId, x.SubmissionDate });

        builder.HasIndex(x => new { x.StudentId, x.SubmissionDate });
       
        builder.HasIndex(x => new { x.AssignmentId, x.Grade });
    }
}