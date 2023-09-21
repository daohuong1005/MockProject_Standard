using ABSD.Data.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABSD.Data.EF
{
    public class DbInitializer
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        public DbInitializer(AppDbContext context,
            UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task Seed()
        {
            if (!_roleManager.Roles.Any())
            {
                await _roleManager.CreateAsync(new AppRole()
                {
                    Name = "Admin",
                    NormalizedName = "Admin",
                });
                await _roleManager.CreateAsync(new AppRole()
                {
                    Name = "Funder",
                    NormalizedName = "Funder",
                });
                await _roleManager.CreateAsync(new AppRole()
                {
                    Name = "Funder",
                    NormalizedName = "Funder",
                });
                await _roleManager.CreateAsync(new AppRole()
                {
                    Name = "Lead",
                    NormalizedName = "Lead",
                });
                await _roleManager.CreateAsync(new AppRole()
                {
                    Name = "Delivery",
                    NormalizedName = "Delivery",
                });
                await _roleManager.CreateAsync(new AppRole()
                {
                    Name = "Auditor",
                    NormalizedName = "Auditor",
                });
            }

            if (!_userManager.Users.Any())
            {
                await _userManager.CreateAsync(new AppUser()
                {
                    UserName = "admin",
                    //FullName = "Administrator",
                    Email = "admin@gmail.com"
                }, "123456@");

                var user = await _userManager.FindByNameAsync("admin");
                if (user != null)
                {
                    await _userManager.AddToRoleAsync(user, "Admin");
                }
            }

            if (!_context.Contracts.Any())
            {
                await _context.Contracts.AddRangeAsync(new List<Contract>()
                {
                    new Contract(){ ContractName = "Contract Name 1" },
                });

                await _context.SaveChangesAsync();
            }

            if (!_context.Accreditations.Any())
            {
                await _context.Accreditations.AddRangeAsync(new List<Accreditation>()
                {
                    new Accreditation(){Name = "Accreditation 1"},
                    new Accreditation(){Name = "Accreditation 2"},
                    new Accreditation(){Name = "Accreditation 3"}
                });

                await _context.SaveChangesAsync();
            }

            if (!_context.Premises.Any())
            {
                await _context.Premises.AddRangeAsync(new List<Premise>()
                {
                    new Premise(){LocationName = "Location HuongDX", AddressLine = "Hai Duong",PhoneNumber = "0971928933",IsActive = true},
                    new Premise(){LocationName = "Location NamNH95", AddressLine = "Ha Noi",PhoneNumber = "0971928923",IsActive = true},
                    new Premise(){LocationName = "Location ThongCT", AddressLine = "Thanh Hoa",PhoneNumber = "0928928933",IsActive = true},
                    new Premise(){LocationName = "Location TuBD", AddressLine = "Hai Phong",PhoneNumber = "0971928984",IsActive = true}
                });
                await _context.SaveChangesAsync();
            }

            if (!_context.Organizations.Any())
            {
                await _context.Organizations.AddRangeAsync(new List<Organization>()
                {
                    new Organization(){OrgName = "Org HuongDX", ShortDescription = "Huong cao dang"},
                    new Organization(){OrgName = "Org NamNH95", ShortDescription = "Nam hoc vien ki thuat quan su"},
                    new Organization(){OrgName = "Org ThongCT", ShortDescription = "Thong buu chinh vien thong"},
                    new Organization(){OrgName = "Org TuBD", ShortDescription = "Tu mo"}
                });
                await _context.SaveChangesAsync();
            }
            if (!_context.ClientSupports.Any())
            {
                await _context.ClientSupports.AddRangeAsync(new List<ClientSupport>()
                {
                    new ClientSupport(){ Name = "ClientSupport 1", Type =1 },
                    new ClientSupport(){ Name = "ClientSupport 2",Type =2 },
                    new ClientSupport(){ Name = "ClientSupport 3",Type =2 },
                });

                await _context.SaveChangesAsync();
            }

            if (!_context.RoleOrganizations.Any())
            {
                await _context.RoleOrganizations.AddRangeAsync(new List<RoleOrganization>()
                {
                    new RoleOrganization(){ OrganizationId = 1, RoleId = 2},
                    new RoleOrganization(){ OrganizationId = 1, RoleId = 3},
                    new RoleOrganization(){ OrganizationId = 2, RoleId = 3},
                    new RoleOrganization(){ OrganizationId = 2, RoleId = 4},
                    new RoleOrganization(){ OrganizationId = 3, RoleId = 4},
                    new RoleOrganization(){ OrganizationId = 4, RoleId = 1}
                });
                await _context.SaveChangesAsync();
            }

            if (!_context.Contents.Any())
            {
                await _context.Contents.AddRangeAsync(new List<Content>()
                {
                    new Content(){ContentName = "ContentName1", Type = 1 },
                    new Content(){ContentName = "ContentName1", Type = 1 },
                    new Content(){ContentName = "ContentName3", Type = 1 },
                });

                await _context.SaveChangesAsync();
            }

            if (!_context.Countries.Any())
            {
                await _context.Countries.AddRangeAsync(new List<Country>()
                {
                    new Country(){CountryName = "Litva", IsActive = true},
                    new Country(){CountryName = "United Kingdom", IsActive = true},
                    new Country(){CountryName = "Latvita", IsActive = true},
                    new Country(){CountryName = "Viet Nam", IsActive = true}
                });

                await _context.SaveChangesAsync();
            }

            if (!_context.Criterions.Any())
            {
                await _context.Criterions.AddRangeAsync(new List<Criterion>()
                {
                    new Criterion(){ Name = "Disability Living Allowance 1", Type = 1 },
                    new Criterion(){ Name = "Disability Living Allowance 2", Type = 2 },
                    new Criterion(){ Name = "Disability Living Allowance 3", Type = 2 },
                });

                await _context.SaveChangesAsync();
            }

            if (!_context.Interventions.Any())
            {
                await _context.Interventions.AddRangeAsync(new List<Intervention>()
                {
                    new Intervention(){ InterventionName = "Intervention 1" },
                    new Intervention(){ InterventionName = "Intervention 2" },
                    new Intervention(){ InterventionName = "Intervention 3" },
                });
                await _context.SaveChangesAsync();
            }

            if (!_context.Regions.Any())
            {
                await _context.Regions.AddRangeAsync(new List<TrustRegion>()
                {
                    new TrustRegion(){RegionName = "Region Name 1", Description = "Region 1", IsActive = true, CountryId = 1},
                    new TrustRegion(){RegionName = "Region Name 2", Description = "Region 2", IsActive = false, CountryId = 2},
                    new TrustRegion(){RegionName = "Region Name 3", Description = "Region 3", IsActive = true, CountryId = 3},
                    new TrustRegion(){RegionName = "Region Name 4", Description = "Region 4", IsActive = true, CountryId = 4},
                    new TrustRegion(){RegionName = "Region Name 5", Description = "Region 5", IsActive = true, CountryId = 4}
                });

                await _context.SaveChangesAsync();
            }

            if (!_context.Participations.Any())
            {
                await _context.Participations.AddRangeAsync(new List<Participation>()
                {
                    new Participation(){ ParticipationName = "Participation 1" },
                    new Participation(){ ParticipationName = "Participation 2" },
                    new Participation(){ ParticipationName = "Participation 3" },
                });
                await _context.SaveChangesAsync();
            }

            if (!_context.Districts.Any())
            {
                await _context.Districts.AddRangeAsync(new List<TrustDistrict>()
                {
                    new TrustDistrict(){DistrictName = "Region Name 1", Description = "Region 1", IsActive = true, TrustRegionId = 1},
                    new TrustDistrict(){DistrictName = "Region Name 2", Description = "Region 2", IsActive = true, TrustRegionId = 2},
                    new TrustDistrict(){DistrictName = "Region Name 3", Description = "Region 3", IsActive = false, TrustRegionId = 3},
                    new TrustDistrict(){DistrictName = "Region Name 4", Description = "Region 4", IsActive = false, TrustRegionId = 4},
                    new TrustDistrict(){DistrictName = "Region Name 5", Description = "Region 5", IsActive = true, TrustRegionId = 5}
                });
                await _context.SaveChangesAsync();
            }

            if (!_context.ServiceTypes.Any())
            {
                await _context.ServiceTypes.AddRangeAsync(new List<ServiceType>()
                {
                    new ServiceType { Name="Service Type 1"},
                    new ServiceType { Name="Service Type 2"},
                    new ServiceType { Name="Service Type 3"},
                    new ServiceType { Name="Service Type 4"},
                });
                await _context.SaveChangesAsync();
            }

            if (!_context.Contacts.Any())
            {
                await _context.Contacts.AddRangeAsync(new List<Contact>()
                {
                    new Contact
                    {
                        FirstName="Chu Trong",
                        SurName="Thong",
                        MobilePhone="0987356225",
                        Email="Demo1@gmail.com",
                        ContactType="Fax"
                    },
                      new Contact
                    {
                        FirstName="Dao Xuan",
                        SurName="Huong",
                        MobilePhone="0987378791",
                        Email="Demo2@gmail.com",
                        ContactType="Phone"
                    }
                });
                await _context.SaveChangesAsync();
            }

            if (!_context.Fundings.Any())
            {
                await _context.Fundings.AddRangeAsync(new List<Funding>()
                {
                    new Funding
                    {
                        FundingSource=1,
                        ContactId=1,
                        FundingAmount=1000,
                        FundingStart=DateTime.Now,
                        FundingEnd=DateTime.Now,
                        FundraisingForText="Text 1",
                        FundraisingWhy="Text 1",
                        FundraisingDonorAnonymous=true,
                        FundraisingDonorAmount=1000,
                        FundingNeeds="Need 1",
                        FundingContinuationNeeded=true,
                        FundingContinuationAmount=1000,
                        FundingContinuationDetails="Details 1",
                        FundraisingNeeded="Need 1",
                        FundraisingRequiredBy=DateTime.Now,
                        FundraisingComplete=true,
                        FundraisingCompleteDate=DateTime.Now,
                        FundraisingDonationDate=DateTime.Now,
                        FundraisingDonationIncremental=true
                    },
                     new Funding
                    {
                        FundingSource=1,
                        ContactId=2,
                        FundingAmount=1000,
                        FundingStart=DateTime.Now,
                        FundingEnd=DateTime.Now,
                        FundraisingForText="Text 2",
                        FundraisingWhy="Text 2",
                        FundraisingDonorAnonymous=true,
                        FundraisingDonorAmount=1000,
                        FundingNeeds="Need 2",
                        FundingContinuationNeeded=true,
                        FundingContinuationAmount=2000,
                        FundingContinuationDetails="Details 2",
                        FundraisingNeeded="Need 2",
                        FundraisingRequiredBy=DateTime.Now,
                        FundraisingComplete=true,
                        FundraisingCompleteDate=DateTime.Now,
                        FundraisingDonationDate=DateTime.Now,
                        FundraisingDonationIncremental=true
                    },
                });

                await _context.SaveChangesAsync();
            }

            if (!_context.Services.Any())
            {
                await _context.Services.AddRangeAsync(new List<Service>()
                {
                    new Service
                    {
                        ServiceName="ServiceName 1",
                        ServiceShortDescription="Short Description 1",
                        FundingId=1,
                        ContactId=1,
                        ContractId=null,
                        ClientDescription="ClientDescription 1",
                        ParticipationId=1,
                        ServiceStartExpected=DateTime.Now,
                        ServiceStartDate=DateTime.Now,
                        ServiceEndDate=DateTime.Now,
                        ServiceExtendable=DateTime.Now,
                        ServiceActive=true,
                        ServiceFullDescription="Service Full Description 1",
                        DeptCode="Code 1",
                        ServiceTypeId=1,
                        ServiceDesscriptionDelivery="Delivery 1",
                        ServiceContactCode="Code 1",
                        ServiceContractValue=1000,
                        ContractStagedPayment=true,
                        ReferralProcess="Referral 1",
                        ServiceTimeLimited=DateTime.Now,
                        ProgrammeId=1,
                    },
                     new Service
                    {
                        ServiceName="ServiceName 2",
                        ServiceShortDescription="Short Description 2",
                        FundingId=1,
                        ContactId=1,
                        ContractId=null,
                        ClientDescription="ClientDescription 2",
                        ParticipationId=1,
                        ServiceStartExpected=DateTime.Now,
                        ServiceStartDate=DateTime.Now,
                        ServiceEndDate=DateTime.Now,
                        ServiceExtendable=DateTime.Now,
                        ServiceActive=true,
                        ServiceFullDescription="Service Full Description 2",
                        DeptCode="Code 2",
                        ServiceTypeId=1,
                        ServiceDesscriptionDelivery="Delivery 2",
                        ServiceContactCode="Code 2",
                        ServiceContractValue=2000,
                        ContractStagedPayment=true,
                        ReferralProcess="Referral 2",
                        ServiceTimeLimited=DateTime.Now,
                        ProgrammeId=1
                    },
                });
                await _context.SaveChangesAsync();
            }

            if (!_context.ServiceOrganizations.Any())
            {
                await _context.ServiceOrganizations.AddRangeAsync(new List<ServiceOrganization>()
                {
                    new ServiceOrganization(){ OrganizationId = 1, ServiceId = 1},
                    new ServiceOrganization(){ OrganizationId = 2, ServiceId = 1},
                    new ServiceOrganization(){ OrganizationId = 3, ServiceId = 1},
                    new ServiceOrganization(){ OrganizationId = 2, ServiceId = 2},
                    new ServiceOrganization(){ OrganizationId = 3, ServiceId = 2},
                    new ServiceOrganization(){ OrganizationId = 4, ServiceId = 1}
                });
                await _context.SaveChangesAsync();
            }
            if (!_context.ServicePremises.Any())
            {
                await _context.ServicePremises.AddRangeAsync(new List<ServicePremises>()
                {
                    new ServicePremises(){ PremiseId = 1, ServiceId = 1,ProjectCode = "adaSa"},
                    new ServicePremises(){ PremiseId = 2, ServiceId = 1},
                    new ServicePremises(){ PremiseId = 3, ServiceId = 1},
                    new ServicePremises(){ PremiseId = 2, ServiceId = 2},
                    new ServicePremises(){ PremiseId = 3, ServiceId = 2},
                    new ServicePremises(){ PremiseId = 4, ServiceId = 1}
                });
                await _context.SaveChangesAsync();
            }
        }
    }
}