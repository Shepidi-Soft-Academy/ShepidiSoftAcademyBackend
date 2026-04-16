using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShepidiSoft.Domain.Entities;

namespace ShepidiSoft.Persistence.Documents;

public sealed class DocumentConfiguration : IEntityTypeConfiguration<Document>
{
    public void Configure(EntityTypeBuilder<Document> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.DocumentTopicId)
            .IsRequired();

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(300);

        builder.Property(x => x.Description)
            .HasMaxLength(2000);

        builder.Property(x => x.FileUrl)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(x => x.PublishedAt)
            .IsRequired();

        builder.Property(x => x.UploadedByUserId)
            .IsRequired()
            .HasMaxLength(450);

        builder.Property(x => x.Created)
            .IsRequired();

        builder.Property(x => x.Updated);

        // Relationship: Document -> DocumentTopic (Many-to-One)
        builder.HasOne(x => x.DocumentTopic)
            .WithMany() 
            .HasForeignKey(x => x.DocumentTopicId)
            .OnDelete(DeleteBehavior.Cascade); // Topic silinince Document'lar da silinir

        // Index'ler
        builder.HasIndex(x => x.DocumentTopicId);
        builder.HasIndex(x => x.PublishedAt);
        builder.HasIndex(x => x.UploadedByUserId);

        // Composite index - Bir topic'in dökümanları tarihe göre
        builder.HasIndex(x => new { x.DocumentTopicId, x.PublishedAt });

        // Composite index - Bir kullanıcının yüklediği dökümanlar tarihe göre
        builder.HasIndex(x => new { x.UploadedByUserId, x.PublishedAt });
    }
}