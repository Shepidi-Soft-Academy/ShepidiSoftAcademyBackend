using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShepidiSoft.Domain.Entities;

namespace ShepidiSoft.Persistence.CourseMemberships
{
    public sealed class CourseMembershipConfiguration : IEntityTypeConfiguration<CourseMembership>
    {
        public void Configure(EntityTypeBuilder<CourseMembership> builder)
        {
            // Primary Key
            builder.HasKey(cm => cm.Id);

            // Unique constraint: aynı kullanıcı aynı kursa birden fazla kayıt olmasın
            builder.HasIndex(cm => new { cm.CourseId, cm.UserId })
                   .IsUnique();

            // Relation: Course -> Memberships
            builder.HasOne(cm => cm.Course)
                   .WithMany(c => c.Memberships)
                   .HasForeignKey(cm => cm.CourseId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Role property
            builder.Property(cm => cm.Role)
                   .IsRequired()
                   .HasMaxLength(50);

            // JoinedAt
            builder.Property(cm => cm.JoinedAt)
                   .IsRequired()
                   .HasDefaultValueSql("GETUTCDATE()");

            // Audit fields
            builder.Property(cm => cm.Created)
                   .IsRequired()
                   .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(cm => cm.Updated)
                   .IsRequired(false);
        }
    }
}