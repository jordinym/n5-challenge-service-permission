using AutoMapper;
using N5Permission.Application.Common.Mappings;
using N5Permission.Domain.AggregatesModel.PermissionAggregate;

namespace N5Permission.Application.Permission.Queries.GetPermissions
{
    public class GetPermissionsQueryResponse: IMapFrom<PermissionEntity>
    {
        public int Id { get; set; }
        public string EmployeeForename { get; set; }
        public string EmployeeSurname { get; set; }
        public int PermissionTypeId { get; set; }
        public string PermissionType { get; set; }
        public string RegistrationDate { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<PermissionEntity, GetPermissionsQueryResponse>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.EmployeeForename, opt => opt.MapFrom(s => s.EmployeeForename))
                .ForMember(d => d.EmployeeSurname, opt => opt.MapFrom(s => s.EmployeeSurname))
                .ForMember(d => d.RegistrationDate, opt => opt.MapFrom(s => s.RegistrationDate))
                .ForMember(d => d.PermissionTypeId, opt => opt.MapFrom(s => s.PermissionType.Id))
                .ForMember(d => d.PermissionType, opt => opt.MapFrom(s => s.PermissionType.Description));
        }
    }
}
