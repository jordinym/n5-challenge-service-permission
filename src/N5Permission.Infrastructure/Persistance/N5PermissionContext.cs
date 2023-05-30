using Microsoft.EntityFrameworkCore;
using N5Permission.Domain.AggregatesModel.PermissionAggregate;
using N5Permission.Infrastructure.EntityConfigurations;

namespace N5Permission.Infrastructure.Persistance
{
    public class N5PermissionContext : DbContext
    {
        public N5PermissionContext(DbContextOptions<N5PermissionContext> options) : base(options)
        {
        }

        #region DbSet
        public virtual DbSet<PermissionTypeEntity> PermissionTypes { get; set; }
        public virtual DbSet<PermissionEntity> Permissions { get; set; }
        #endregion

        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new PermissionConfiguration());
            builder.ApplyConfiguration(new PermissionTypeConfiguration());

            #region Function Configurations

            builder.HasDbFunction(typeof(N5PermissionContext).GetMethod(nameof(ConvertDateTimeToString), new[] { typeof(DateTime), typeof(string) }) ?? throw new InvalidOperationException())
            .HasName("to_char");

            #endregion
        }

        #region Functions
        public string ConvertDateTimeToString(DateTime dateTime, string format)
            => throw new NotSupportedException(); 
        #endregion
    }
}