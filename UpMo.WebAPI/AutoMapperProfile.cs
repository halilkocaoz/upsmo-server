using AutoMapper;
using UpMo.Common.DTO.Request.Organization;
using UpMo.Common.DTO.Request.Monitor;
using UpMo.Common.DTO.Request.Auth;
using UpMo.Common.DTO.Response;
using UpMo.Common.DTO.Response.Organization;
using UpMo.Common.DTO.Response.Monitor;
using UpMo.Common.Monitor;
using UpMo.Entities;
using System;

namespace UpMo.WebAPI
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region DTO > Entity
            CreateMap<SignUpRequest, AppUser>();

            CreateMap<OrganizationRequest, Organization>().ForMember(organization => organization.FounderUserID, opts =>
            {
                opts.MapFrom(x => x.AuthenticatedUserID);
            });

            CreateMap<ManagerCreateRequest, Manager>();
            CreateMap<ManagerUpdateRequest, Manager>();

            CreateMap<MonitorCreateRequest, Monitor>().ForMember(monitor => monitor.PostForms, opts =>
            {
                opts.Condition(monitorCreateRequet => monitorCreateRequet.Method == MonitorMethodType.POST);
            }).ForMember(monitor => monitor.Region, opts =>
            {
                opts.MapFrom(monitorCreateRequest => Enum.GetName(monitorCreateRequest.Region));
            });

            CreateMap<MonitorUpdateRequest, Monitor>().ForMember(monitor => monitor.PostForms, opts =>
            {
                opts.Condition(monitorUpdateRequest => monitorUpdateRequest.Method == MonitorMethodType.POST);
            }).ForMember(monitor => monitor.Region, opts =>
            {
                opts.MapFrom(monitorUpdateRequest => Enum.GetName(monitorUpdateRequest.Region));
            });

            CreateMap<PostFormRequest, PostForm>();
            CreateMap<HeaderRequest, Header>();
            #endregion

            #region Entity > DTO
            CreateMap<Organization, OrganizationResponse>();
            CreateMap<Manager, ManagerResponse>();

            CreateMap<AppUser, AppUserResponse>();

            CreateMap<Monitor, MonitorResponse>();
            CreateMap<PostForm, PostFormResponse>();
            CreateMap<Header, HeaderResponse>();

            #endregion
        }
    }
}