namespace N5Permission.UnitTests.Application.Commands.PermissionType
{

    public class GetPermissionTypesQueryHandlerTest
    {   
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IPermissionTypeRepository> _permissionRepositoryMock;
        public GetPermissionTypesQueryHandlerTest()
        {   
            _mapperMock = new Mock<IMapper>();
            _permissionRepositoryMock = new Mock<IPermissionTypeRepository>();
        }

        [Fact]
        public async Task Handle_Return_AllPermissionType()
        {
            // Arrange 
            var permissionId = 2;
            var fakeDynamicResult = Enumerable.Empty<PermissionTypeEntity>();
            var permissionsResponse = new List<GetPermissionTypesQueryResponse>
            {
                new GetPermissionTypesQueryResponse {Id=permissionId}
            };

            var cancellation = new CancellationToken();
            _permissionRepositoryMock.Setup(uow => uow.GetPermissionTypes()).Returns(Task.FromResult(fakeDynamicResult.ToList()));
            _mapperMock.Setup(map => map.Map<List<PermissionTypeEntity>, List<GetPermissionTypesQueryResponse>>(It.IsAny<List<PermissionTypeEntity>>())).Returns(permissionsResponse);


            //Act 
            var handler = new GetPermissionTypesQueryHandler(_permissionRepositoryMock.Object, _mapperMock.Object);
            var result = await handler.Handle(It.IsAny<GetPermissionTypesQuery>(), cancellation);

            //Assert
            Assert.True(result.FirstOrDefault().Id== permissionId);
        }

    }
}
