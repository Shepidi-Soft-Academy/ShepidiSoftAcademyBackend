using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShepidiSoft.Domain.Entities.Organizations;

namespace ShepidiSoft.Persistence.OrganizationMemberPositions
{
    public sealed class OrganizationMemberPositionConfiguration : IEntityTypeConfiguration<OrganizationMemberPosition>
    {
        public void Configure(EntityTypeBuilder<OrganizationMemberPosition> builder)
        {
            // Composite Primary Key (Many-to-Many junction table)
            builder.HasKey(x => new { x.OrganizationMemberId, x.OrganizationPositionId });

            builder.Property(x => x.OrganizationMemberId)
                .IsRequired();

            builder.Property(x => x.OrganizationPositionId)
                .IsRequired();

            builder.Property(x => x.Created)
                .IsRequired();

            builder.Property(x => x.Updated);

            // Relationship: OrganizationMemberPosition -> OrganizationMember (Many-to-One)
            builder.HasOne(x => x.OrganizationMember)
                .WithMany(m => m.Positions)
                .HasForeignKey(x => x.OrganizationMemberId)
                .OnDelete(DeleteBehavior.Cascade); // Member silinince pozisyonları da silinir

            // Relationship: OrganizationMemberPosition -> OrganizationPosition (Many-to-One)
            builder.HasOne(x => x.OrganizationPosition)
                .WithMany(p => p.Members)
                .HasForeignKey(x => x.OrganizationPositionId)
                .OnDelete(DeleteBehavior.Restrict);

            // Index'ler (Composite key zaten index oluşturur ama ters yönde de lazım)
            builder.HasIndex(x => x.OrganizationMemberId);
            builder.HasIndex(x => x.OrganizationPositionId);
            
            // Composite index - Tarihe göre sıralama için
            builder.HasIndex(x => new { x.OrganizationMemberId, x.Created });
            builder.HasIndex(x => new { x.OrganizationPositionId, x.Created });

            // Table name
            builder.ToTable("OrganizationMemberPositions");
        }
    }
}