using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using N5Permission.Domain.AggregatesModel.PermissionAggregate;
using N5Permission.Infrastructure.EntityConfigurations.Base;

namespace N5Permission.Infrastructure.EntityConfigurations
{
    public class PermissionConfiguration : BaseEntityTypeConfiguration<PermissionEntity>
    {
        public override void Configure(EntityTypeBuilder<PermissionEntity> builder)
        {
            builder.HasKey(c => c.Id);
            builder.ToTable("permission");

            builder.Property(c => c.Id)
                .HasColumnName("in_id")
                .HasComment("Unique ID")
                .IsRequired();

            builder.Property(c => c.PermisionTypeId)
                .HasColumnName("in_permission_type_id")
                .HasComment("Permission Type")
                .IsRequired();

            builder.Property(c => c.EmployeeForename)
                .HasColumnName("vc_employee_forename")
                .HasComment("Employee Forename")
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder.Property(c => c.EmployeeSurname)
                .HasColumnName("vc_employee_surname")
                .HasComment("Employee Surname")
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder.Ignore(c => c.RegistrationDate);

            builder.HasOne(d => d.PermissionType).WithMany(p => p.Permissions).HasForeignKey(d => d.PermisionTypeId);
            base.Configure(builder);
        }
    }
}
