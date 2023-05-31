
using N5Permission.Application.Permission.Commands.UpdatePermission;
using N5Permission.Application.Permission.Dto;
using Nest;
using System.Linq.Expressions;

namespace N5Permission.UnitTests.Application.Commands.Permission
{

    public class UpdatePermissionCommandHandlerTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IElasticClient> _elasticClientMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IPermissionRepository> _permissionRepositoryMock;
        public UpdatePermissionCommandHandlerTest()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _elasticClientMock = new Mock<IElasticClient>();
            _mapperMock = new Mock<IMapper>();
            _permissionRepositoryMock = new Mock<IPermissionRepository>();
        }

        [Fact]
        public async Task Handle_ReturnGreaterThanZero_When_Permission_IsSuccessfullyUpdated()
        {
            // Arrange 
            var cancellation = new CancellationToken();
            var createPermissionCommand = MockData.GetUpdatePermissionCommandMock();
            _permissionRepositoryMock.Setup(rep => rep.FirstOrDefaultAsync(It.IsAny<Expression<Func<PermissionEntity, bool>>>())).Returns(Task.FromResult(new PermissionEntity { Id = createPermissionCommand?.Id ?? 1 }));
            _unitOfWorkMock.Setup(uow => uow.Repository<IPermissionRepository>()).Returns(_permissionRepositoryMock.Object);
            _unitOfWorkMock.Setup(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>())).Returns(Task.FromResult(1));
            _mapperMock.Setup(map => map.Map<PermissionEntity, PermissionChangeLog>(It.IsAny<PermissionEntity>())).Returns(new PermissionChangeLog());

            //Act 
            var handler = new UpdatePermissionCommandHandler(_unitOfWorkMock.Object, _elasticClientMock.Object, _mapperMock.Object);
            var result = await handler.Handle(createPermissionCommand, cancellation);

            //Assert
            Assert.True(result == createPermissionCommand.Id);
        }

        [Fact]
        public async Task Handle_ReturnNotFoundException_When_Permission_IsNotValid()
        {
            // Arrange 
            var cancellation = new CancellationToken();
            var createPermissionCommand = MockData.GetUpdatePermissionCommandMock();
            _unitOfWorkMock.Setup(uow => uow.Repository<IPermissionRepository>()).Returns(_permissionRepositoryMock.Object);

            //Act 
            var handler = new UpdatePermissionCommandHandler(_unitOfWorkMock.Object, _elasticClientMock.Object, _mapperMock.Object);
            Func<Task> updatePermissionTask = () => handler.Handle(createPermissionCommand, cancellation);

            //Assert
            Assert.ThrowsAsync<NotFoundException>(updatePermissionTask);
        }

    }
}
