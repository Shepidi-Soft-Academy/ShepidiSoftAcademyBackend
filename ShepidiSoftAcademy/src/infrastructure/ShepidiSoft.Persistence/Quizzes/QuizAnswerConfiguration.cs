using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public sealed class QuizAnswerConfiguration : IEntityTypeConfiguration<QuizAnswer>
{
    public void Configure(EntityTypeBuilder<QuizAnswer> builder)
    {
        builder.Property(x => x.QuizAttemptId)
               .IsRequired();

        builder.Property(x => x.QuestionId)
               .IsRequired();

        builder.Property(x => x.SelectedOption)
               .HasMaxLength(50);

        builder.Property(x => x.IsCorrect)
               .IsRequired();

        builder.Property(x => x.Created)
               .IsRequired();

        builder.Property(x => x.Updated);

        builder.Property(x => x.CreatedBy);

        builder.Property(x => x.UpdatedBy);

        // Relationships
        builder.HasOne(x => x.QuizAttempt)
               .WithMany(x => x.Answers)
               .HasForeignKey(x => x.QuizAttemptId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Question)
               .WithMany()
               .HasForeignKey(x => x.QuestionId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}   