using AutoMapper;
using N5Permission.Application.Common.Mappings;
using N5Permission.Domain.AggregatesModel.PermissionAggregate;

namespace N5Permission.Application.Permission.Dto
{
    public class PermissionChangeLog : IMapFrom<PermissionEntity>
    {
        public string LogId { get; set; }
        public int Id { get; set; }
        public string EmployeeForename { get; set; }
        public string EmployeeSurname { get; set; }
        public int PermisionTypeId { get; set; }
        public string CreationUser { get; set; }
        public DateTime CreationDate { get; set; }
        public string ModificationUser { get; set; }
        public DateTime? ModificationDate { get; set; }
        public bool Status { get; set; }
        public string SourceContext { get; set; }
        public bool NewEntity { get; set; } = false;
        public bool ModifiedEntity { get; set; } = false;
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public void Mapping(Profile profile)
        {
            profile.CreateMap<PermissionEntity, PermissionChangeLog>()
                .ForMember(d => d.LogId, opt => Guid.NewGuid().ToString())
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.EmployeeForename, opt => opt.MapFrom(s => s.EmployeeForename))
                .ForMember(d => d.EmployeeSurname, opt => opt.MapFrom(s => s.EmployeeSurname))
                .ForMember(d => d.PermisionTypeId, opt => opt.MapFrom(s => s.PermisionTypeId))
                .ForMember(d => d.CreationUser, opt => opt.MapFrom(s => s.CreationUser))
                .ForMember(d => d.CreationDate, opt => opt.MapFrom(s => s.CreationDate))
                .ForMember(d => d.ModificationUser, opt => opt.MapFrom(s => s.ModificationUser))
                .ForMember(d => d.ModificationDate, opt => opt.MapFrom(s => s.ModificationDate))
                .ForMember(d => d.Status, opt => opt.MapFrom(s => s.Status));

        }
    }

}
