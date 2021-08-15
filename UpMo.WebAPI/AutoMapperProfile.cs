using AutoMapper;
using UpMo.Common.DTO.Request.Organization;
using UpMo.Common.DTO.Request.Monitor;
using UpMo.Common.DTO.Request.Auth;
using UpMo.Common.DTO.Response;
using UpMo.Common.DTO.Response.Organization;
using UpMo.Common.DTO.Response.Monitor;
using UpMo.Common.Monitor;
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

            CreateMap<ManagerCreateRequest, Manager>();
            CreateMap<ManagerUpdateRequest, Manager>();

            CreateMap<MonitorCreateRequest, Monitor>().ForMember(monitor => monitor.PostForms, opts =>
            {
                opts.Condition(monitorCreateRequet => monitorCreateRequet.Method == MonitorMethodType.POST);
            });
            CreateMap<MonitorUpdateRequest, Monitor>().ForMember(monitor => monitor.PostForms, opts =>
            {
                opts.Condition(monitorCreateRequet => monitorCreateRequet.Method == MonitorMethodType.POST);
            });;

            CreateMap<PostFormCreateRequest, PostForm>();
            CreateMap<PostFormUpdateRequest, PostForm>();
            #endregion

            #region Entity > DTO
            CreateMap<Organization, OrganizationResponse>();
            CreateMap<Manager, ManagerResponse>();

            CreateMap<AppUser, AppUserResponse>();
            
            CreateMap<Monitor, MonitorResponse>();
            CreateMap<PostForm, PostFormResponse>();
            #endregion
        }
    }
}