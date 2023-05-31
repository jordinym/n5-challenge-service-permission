using N5Permission.Application.Permission.Commands.CreatePermission;
using NUnit.Framework;
using FluentAssertions;
using static N5Permission.IntegrationTests.Testing;
using N5Permission.Application.Common.Exceptions;
using N5Permission.Domain.AggregatesModel.PermissionAggregate;
using N5Permission.Application.Permission.Commands.CreatePermissionType;
using N5Permission.Application.Permission.Commands.UpdatePermission;

namespace N5Permission.IntegrationTests.Application.Commands.Permission
{

    public class UpdatePermissionCommandHandlerTest : BaseTestFixture
    {
        [Test]
        public async Task Handle_ReturnException_When_RequireMinimumFields()
        {
            var command = new UpdatePermissionCommand();

            await FluentActions.Invoking(() =>
                SendAsync(command)).Should().ThrowAsync<ValidationException>();
        }

        [Test]
        public async Task Handle_ReturnGreaterThanZero_When_UpdatePermission()
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

            var command = new UpdatePermissionCommand
            {
                EmployeeForename = "JordinyTest",
                EmployeeSurname = "MontanezTest",
                PermisionTypeId = permissionType,
                Id= permission

            };

            var permissionUpdate = await SendAsync(command);


            var permissionEntity = await FindAsync<PermissionEntity>(permissionUpdate);

            permissionEntity.Should().NotBeNull();
            permissionEntity!.Id.Should().Be(permissionUpdate);
            permissionEntity.PermisionTypeId.Should().Be(permissionType);
            permissionEntity.EmployeeForename.Should().Be(command.EmployeeForename);
            permissionEntity.EmployeeSurname.Should().Be(command.EmployeeSurname);
        }


    }
}
