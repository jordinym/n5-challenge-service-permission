namespace N5Permission.UnitTests.Application.Commands.PermissionType
{

    public class GetPermissionsQueryHandlerTest
    {   
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IPermissionRepository> _permissionRepositoryMock;
        public GetPermissionsQueryHandlerTest()
        {   
            _mapperMock = new Mock<IMapper>();
            _permissionRepositoryMock = new Mock<IPermissionRepository>();
        }

        [Fact]
        public async Task Handle_Return_AllPermission_AndPermissionType()
        {
            // Arrange 
            var permissionId = 5;
            var fakeDynamicResult = Enumerable.Empty<PermissionEntity>();
            var permissionsResponse = new List<GetPermissionsQueryResponse>
            {
                new GetPermissionsQueryResponse {Id=permissionId}
            };

            var cancellation = new CancellationToken();
            _permissionRepositoryMock.Setup(uow => uow.GetPermissions()).Returns(Task.FromResult(fakeDynamicResult.ToList()));
            _mapperMock.Setup(map => map.Map<List<PermissionEntity>, List<GetPermissionsQueryResponse>>(It.IsAny<List<PermissionEntity>>())).Returns(permissionsResponse);


            //Act 
            var handler = new GetPermissionsQueryHandler(_permissionRepositoryMock.Object, _mapperMock.Object);
            var result = await handler.Handle(It.IsAny<GetPermissionsQuery>(), cancellation);

            //Assert
            Assert.True(result.FirstOrDefault().Id== permissionId);
        }

    }
}
