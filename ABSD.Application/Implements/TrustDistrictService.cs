using ABSD.Application.Interfaces;
using ABSD.Application.ViewModels;
using ABSD.Common.Constants;
using ABSD.Common.Paging;
using ABSD.Data;
using ABSD.Data.Entities;
using ABSD.Data.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ABSD.Application.Implements
{
    public class TrustDistrictService : ITrustDistrictService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ITrustDistrictRepository districtRepository;

        public TrustDistrictService(IUnitOfWork unitOfWork, ITrustDistrictRepository districtRepository)
        {
            this.unitOfWork = unitOfWork;
            this.districtRepository = districtRepository;
        }

        public PagedResult<TrustDistrictViewModel> GetTrustDistrictsWithPaging(int regionId, int? page, string firstCharacters = "", bool includeInActive = false)
        {
            var query = districtRepository.GetMany(x => x.TrustRegionId == regionId, x => x.Region);

            if (!includeInActive)
                query = query.Where(x => x.IsActive == true);

            if (!string.IsNullOrEmpty(firstCharacters))
            {
                query = query.Where(x => firstCharacters.Contains(x.DistrictName.Substring(0, 1)));
            }

            int rowCount = query.Count();
            int pageCount = (int)Math.Ceiling((double)rowCount / Paging.PageSize);
            int currentPage = page.HasValue ? page.Value : 1;

            var districts = query.OrderBy(x => x.DistrictName)
                            .Skip((currentPage - 1) * Paging.PageSize)
                            .Take(Paging.PageSize)
                            .ToList();

            var districtViewModels = new List<TrustDistrictViewModel>();

            foreach (var district in districts)
            {
                districtViewModels.Add(new TrustDistrictViewModel()
                {
                    Id = district.Id,
                    DistrictName = district.DistrictName,
                    Description = district.Description,
                    IsActive = district.IsActive,
                    Region = new TrustRegionViewModel()
                    {
                        Id = district.Region.Id,
                        RegionName = district.Region.RegionName
                    }
                });
            }

            return new PagedResult<TrustDistrictViewModel>()
            {
                PageCount = pageCount,
                RowCount = rowCount,
                CurrentPage = currentPage,
                Items = districtViewModels
            };
        }

        public TrustDistrictViewModel GetTrustDistrictDetail(int districtId)
        {
            var district = districtRepository.Single(x => x.Id == districtId, x => x.Region);

            if (district == null)
                return null;

            TrustDistrictViewModel trustDistrictViewModel = new TrustDistrictViewModel();

            trustDistrictViewModel.Id = district.Id;
            trustDistrictViewModel.DistrictName = district.DistrictName;
            trustDistrictViewModel.Description = district.Description;
            trustDistrictViewModel.Region = new TrustRegionViewModel()
            {
                Id = district.Region.Id,
                RegionName = district.Region.RegionName
            };

            return trustDistrictViewModel;
        }

        public int ActiveTrustDistrict(int districtId)
        {
            var district = districtRepository.Single(x => x.Id == districtId);

            if (district != null)
            {
                district.IsActive = true;

                return unitOfWork.Commit();
            }

            return 0;
        }

        public int InActiveTrustDistrict(int districtId)
        {
            var district = districtRepository.Single(x => x.Id == districtId);

            if (district != null)
            {
                district.IsActive = false;

                return unitOfWork.Commit();
            }

            return 0;
        }

        public bool CheckExistedDistrictName(int regionId, string districtName)
        {
            return districtRepository.GetMany(x => x.TrustRegionId == regionId && x.DistrictName.ToLower() == districtName.ToLower())
                                   .Count() > 0;
        }

        public int CreateTrustDistrict(TrustDistrictViewModel districtViewModel)
        {
            TrustDistrict district = new TrustDistrict();

            district.DistrictName = districtViewModel.DistrictName;
            district.Description = districtViewModel.Description;
            district.TrustRegionId = districtViewModel.Region.Id;
            district.IsActive = true;

            districtRepository.Add(district);

            return unitOfWork.Commit();
        }

        public int UpdateTrustDistrict(TrustDistrictViewModel districtViewModel)
        {
            TrustDistrict district = districtRepository.Single(x => x.Id == districtViewModel.Id);

            district.DistrictName = districtViewModel.DistrictName;
            district.Description = districtViewModel.Description;

            districtRepository.Update(district);

            return unitOfWork.Commit();
        }
    }
}