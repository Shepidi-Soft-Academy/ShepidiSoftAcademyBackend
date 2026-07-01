using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShepidiSoft.Domain.Entities;

namespace ShepidiSoft.Persistence.Configurations;

public sealed class PartnerConfiguration : IEntityTypeConfiguration<Partner>
{
    public void Configure(EntityTypeBuilder<Partner> builder)
    {
        builder.HasKey(x => x.Id);

        
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);

   
        builder.Property(x => x.Logo)
            .IsRequired()
            .HasMaxLength(500);

     
        builder.Property(x => x.WebsiteUrl)
            .HasMaxLength(500);

      
        builder.HasIndex(x => x.PartnerId).IsUnique();

        // Audit
        builder.Property(x => x.Created)
            .IsRequired();

        builder.Property(x => x.Updated);


        builder.Property(x => x.CreatedBy);

        builder.Property(x => x.UpdatedBy);
    }
}