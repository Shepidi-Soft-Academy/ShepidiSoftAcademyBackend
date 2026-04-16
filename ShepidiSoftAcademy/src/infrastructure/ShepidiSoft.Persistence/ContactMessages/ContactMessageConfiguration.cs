using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShepidiSoft.Domain.Entities;

namespace ShepidiSoft.Persistence.ContactMessages;

public sealed class ContactMessageConfiguration : IEntityTypeConfiguration<ContactMessage>
{
    public void Configure(EntityTypeBuilder<ContactMessage> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(256);

        builder.Property(x => x.Phone)
            .HasMaxLength(20);

        builder.Property(x => x.Content)
            .IsRequired()
            .HasMaxLength(5000);

        builder.Property(x => x.SentAt)
            .IsRequired();

        builder.Property(x => x.IsRead)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(x => x.Created)
            .IsRequired();

        builder.Property(x => x.Updated);

        // Index'ler
        builder.HasIndex(x => x.Email);
        builder.HasIndex(x => x.SentAt);
        builder.HasIndex(x => x.IsRead);

        // Composite index - Okunmamış mesajları tarihe göre
        builder.HasIndex(x => new { x.IsRead, x.SentAt });
    }
}