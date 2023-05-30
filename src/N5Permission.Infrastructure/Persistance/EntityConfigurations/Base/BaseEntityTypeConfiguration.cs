using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using N5Permission.Domain.Common;
using System;

namespace N5Permission.Infrastructure.EntityConfigurations.Base
{
    public abstract class BaseEntityTypeConfiguration<T> : IEntityTypeConfiguration<T>
    where T : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(c => c.CreationUser)
                .HasColumnName("vc_creation_user")
                .HasComment("Creation user")
                .HasColumnType("varchar(120)")
                .IsRequired();

            builder.Property(c => c.CreationDate)
                .HasColumnName("dt_creation_date")
                .HasComment("Creation date")
                .HasColumnType("datetime2")
                .IsRequired(); 

            builder.Property(c => c.ModificationUser)
                .HasColumnName("vc_modification_user")
                .HasComment("Modification user")
                .HasColumnType("varchar(120)")
                .IsRequired(false);


            builder.Property(c => c.ModificationDate)
                .HasColumnName("dt_modification_date")
                .HasComment("Modification date")
                .HasColumnType("datetime2");

             
            builder.Property(c => c.Status).HasColumnName("bt_status").IsRequired();

            builder.HasQueryFilter(c => c.Status);
        }
    }
}
