using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public sealed class QuizConfiguration : IEntityTypeConfiguration<Quiz>
{
    public void Configure(EntityTypeBuilder<Quiz> builder)
    {
        builder.Property(q => q.Title)
               .IsRequired()
               .HasMaxLength(300);

        builder.Property(q => q.Description)
               .IsRequired()
               .HasMaxLength(5000);

        builder.Property(q => q.TimeLimit)
               .IsRequired();

        builder.Property(q => q.ScoreLimit)
               .IsRequired();

        // Navigation - Questions
        builder.HasMany(q => q.Questions)
               .WithOne(q => q.Quiz)
               .HasForeignKey(q => q.QuizId)
               .OnDelete(DeleteBehavior.Cascade);

        // Navigation - Attempts
        builder.HasMany(q => q.Attempts)
               .WithOne(a => a.Quiz)
               .HasForeignKey(a => a.QuizId)
               .OnDelete(DeleteBehavior.Cascade);

        // Audit fields
        builder.Property(q => q.Created)
               .IsRequired();

        builder.Property(q => q.Updated);

        builder.Property(q => q.CreatedBy);

        builder.Property(q => q.UpdatedBy);
    }
}
