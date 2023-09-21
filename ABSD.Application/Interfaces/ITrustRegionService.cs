using ABSD.Application.ViewModels;
using ABSD.Common.Paging;

namespace ABSD.Application.Interfaces
{
    public interface ITrustRegionService
    {
        PagedResult<TrustRegionViewModel> GetTrustRegionsWithPaging(int? page, string firstCharacterToFilter = "", bool includeInActive = false);

        TrustRegionViewModel GetTrustRegionDetail(int regionId);

        TrustRegionViewModel GetRegion(int regionId);

        int ActiveTrustRegion(int regionId);

        int InActiveTrustRegion(int regionId);

        bool CheckExistedRegionName(string regionName);

        int CreateTrustRegion(TrustRegionViewModel regionViewModel);

        int UpdateTrustRegion(TrustRegionViewModel regionViewModel);
    }
}