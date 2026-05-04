using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShepidiSoft.Domain.Entities;

namespace ShepidiSoft.Persistence.CommunityServices;

public sealed class CommunityServiceConfiguration : IEntityTypeConfiguration<CommunityService>
{
    public void Configure(EntityTypeBuilder<CommunityService> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(2000);

        builder.Property(x => x.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(x => x.ImageUrl)
            .IsRequired(false)
            .HasMaxLength(500);

        builder.Property(x => x.Created)
            .IsRequired();

        builder.Property(x => x.CreatedBy)
            .IsRequired(false);

        builder.Property(x => x.Updated)
            .IsRequired(false);

        builder.Property(x => x.UpdatedBy)
            .IsRequired(false);

        // Index'ler
        builder.HasIndex(x => x.IsActive);
        builder.HasIndex(x => x.Created);
    }
}

