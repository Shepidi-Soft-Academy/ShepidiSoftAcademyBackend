using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShepidiSoft.Domain.Entities;

namespace ShepidiSoft.Persistence.Announcements;

public sealed class AnnouncementConfiguration : IEntityTypeConfiguration<Announcement>
{
    public void Configure(EntityTypeBuilder<Announcement> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(300);

        builder.Property(x => x.Content)
            .IsRequired()
            .HasMaxLength(5000);

        builder.Property(x => x.PublishedAt)
            .IsRequired();

        builder.Property(x => x.CreatedByUserId)
            .IsRequired()
            .HasMaxLength(450); 

        builder.Property(x => x.Created)
            .IsRequired();

        builder.Property(x => x.Updated);

        // Index
        builder.HasIndex(x => x.PublishedAt);
        builder.HasIndex(x => x.CreatedByUserId);

        // Composite index - Kullanıcının duyurularını tarihe göre sıralamak için
        builder.HasIndex(x => new { x.CreatedByUserId, x.PublishedAt });
    }
}