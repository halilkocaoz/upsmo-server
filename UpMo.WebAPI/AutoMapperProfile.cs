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
            CreateMap<OrganizationUpdateRequest, Organization>();

            CreateMap<OrganizationManagerCreateRequest, OrganizationManager>();
            CreateMap<OrganizationManagerUpdateRequest, OrganizationManager>();

            CreateMap<MonitorCreateRequest, Monitor>();
            CreateMap<MonitorUpdateRequest, Monitor>();

            CreateMap<PostFormDataCreateRequest, PostFormData>();
            #endregion
            
            #region Entity > DTO
            CreateMap<Organization, OrganizationResponse>();
            CreateMap<OrganizationManager, OrganizationManagerResponse>();
            CreateMap<AppUser, AppUserResponse>();
            CreateMap<Monitor, MonitorResponse>();
            CreateMap<PostFormData, PostFormDataResponse>();
            #endregion
        }
    }
}