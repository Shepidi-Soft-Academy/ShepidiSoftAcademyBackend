using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShepidiSoft.Domain.Entities;
using ShepidiSoft.Domain.Entities.Organizations;
using ShepidiSoft.Identity.Models;
using System.Reflection;

namespace ShepidiSoft.Persistence.Context;

public class AppDbContext(DbContextOptions options) : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
     
    }

    public DbSet<Student> Students { get; set; }
    public DbSet<Announcement> Announcements { get; set; }
    public DbSet<Activity> Activities { get; set; }
    public DbSet<Assignment> Assigments { get; set; }
    public DbSet<AssignmentSubmission> AssigmentSubmissions { get; set; }
    public DbSet<ContactMessage> ContactMessages { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<CourseMembership> CourseMemberships { get; set; }
    public DbSet<Document> Documents { get; set; }
    public DbSet<DocumentTopic> DocumentTopics { get; set; }
    public DbSet<Instructor> Instructors { get; set; }
    public DbSet<Offering> Offerings { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<ProjectImage> ProjectImages { get; set; }
    public DbSet<Organization> Organizations { get; set; }
    public DbSet<OrganizationMember> OrganizationMembers { get; set; } 
    public DbSet<OrganizationPosition> OrganizationPositions { get; set; }
    public DbSet<CareerApplication> CareerApplications { get; set; }
    public DbSet<OutboxMessage> OutboxMessages { get; set; }
}
