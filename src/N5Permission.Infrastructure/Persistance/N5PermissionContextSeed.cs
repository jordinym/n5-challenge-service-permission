using N5Permission.Domain.AggregatesModel.PermissionAggregate;
using N5Permission.Application.Common;

namespace N5Permission.Infrastructure.Persistance
{
    public static class N5PermissionContextSeed
    {  
        public static async Task SeedDataAsync(N5PermissionContext context)
        { 
            await AddPermissionType(context); 
        }
        public static async Task AddPermissionType(N5PermissionContext context)
        {
            if (!context.PermissionTypes.Any(a => a.Description.Equals("Administrator")))
            {
                context.PermissionTypes.AddRange(
                  new PermissionTypeEntity { Description = "Administrator", Status=true,CreationUser="SYSTEM",CreationDate=Constants.GetCustomDateTimeNow() }
                 );
                await context.SaveChangesAsync();
            }

        } 
    }
}
