using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShepidiSoft.Domain.Entities;

namespace ShepidiSoft.Persistence.StudentRequests
{
    public sealed class StudentRequestConfiguration : IEntityTypeConfiguration<StudentRequest>
    {
        public void Configure(EntityTypeBuilder<StudentRequest> builder)
        {
            builder.HasKey(x=>x.Id);
            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(200);
            builder.Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(2000);
            builder.Property(x => x.StudentRequestStatus)
                .IsRequired();
            //ilişki
            builder.HasOne(x => x.Student)
                .WithMany()
                .HasForeignKey(x=>x.StudentId)
                .OnDelete(DeleteBehavior.Cascade);//baglı kayıtları sil
            builder.HasIndex(x => x.StudentId);
            builder.HasIndex(x => x.StudentRequestStatus);
        }
    }
}
