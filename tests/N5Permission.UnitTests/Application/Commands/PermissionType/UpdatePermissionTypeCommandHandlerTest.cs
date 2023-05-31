using N5Permission.Application.Permission.Commands.UpdatePermissionType;
using N5Permission.Application.Permission.Dto;
using Nest;
using System.Linq.Expressions;

namespace N5Permission.UnitTests.Application.Commands.PermissionType
{

    public class UpdatePermissionTypeCommandHandlerTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IElasticClient> _elasticClientMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IPermissionTypeRepository> _permissionRepositoryMock;
        public UpdatePermissionTypeCommandHandlerTest()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _elasticClientMock = new Mock<IElasticClient>();
            _mapperMock = new Mock<IMapper>();
            _permissionRepositoryMock = new Mock<IPermissionTypeRepository>();
        }

        [Fact]
        public async Task Handle_ReturnGreaterThanZero_When_PermissionType_IsSuccessfullyUpdated()
        {
            // Arrange 
            var cancellation = new CancellationToken();
            var createPermissionCommand = MockData.GetUpdatePermissionTypeCommandMock();
            _permissionRepositoryMock.Setup(rep => rep.FirstOrDefaultAsync(It.IsAny<Expression<Func<PermissionTypeEntity, bool>>>())).Returns(Task.FromResult(new PermissionTypeEntity { Id = createPermissionCommand?.Id ?? 1 }));
            _unitOfWorkMock.Setup(uow => uow.Repository<IPermissionTypeRepository>()).Returns(_permissionRepositoryMock.Object);
            _unitOfWorkMock.Setup(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>())).Returns(Task.FromResult(1));
            _mapperMock.Setup(map => map.Map<PermissionEntity, PermissionChangeLog>(It.IsAny<PermissionEntity>())).Returns(new PermissionChangeLog());

            //Act 
            var handler = new UpdatePermissionTypeCommandHandler(_unitOfWorkMock.Object, _elasticClientMock.Object, _mapperMock.Object);
            var result = await handler.Handle(createPermissionCommand, cancellation);

            //Assert
            Assert.True(result == createPermissionCommand.Id);
        }

        [Fact]
        public async Task Handle_ReturnNotFoundException_When_PermissionType_IsNotValid()
        {
            // Arrange 
            var cancellation = new CancellationToken();
            var createPermissionCommand = MockData.GetUpdatePermissionTypeCommandMock();
            _unitOfWorkMock.Setup(uow => uow.Repository<IPermissionTypeRepository>()).Returns(_permissionRepositoryMock.Object);

            //Act 
            var handler = new UpdatePermissionTypeCommandHandler(_unitOfWorkMock.Object, _elasticClientMock.Object, _mapperMock.Object);
            Func<Task> updatePermissionTask = () => handler.Handle(createPermissionCommand, cancellation);

            //Assert
            Assert.ThrowsAsync<NotFoundException>(updatePermissionTask);
        }

    }
}
