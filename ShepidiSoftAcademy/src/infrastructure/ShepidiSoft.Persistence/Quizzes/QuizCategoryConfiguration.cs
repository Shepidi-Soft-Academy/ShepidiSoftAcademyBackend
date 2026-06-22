using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShepidiSoft.Domain.Entities.Quizzes;

public sealed class QuizCategoryConfiguration : IEntityTypeConfiguration<QuizCategory>
{
    public void Configure(EntityTypeBuilder<QuizCategory> builder)
    {
        builder.Property(q => q.Name)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(q => q.Description)
               .IsRequired()
               .HasMaxLength(5000);

        builder.Property(q => q.Created)
               .IsRequired();

        builder.Property(q => q.Updated);

        builder.Property(q => q.CreatedBy);

        builder.Property(q => q.UpdatedBy);
    }
}   