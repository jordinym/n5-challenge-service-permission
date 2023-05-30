using AutoMapper;
using N5Permission.Application.Common.Mappings; 
using N5Permission.Domain.AggregatesModel.PermissionAggregate;

namespace N5Permission.Application.Permission.Queries.GetPermissionTypes
{
    public class GetPermissionTypesQueryResponse : IMapFrom<PermissionTypeEntity>
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string RegistrationDate { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<PermissionTypeEntity, GetPermissionTypesQueryResponse>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.Description, opt => opt.MapFrom(s => s.Description)) 
                .ForMember(d => d.RegistrationDate, opt => opt.MapFrom(s => s.RegistrationDate)) ;
        }
    }
}
