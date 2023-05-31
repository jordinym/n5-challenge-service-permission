using N5Permission.Application.Permission.Commands.CreatePermissionType;
using Nest;

namespace N5Permission.UnitTests.Application.Commands.PermissionType
{

    public class CreatePermissionTypeCommandHandlerTest
    { 
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IElasticClient> _elasticClientMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IPermissionTypeRepository> _permissionRepositoryMock;
        public CreatePermissionTypeCommandHandlerTest()
        { 
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _elasticClientMock = new Mock<IElasticClient>();
            _mapperMock = new Mock<IMapper>();
            _permissionRepositoryMock = new Mock<IPermissionTypeRepository>();
        }

        [Fact]
        public async Task Handle_ReturnGreaterThanZero_When_PermissionType_IsSuccessfullyRegistered()
        {
            // Arrange 
            var createPermissionCommand = new CreatePermissionTypeCommand
            {
                Description= "Administrator",
            };
            var cancellation = new CancellationToken();
            _unitOfWorkMock.Setup(uow => uow.Repository<IPermissionTypeRepository>()).Returns(_permissionRepositoryMock.Object);
            _unitOfWorkMock.Setup(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>())).Returns(Task.FromResult(1)); 

            //Act 
            var handler = new CreatePermissionTypeCommandHandler(_unitOfWorkMock.Object, _elasticClientMock.Object, _mapperMock.Object);
            var result = await handler.Handle(createPermissionCommand, cancellation);

            //Assert
            Assert.NotNull(result);
        }

    }
}
