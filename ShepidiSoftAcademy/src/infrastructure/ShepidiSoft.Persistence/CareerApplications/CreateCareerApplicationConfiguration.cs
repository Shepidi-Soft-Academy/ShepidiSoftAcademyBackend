using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShepidiSoft.Domain.Entities;

public sealed class CreateCareerApplicationConfiguration : IEntityTypeConfiguration<CareerApplication>
{
    public void Configure(EntityTypeBuilder<CareerApplication> builder)
    {
        builder.ToTable("CareerApplications");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.FirstName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.LastName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(x => x.PhoneNumber)
            .HasMaxLength(20);

        builder.Property(x => x.LinkedInUrl)
            .HasMaxLength(250);

        builder.Property(x => x.GithubUrl)
            .HasMaxLength(250);

        builder.Property(x => x.CoverLetter)
            .IsRequired()
            .HasMaxLength(2000);

        builder.Property(x => x.CvUrl)
            .HasMaxLength(500);

        builder.Property(x => x.AdminNote)
            .HasMaxLength(1000);

        // Enum
        builder.Property(x => x.Status)
            .IsRequired()
            .HasConversion<int>();

        // Dates
        builder.Property(x => x.CreatedDate)
            .IsRequired();

        builder.Property(x => x.Created)
            .IsRequired();

        builder.Property(x => x.Updated);

        // Audit
        builder.Property(x => x.CreatedBy);
        builder.Property(x => x.UpdatedBy);

        // Relationships
        builder.HasOne(x => x.OrganizationPosition)
            .WithMany() 
            .HasForeignKey(x => x.OrganizationPositionId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.Email);
        builder.HasIndex(x => x.OrganizationPositionId);
    }
}