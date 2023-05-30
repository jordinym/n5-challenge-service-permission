using N5Permission.Application.Permission.Commands.CreatePermission;
using N5Permission.Application.Permission.Commands.CreatePermissionType;
using N5Permission.Application.Permission.Commands.UpdatePermission;
using N5Permission.Application.Permission.Commands.UpdatePermissionType;
using N5Permission.Application.Permission.Queries.GetPermissionTypes;
using N5Permission.Application.Permission.Queries.GetPermissionWithPagination;

namespace N5Permission.Application.Common
{
    public class Constants
    {
        public struct StatusCode
        {
            public const int Ok = 200;
            public const int TechnicalError = -1;
            public const int FunctionalError = 1;
        }
        public struct DateFormat
        {
            public const string Date = "dd/MM/yyyy";
            public const string DateAndTime = "dd/MM/yyyy HH:mm";
            public const string SQLMinDate = "01/01/1900";
        }

        public static DateTime GetCustomDateTimeNow()
        {
            return DateTime.Now;
        }
        public static Dictionary<string, string> OperationNames = new Dictionary<string, string> {
            { nameof(CreatePermissionCommand),"request"},
            { nameof(UpdatePermissionCommand),"modify"},
            { nameof(GetPermissionsQuery),"get"},
            { nameof(CreatePermissionTypeCommand),"request"},
            { nameof(UpdatePermissionTypeCommand),"modify"},
            { nameof(GetPermissionTypesQuery),"get"},
        };

    }
}
