using NUnit.Framework;
using FluentAssertions;
using static N5Permission.IntegrationTests.Testing;
using N5Permission.Domain.AggregatesModel.PermissionAggregate;
using N5Permission.Application.Common;
using N5Permission.Application.Permission.Queries.GetPermissionTypes;

namespace N5Permission.IntegrationTests.Application.Commands.Permission
{

    public class GetPermissionTypesQueryHandlerTest : BaseTestFixture
    {  
        [Test]
        public async Task Handle_Return_AllPermissionType()
        { 
            await AddAsync(new PermissionTypeEntity
            {
                Description = "Administrator",
                CreationUser = "SYSTEM",
                CreationDate = Constants.GetCustomDateTimeNow()
                
            });

            var query = new GetPermissionTypesQuery();

            var permissions = await SendAsync(query);
               
            permissions.Should().HaveCountGreaterThan(0); 
        }
        

    }
}
