using N5Permission.Application.Permission.Commands.CreatePermission;
using NUnit.Framework;
using FluentAssertions;
using static N5Permission.IntegrationTests.Testing;
using N5Permission.Application.Common.Exceptions;
using N5Permission.Domain.AggregatesModel.PermissionAggregate;
using N5Permission.Application.Permission.Commands.CreatePermissionType;
using N5Permission.Application.Permission.Commands.UpdatePermission;
using N5Permission.Application.Permission.Commands.UpdatePermissionType;

namespace N5Permission.IntegrationTests.Application.Commands.Permission
{

    public class UpdatePermissionTypeCommandHandlerTest : BaseTestFixture
    {
        [Test]
        public async Task Handle_ReturnException_When_RequireMinimumFields()
        {
            var command = new UpdatePermissionTypeCommand();

            await FluentActions.Invoking(() =>
                SendAsync(command)).Should().ThrowAsync<ValidationException>();
        }

        [Test]
        public async Task Handle_ReturnGreaterThanZero_When_UpdatePermissionType()
        {
            var permissionType = await SendAsync(new CreatePermissionTypeCommand
            {
                Description = "Administrator",
            });
             
            var command = new UpdatePermissionTypeCommand
            {
                Description = "AdministratorTest", 
                Id= permissionType

            };

            var permissionUpdate = await SendAsync(command);


            var permissionTypeEntity = await FindAsync<PermissionTypeEntity>(permissionUpdate);

            permissionTypeEntity.Should().NotBeNull();
            permissionTypeEntity!.Id.Should().Be(permissionUpdate); 
            permissionTypeEntity.Description.Should().Be(command.Description); 
        }


    }
}
