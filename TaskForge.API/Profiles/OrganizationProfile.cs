using AutoMapper;
using TaskForge.Api.DTOs;
using TaskForge.Domain.Entities;

namespace TaskForge.Api.Profiles
{
    public class OrganizationProfile : Profile
    {
        public OrganizationProfile()
        {
            CreateMap<Organization, OrganizationResponseDto>();
            CreateMap<CreateOrganizationDto, Organization>();
            CreateMap<UpdateOrganizationDto, Organization>();
        }
    }
}
