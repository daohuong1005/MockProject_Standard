using ABSD.Application.Interfaces;
using ABSD.Application.ViewModels;
using ABSD.Common.Constants;
using ABSD.Common.Paging;
using ABSD.Data;
using ABSD.Data.Entities;
using ABSD.Data.IRepositories;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ABSD.Application.Implements
{
    public class OrganizationService : IOrganizationService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IOrganizationRepository organizationRepository;
        private readonly IRoleOrganizationRepository roleOrganizationRepository;
        private readonly IAppRoleRepository appRoleRepository;
        private readonly IMapper _mapper;

        public OrganizationService(IUnitOfWork unitOfWork, IOrganizationRepository organizationRepository, IMapper mapper,
            IAppRoleRepository appRoleRepository, IRoleOrganizationRepository roleOrganizationRepository)
        {
            this.unitOfWork = unitOfWork;
            this.organizationRepository = organizationRepository;
            this.roleOrganizationRepository = roleOrganizationRepository;
            this.appRoleRepository = appRoleRepository;
            _mapper = mapper;
        }

        public PagedResult<OrganizationViewModel> GetOrganizationWithPaging(int serviceId, int? page)
        {
            var query = organizationRepository.GetByServiceId(serviceId);

            int rowCount = query.Count();
            int pageCount = (int)Math.Ceiling((double)rowCount / Paging.PageSize);
            int currentPage = page.HasValue ? page.Value : 1;

            var organizations = query.OrderBy(x => x.OrgName)
                            .Skip((currentPage - 1) * Paging.PageSize)
                            .Take(Paging.PageSize)
                            .ToList();
            var organizationVM = _mapper.Map<List<OrganizationViewModel>>(organizations);

            string roleNames = string.Empty;

            foreach (var item in organizationVM)
            {
                roleNames = "";
                var roles = roleOrganizationRepository.GetMany(x => x.OrganizationId == item.Id, x => x.AppRole).ToList();
                int length = roles.Count() - 1;
                for (int i = 0; i <= length; i++)
                {
                    if (i == length)
                    {
                        roleNames += roles[i].AppRole.Name;
                        break;
                    }
                    roleNames += roles[i].AppRole.Name + ",";
                }
                item.RoleViewModels = roleNames;
            }

            return new PagedResult<OrganizationViewModel>()
            {
                PageCount = pageCount,
                RowCount = rowCount,
                CurrentPage = currentPage,
                Items = organizationVM
            };
        }

        public List<AppRoleViewModel> GetRoleByOrganization(int organizationId)
        {
            var roles = roleOrganizationRepository.GetMany(x => x.OrganizationId == organizationId, x => x.AppRole)
                                                 .Select(x => x.AppRole).ToList();

            var appRoleVMs = _mapper.Map<List<AppRoleViewModel>>(appRoleRepository.GetAll().ToList());
            foreach (var role in roles)
            {
                foreach (var appRoleVM in appRoleVMs)
                {
                    if (role.Id == appRoleVM.Id)
                        appRoleVM.IsChecked = true;
                }
            }

            return appRoleVMs;
        }

        public bool UpdateRoleOrganization(int organizationId, int[] roleIds)
        {
            var roleOrganizationOld = roleOrganizationRepository.GetMany(x => x.OrganizationId == organizationId).ToList();

            if (roleOrganizationOld.Count != 0)
                roleOrganizationRepository.RemoveRange(roleOrganizationOld);

            int length = roleIds.Length;
            if (length > 0)
            {
                for (int i = 0; i < length; i++)
                {
                    var roleOrganiztionNew = new RoleOrganization()
                    {
                        OrganizationId = organizationId,
                        RoleId = roleIds[i]
                    };
                    roleOrganizationRepository.Add(roleOrganiztionNew);
                }
                unitOfWork.Commit();
                return true;
            }

            return false;
        }
    }
}