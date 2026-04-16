using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShepidiSoft.Domain.Entities;

namespace ShepidiSoft.Persistence.Courses;

public sealed class CourseConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(300);

        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(5000);

        builder.Property(x => x.InstructorId)
            .IsRequired();

        builder.Property(x => x.Location)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(x => x.IsOnline)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(x => x.MeetingUrl)
            .HasMaxLength(1000);

        builder.Property(x => x.CoverImageUrl)
            .HasMaxLength(1000);

        builder.Property(x => x.DurationInWeeks)
            .IsRequired();

        builder.Property(x => x.StartedDate)
            .IsRequired();

        builder.Property(x => x.EndDate)
            .IsRequired();

        builder.Property(x => x.Status)
            .IsRequired()
            .HasMaxLength(50); 

        builder.Property(x => x.Price)
            .HasPrecision(10, 2); 

        builder.Property(x => x.Created)
            .IsRequired();

        builder.Property(x => x.Updated);

        // Relationship: Course -> Instructor (Many-to-One)
        builder.HasOne(x => x.Instructor)
            .WithMany() // Instructor'da Courses varsa: .WithMany(i => i.Courses)
            .HasForeignKey(x => x.InstructorId)
            .OnDelete(DeleteBehavior.Restrict); 

        // Relationship: Course -> Assignments (One-to-Many)
        builder.HasMany(x => x.Assignments)
            .WithOne(a => a.Course)
            .HasForeignKey(a => a.CourseId)
            .OnDelete(DeleteBehavior.Cascade); // Course silinince Assignment'lar da silinir

        // Relationship: Course <-> Students (Many-to-Many)
        builder.HasMany(x => x.Students)
            .WithMany(s => s.Courses)
            .UsingEntity<Dictionary<string, object>>(
                "CourseStudent", // Junction table adı
                j => j.HasOne<Student>()
                      .WithMany()
                      .HasForeignKey("StudentId")
                      .OnDelete(DeleteBehavior.Cascade),
                j => j.HasOne<Course>()
                      .WithMany()
                      .HasForeignKey("CourseId")
                      .OnDelete(DeleteBehavior.Cascade),
                j =>
                {
                    j.HasKey("CourseId", "StudentId");
                    j.ToTable("CourseStudents");

                    // Junction table için index'ler
                    j.HasIndex("StudentId");
                    j.HasIndex("CourseId");
                }
            );

        // Index
        builder.HasIndex(x => x.InstructorId);
        builder.HasIndex(x => x.Status);
        builder.HasIndex(x => x.IsOnline);
        builder.HasIndex(x => x.StartedDate);
        builder.HasIndex(x => x.EndDate);

        // Composite index - Aktif kurslar, başlangıç tarihine göre
        builder.HasIndex(x => new { x.Status, x.StartedDate });

        // Composite index - Bir eğitmenin kursları, tarihe göre
        builder.HasIndex(x => new { x.InstructorId, x.StartedDate });

        // Composite index - Aktif online kurslar
        builder.HasIndex(x => new { x.Status, x.IsOnline, x.StartedDate });
    }
}