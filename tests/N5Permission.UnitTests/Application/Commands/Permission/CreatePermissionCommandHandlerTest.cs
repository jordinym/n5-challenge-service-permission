using MediatR;
using Microsoft.Extensions.Logging;
using N5Permission.Application.Permission.Commands.CreatePermission;
using N5Permission.Application.Permission.Dto;
using Nest;

namespace N5Permission.UnitTests.Application.Commands.Permission
{

    public class CreatePermissionCommandHandlerTest
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<ILogger<CreatePermissionCommandHandler>> _loggerMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IElasticClient> _elasticClientMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IPermissionRepository> _permissionRepositoryMock;
        public CreatePermissionCommandHandlerTest()
        {
            _mediatorMock = new Mock<IMediator>();
            _loggerMock = new Mock<ILogger<CreatePermissionCommandHandler>>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _elasticClientMock = new Mock<IElasticClient>();
            _mapperMock = new Mock<IMapper>();
            _permissionRepositoryMock = new Mock<IPermissionRepository>();
        }

        [Fact]
        public async Task Handle_ReturnGreaterThanZero_When_Permission_IsSuccessfullyRegistered()
        {
            // Arrange 
            var createPermissionCommand = new CreatePermissionCommand
            {
                EmployeeForename = "Jordiny",
                EmployeeSurname = "Montanez",
                PermisionTypeId = 1
            };
            var cancellation = new CancellationToken();
            _unitOfWorkMock.Setup(uow => uow.Repository<IPermissionRepository>()).Returns(_permissionRepositoryMock.Object);
            _unitOfWorkMock.Setup(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>())).Returns(Task.FromResult(1));
            _mapperMock.Setup(map => map.Map<PermissionEntity, PermissionChangeLog>(It.IsAny<PermissionEntity>())).Returns(new PermissionChangeLog());

            //Act 
            var handler = new CreatePermissionCommandHandler(_unitOfWorkMock.Object, _elasticClientMock.Object, _mapperMock.Object);
            var result = await handler.Handle(createPermissionCommand, cancellation);

            //Assert
            Assert.NotNull(result);
        }

    }
}
