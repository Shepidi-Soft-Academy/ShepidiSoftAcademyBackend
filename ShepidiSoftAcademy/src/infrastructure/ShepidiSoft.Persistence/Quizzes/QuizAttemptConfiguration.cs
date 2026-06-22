using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShepidiSoft.Domain.Entities.Quizzes;

public sealed class QuizAttemptConfiguration : IEntityTypeConfiguration<QuizAttempt>
{
    public void Configure(EntityTypeBuilder<QuizAttempt> builder)
    {
        // Primary Key
        builder.HasKey(x => x.Id);

        // Required fields
        builder.Property(x => x.QuizId)
               .IsRequired();

        builder.Property(x => x.StudentId)
               .IsRequired();

        builder.Property(x => x.StartedAt)
               .IsRequired();

        builder.Property(x => x.SubmittedAt)
               .IsRequired();

        builder.Property(x => x.Duration)
               .IsRequired();

        builder.Property(x => x.TotalScore)
               .IsRequired();

        builder.Property(x => x.CorrectAnswers)
               .IsRequired();

        builder.Property(x => x.IncorrectAnswers)
               .IsRequired();

        builder.Property(x => x.Created)
               .IsRequired();

        builder.Property(x => x.Updated);

        builder.Property(x => x.CreatedBy);

        builder.Property(x => x.UpdatedBy);

        // Relationships
        builder.HasOne(x => x.Quiz)
               .WithMany(x => x.Attempts)
               .HasForeignKey(x => x.QuizId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Student)
               .WithMany(x => x.QuizAttempts)
               .HasForeignKey(x => x.StudentId)
               .OnDelete(DeleteBehavior.Cascade);

    }
}