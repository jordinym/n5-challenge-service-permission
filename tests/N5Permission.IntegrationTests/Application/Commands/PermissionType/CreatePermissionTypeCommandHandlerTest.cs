using NUnit.Framework;
using FluentAssertions;
using static N5Permission.IntegrationTests.Testing;
using N5Permission.Application.Common.Exceptions;
using N5Permission.Domain.AggregatesModel.PermissionAggregate;
using N5Permission.Application.Permission.Commands.CreatePermissionType;

namespace N5Permission.IntegrationTests.Application.Commands.Permission
{

    public class CreatePermissionTypeCommandHandlerTest : BaseTestFixture
    {
        [Test]
        public async Task Handle_ReturnException_When_RequireMinimumFields()
        {
            var command = new CreatePermissionTypeCommand();

            await FluentActions.Invoking(() =>
                SendAsync(command)).Should().ThrowAsync<ValidationException>();
        }

        [Test]
        public async Task Handle_ReturnGreaterThanZero_When_CreatePermissionType()
        {
            var command = new CreatePermissionTypeCommand
            {
                Description = "Administrator",
            };

            var permissionType = await SendAsync(command);
             

            var permissionTypeEntity = await FindAsync<PermissionTypeEntity>(permissionType);

            permissionTypeEntity.Should().NotBeNull();
            permissionTypeEntity!.Id.Should().Be(permissionType);
            permissionTypeEntity.Description.Should().Be(command.Description); 
        }
        

    }
}
