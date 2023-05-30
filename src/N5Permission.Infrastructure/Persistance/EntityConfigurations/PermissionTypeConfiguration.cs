using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using N5Permission.Domain.AggregatesModel.PermissionAggregate;
using N5Permission.Infrastructure.EntityConfigurations.Base;

namespace N5Permission.Infrastructure.EntityConfigurations
{
    public class PermissionTypeConfiguration : BaseEntityTypeConfiguration<PermissionTypeEntity>
    {
        public override void Configure(EntityTypeBuilder<PermissionTypeEntity> builder)
        {
            builder.HasKey(c => c.Id);
            builder.ToTable("permission_type");

            builder.Property(c => c.Id)
                .HasColumnName("in_id")
                .HasComment("Unique ID")
                .IsRequired();

            builder.Property(c => c.Description)
                .HasColumnName("vc_description")
                .HasComment("Permission description")
                .HasColumnType("varchar(120)")
                .IsRequired();

            builder.Ignore(c => c.RegistrationDate);

            base.Configure(builder);
        }
    }
}
