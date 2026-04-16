using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShepidiSoft.Domain.Entities;

namespace ShepidiSoft.Persistence.Activities;

public sealed class ActivityConfiguration : IEntityTypeConfiguration<Activity>
{
    public void Configure(EntityTypeBuilder<Activity> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(2000);

        builder.Property(x => x.Date)
            .IsRequired();

        builder.Property(x => x.IsOnline)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(x => x.Location)
            .HasMaxLength(500);

        builder.Property(x => x.OnlineMeetingUrl)
            .HasMaxLength(1000);

        builder.Property(x => x.Created)
            .IsRequired();

        builder.Property(x => x.Updated);

        // Index'ler
        builder.HasIndex(x => x.Date);  // performans artışı
        builder.HasIndex(x => x.IsOnline);
    }
}