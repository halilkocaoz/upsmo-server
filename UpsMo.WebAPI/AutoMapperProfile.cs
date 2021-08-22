using AutoMapper;
using UpsMo.Common.DTO.Request.Organization;
using UpsMo.Common.DTO.Request.Monitor;
using UpsMo.Common.DTO.Request.Auth;
using UpsMo.Common.DTO.Response;
using UpsMo.Common.DTO.Response.Organization;
using UpsMo.Common.DTO.Response.Monitor;
using UpsMo.Common.Monitor;
using UpsMo.Entities;
using System;

namespace UpsMo.WebAPI
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
            })
            .ForMember(monitor => monitor.Method, opts =>
            {
                opts.MapFrom(monitorCreateRequest => Enum.GetName(monitorCreateRequest.Method));
            });

            CreateMap<MonitorUpdateRequest, Monitor>().ForMember(monitor => monitor.PostForms, opts =>
            {
                opts.Condition(monitorUpdateRequest => monitorUpdateRequest.Method == MonitorMethodType.POST);
            }).ForMember(monitor => monitor.Method, opts =>
            {
                opts.MapFrom(monitorUpdateRequest => Enum.GetName(monitorUpdateRequest.Method));
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