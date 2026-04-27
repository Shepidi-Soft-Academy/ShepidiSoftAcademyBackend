using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShepidiSoft.Domain.Entities;

namespace ShepidiSoft.Persistence.CollaborationApplications;

public sealed class CollaborationApplicationConfiguration : IEntityTypeConfiguration<CollaborationApplication>
{
    public void Configure(EntityTypeBuilder<CollaborationApplication> builder)
    {
        builder.Property(e => e.Title).IsRequired().HasMaxLength(200);
        builder.Property(e => e.Description).IsRequired().HasMaxLength(500);
        builder.Property(e => e.CommunityName).IsRequired().HasMaxLength(200);
        builder.Property(e => e.ContactName).IsRequired().HasMaxLength(200);
        builder.Property(e => e.ContactEmail).IsRequired().HasMaxLength(200);
        builder.Property(e => e.ContactPhone).IsRequired().HasMaxLength(20);
    }
}
