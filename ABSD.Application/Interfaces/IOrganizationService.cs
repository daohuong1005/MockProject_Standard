using ABSD.Application.ViewModels;
using ABSD.Common.Paging;
using System.Collections.Generic;

namespace ABSD.Application.Interfaces
{
    public interface IOrganizationService
    {
        PagedResult<OrganizationViewModel> GetOrganizationWithPaging(int serviceId,int? page);

        bool UpdateRoleOrganization(int organizationId, int[] roleIds);

        List<AppRoleViewModel> GetRoleByOrganization(int organizationId);
    }
}