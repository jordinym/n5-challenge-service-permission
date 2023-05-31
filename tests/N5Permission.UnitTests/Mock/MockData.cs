using N5Permission.Application.Permission.Commands.UpdatePermission;
using N5Permission.Application.Permission.Commands.UpdatePermissionType;

namespace N5Permission.UnitTests.Mock
{
    public static class MockData
    {
        public static UpdatePermissionCommand GetUpdatePermissionCommandMock()
        {
            return new UpdatePermissionCommand
            {
                EmployeeForename = "Jordiny",
                EmployeeSurname = "Montanez",
                PermisionTypeId = 1,
                Id = 4
            };
        }
        public static UpdatePermissionTypeCommand GetUpdatePermissionTypeCommandMock()
        {
            return new UpdatePermissionTypeCommand
            {
                Description="Administrator",
                Id = 4
            };
        }
    }
}
