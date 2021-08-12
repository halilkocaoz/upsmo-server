using AutoMapper;
using UpMo.Common.DTO.Request;
using UpMo.Common.DTO.Response;
using UpMo.Entities;

namespace UpMo.WebAPI
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region DTO > Entity
            CreateMap<SignUpRequest, AppUser>();
            CreateMap<OrganizationCreateRequest, Organization>();
            #endregion
            
            #region Entity > DTO
            CreateMap<Organization, OrganizationResponse>();
            #endregion
        }
    }
}