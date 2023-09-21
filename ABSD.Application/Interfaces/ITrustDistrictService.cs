using ABSD.Application.ViewModels;
using ABSD.Common.Paging;

namespace ABSD.Application.Interfaces
{
    public interface ITrustDistrictService
    {
        PagedResult<TrustDistrictViewModel> GetTrustDistrictsWithPaging(int regionId, int? page, string firstCharacterToFilter = "", bool includeInActive = false);

        TrustDistrictViewModel GetTrustDistrictDetail(int DistrictId);

        int ActiveTrustDistrict(int districtId);

        int InActiveTrustDistrict(int districtId);

        bool CheckExistedDistrictName(int regionId, string districtName);

        int CreateTrustDistrict(TrustDistrictViewModel districtViewModel);

        int UpdateTrustDistrict(TrustDistrictViewModel districtViewModel);
    }
}