using ABSD.Application.ViewModels;
using ABSD.Data.Entities;
using AutoMapper;

namespace ABSD.Application.Mapper
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			MappingEntityToViewModel();
			MappingDtoToEntity();
		}
		private void MappingDtoToEntity()
		{
			CreateMap<ServiceViewModel, Service>();
			CreateMap<ServiceTypeViewModel, ServiceType>();
			CreateMap<ParticipationViewModel, Participation>();
			CreateMap<ContactViewModel, Contact>();
			CreateMap<AppRoleViewModel, AppRole>();
		}
		private void MappingEntityToViewModel()
		{
			CreateMap<Service, ServiceViewModel>();
			CreateMap<Participation, ParticipationViewModel>();
			CreateMap<Contact, ContactViewModel>();
			CreateMap<ServiceType, ServiceTypeViewModel>();

			CreateMap<AppRole, AppRoleViewModel>();

            CreateMap<OrganizationViewModel, Organization>();
            CreateMap<Organization, OrganizationViewModel>();

            CreateMap<RoleOrganization, RoleOrganizationViewModel>();
            CreateMap<RoleOrganizationViewModel, RoleOrganization>();

            CreateMap<ServiceOrganization, ServiceOrganizationViewModel>();
            CreateMap<ServiceOrganizationViewModel, ServiceOrganization>();

            CreateMap<Premise, PremiseViewModel>();
            CreateMap<PremiseViewModel, Premise>();

            CreateMap<ServicePremises, ServicePremiseViewModel>();
            CreateMap<ServicePremiseViewModel, ServicePremises>();

            CreateMap<Participation, ParticipationViewModel>();
            CreateMap<ParticipationViewModel, Participation>();

            CreateMap<Contact, ContactViewModel>();
            CreateMap<ContactViewModel, Contact>();

            CreateMap<ServiceType, ServiceTypeViewModel>();
            CreateMap<ServiceTypeViewModel, ServiceType>();

            CreateMap<Funding, FundingViewModel>();
            CreateMap<FundingViewModel, Funding>();

        }
    }
}
		
	