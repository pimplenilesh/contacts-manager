using ContactsManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsManager.Domain.EntityMapper
{
    public class ContactMap : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.HasKey(x => x.Id)
                   .HasName("pk_contactId");

            builder.Property(x => x.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("Id")
                    .HasColumnType("INT");

            builder.Property(x => x.FirstName)
                    .HasColumnType("FirstName")
                    .HasColumnType("NVARCHAR(50)")
                    .IsRequired();

            builder.Property(x => x.LastName)
                  .HasColumnType("LastName")
                  .HasColumnType("NVARCHAR(50)")
                  .IsRequired();

            builder.Property(x => x.Email)
                .HasColumnType("Email")
                .HasColumnType("NVARCHAR(50)")
                .IsRequired();

            builder.Property(x => x.PhoneNumber)
                .HasColumnType("PhoneNumber")
                .HasColumnType("NVARCHAR(20)")
                .IsRequired();

            builder.Property(x => x.Status)
                .HasColumnType("Status")
                .HasColumnType("NVARCHAR(20)")
                .IsRequired();

            builder.Property(x => x.CreatedBy)
                .HasColumnType("CreatedBy")
                .HasColumnType("NVARCHAR(50)")
                .IsRequired();

            builder.Property(x => x.Created)
                .HasColumnType("Created")
                .HasColumnType("datetime")
                .IsRequired();

            builder.Property(x => x.LastModifiedBy)
                .HasColumnType("CreatedBy")
                .HasColumnType("NVARCHAR(50)");

            builder.Property(x => x.LastModified)
              .HasColumnType("LastModified")
              .HasColumnType("datetime");
        }
    }
}
