using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShepidiSoft.Domain.Entities;

namespace ShepidiSoft.Persistence.DocumentTopics;

public sealed class DocumentTopicConfiguration : IEntityTypeConfiguration<DocumentTopic>
{
    public void Configure(EntityTypeBuilder<DocumentTopic> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.Description)
            .HasMaxLength(1000);

        builder.Property(x => x.Created)
            .IsRequired();

        builder.Property(x => x.Updated);

        // Relationship: DocumentTopic -> Documents (One-to-Many)
        builder.HasMany(x => x.Documents)
            .WithOne(d => d.DocumentTopic)
            .HasForeignKey(d => d.DocumentTopicId)
            .OnDelete(DeleteBehavior.Cascade);

        // Unique Constraint: Topic adı benzersiz olmalı
        builder.HasIndex(x => x.Name)
            .IsUnique();

   
    }
}