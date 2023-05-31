using N5Permission.Application.Permission.Commands.CreatePermission;
using NUnit.Framework;
using FluentAssertions;
using static N5Permission.IntegrationTests.Testing;
using N5Permission.Application.Common.Exceptions;
using N5Permission.Domain.AggregatesModel.PermissionAggregate;
using N5Permission.Application.Permission.Commands.CreatePermissionType;

namespace N5Permission.IntegrationTests.Application.Commands.Permission
{

    public class CreatePermissionCommandHandlerTest : BaseTestFixture
    {
        [Test]
        public async Task Handle_ReturnException_When_RequireMinimumFields()
        {
            var command = new CreatePermissionCommand();

            await FluentActions.Invoking(() =>
                SendAsync(command)).Should().ThrowAsync<ValidationException>();
        }

        [Test]
        public async Task Handle_ReturnGreaterThanZero_When_CreatePermission()
        {
            var permissionType = await SendAsync(new CreatePermissionTypeCommand
            {
                Description = "Administrator", 
            });

            var permission = await SendAsync(new CreatePermissionCommand
            {
                EmployeeForename = "Jordiny",
                EmployeeSurname = "Montanez",
                PermisionTypeId = permissionType
            });
             

            var permissionEntity = await FindAsync<PermissionEntity>(permission);

            permissionEntity.Should().NotBeNull();
            permissionEntity!.Id.Should().Be(permission);
            permissionEntity.PermisionTypeId.Should().Be(permissionType); 
        }
        

    }
}
