using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public sealed class QuestionConfiguration : IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> builder)
    {
        // Primary Key
        builder.HasKey(x => x.Id);

        // Required fields
        builder.Property(x => x.QuestionText)
               .IsRequired()
               .HasMaxLength(1000);

        builder.Property(x => x.OptionA)
               .IsRequired()
               .HasMaxLength(200);

        builder.Property(x => x.OptionB)
               .IsRequired()
               .HasMaxLength(200);

        builder.Property(x => x.OptionC)
               .IsRequired()
               .HasMaxLength(200);

        builder.Property(x => x.OptionD)
               .IsRequired()
               .HasMaxLength(200);

        builder.Property(x => x.CorrectOption)
               .IsRequired()
               .HasMaxLength(10);

        builder.Property(x => x.Score)
               .IsRequired();

        builder.Property(x => x.Created)
               .IsRequired();

        builder.Property(x => x.Updated);

        builder.Property(x => x.CreatedBy);

        builder.Property(x => x.UpdatedBy);

        // Relationships
        builder.HasOne(x => x.Quiz)
               .WithMany(x => x.Questions)
               .HasForeignKey(x => x.QuizId)
               .OnDelete(DeleteBehavior.Cascade);

    }
}   