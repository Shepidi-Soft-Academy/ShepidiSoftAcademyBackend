using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShepidiSoft.Domain.Entities;

namespace ShepidiSoft.Persistence.Students;

public sealed class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.UserId)
            .IsRequired()
            .HasMaxLength(450); // ASP.NET Identity UserId standart uzunluğu

        builder.Property(x => x.RegisteredAt)
            .IsRequired();

        builder.Property(x => x.Created)
            .IsRequired();

        builder.Property(x => x.Updated);

        // Relationship: Student <-> Courses (Many-to-Many)
        builder.HasMany(x => x.Courses)
            .WithMany(c => c.Students)
            .UsingEntity<Dictionary<string, object>>(
                "CourseStudent", // Junction table adı
                j => j.HasOne<Course>()
                      .WithMany()
                      .HasForeignKey("CourseId")
                      .OnDelete(DeleteBehavior.Cascade),
                j => j.HasOne<Student>()
                      .WithMany()
                      .HasForeignKey("StudentId")
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

        // Relationship: Student -> AssignmentSubmissions (One-to-Many)
        builder.HasMany(x => x.AssignmentSubmissions)
            .WithOne(a => a.Student)
            .HasForeignKey(a => a.StudentId)
            .OnDelete(DeleteBehavior.Restrict); // Student silinirse submission'ları varsa hata ver

        builder.HasIndex(x => x.UserId)
            .IsUnique();

        // Index'ler
        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.RegisteredAt);
    }
}