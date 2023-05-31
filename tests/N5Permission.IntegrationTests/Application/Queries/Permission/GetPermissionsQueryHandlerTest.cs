using NUnit.Framework;
using FluentAssertions;
using static N5Permission.IntegrationTests.Testing;
using N5Permission.Domain.AggregatesModel.PermissionAggregate;
using N5Permission.Application.Permission.Commands.CreatePermissionType;
using N5Permission.Application.Permission.Queries.GetPermissionWithPagination;
using N5Permission.Application.Common;

namespace N5Permission.IntegrationTests.Application.Commands.Permission
{

    public class GetPermissionsQueryHandlerTest : BaseTestFixture
    {  
        [Test]
        public async Task Handle_Return_AllPermission_AndPermissionType()
        {
            var permissionType = await SendAsync(new CreatePermissionTypeCommand
            {
                Description = "Administrator",
            });

            await AddAsync(new PermissionEntity
            {
                EmployeeForename = "Jordiny",
                EmployeeSurname = "Montanez",
                PermisionTypeId = permissionType,
                CreationUser = "SYSTEM",
                CreationDate = Constants.GetCustomDateTimeNow()
                
            });

            var query = new GetPermissionsQuery();

            var permissions = await SendAsync(query);
               
            permissions.Should().HaveCountGreaterThan(0); 
        }
        

    }
}
