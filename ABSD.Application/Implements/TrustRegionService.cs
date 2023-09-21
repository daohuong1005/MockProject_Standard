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
    public class TrustRegionService : ITrustRegionService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ITrustRegionRepository regionRepository;

        public TrustRegionService(IUnitOfWork unitOfWork, ITrustRegionRepository regionRepository)
        {
            this.unitOfWork = unitOfWork;
            this.regionRepository = regionRepository;
        }

        public PagedResult<TrustRegionViewModel> GetTrustRegionsWithPaging(int? page, string firstCharacters = "", bool includeInActive = false)
        {
            var query = regionRepository.GetAll(x => x.Country);

            if (!includeInActive)
                query = query.Where(x => x.IsActive == true);

            if (!string.IsNullOrEmpty(firstCharacters))
            {
                query = query.Where(x => firstCharacters.Contains(x.RegionName.Substring(0, 1)));
            }

            int rowCount = query.Count();
            int pageCount = (int)Math.Ceiling((double)rowCount / Paging.PageSize);
            int currentPage = page.HasValue ? page.Value : 1;

            var regions = query.OrderBy(x => x.RegionName)
                            .Skip((currentPage - 1) * Paging.PageSize)
                            .Take(Paging.PageSize)
                            .ToList();

            var regionViewModels = new List<TrustRegionViewModel>();

            foreach (var region in regions)
            {
                regionViewModels.Add(new TrustRegionViewModel()
                {
                    Id = region.Id,
                    RegionName = region.RegionName,
                    Description = region.Description,
                    IsActive = region.IsActive,
                    Country = new CountryViewModel()
                    {
                        Id = region.Country.Id,
                        CountryName = region.Country.CountryName
                    }
                });
            }

            return new PagedResult<TrustRegionViewModel>()
            {
                PageCount = pageCount,
                RowCount = rowCount,
                CurrentPage = currentPage,
                Items = regionViewModels
            };
        }

        public TrustRegionViewModel GetTrustRegionDetail(int regionId)
        {
            var region = regionRepository.Single(x => x.Id == regionId, x => x.Country, x => x.Districts);

            if (region == null)
                return null;

            TrustRegionViewModel trustRegionViewModel = new TrustRegionViewModel();

            trustRegionViewModel.Id = region.Id;
            trustRegionViewModel.RegionName = region.RegionName;
            trustRegionViewModel.Description = region.Description;
            trustRegionViewModel.IsActive = region.IsActive;
            trustRegionViewModel.CountryId = region.CountryId;

            if (region.Districts != null)
            {
                trustRegionViewModel.Districts = new List<TrustDistrictViewModel>();

                foreach (var district in region.Districts)
                {
                    trustRegionViewModel.Districts.Add(new TrustDistrictViewModel()
                    {
                        Id = district.Id,
                        DistrictName = district.DistrictName,
                        Description = district.Description,
                        IsActive = district.IsActive,
                        Region = new TrustRegionViewModel { RegionName = region.RegionName }
                    });
                }
            }

            return trustRegionViewModel;
        }

        public TrustRegionViewModel GetRegion(int regionId)
        {
            var region = regionRepository.Single(x => x.Id == regionId, x => x.Country, x => x.Districts);

            if (region == null)
                return null;

            TrustRegionViewModel trustRegionViewModel = new TrustRegionViewModel();

            trustRegionViewModel.Id = region.Id;
            trustRegionViewModel.RegionName = region.RegionName;

            return trustRegionViewModel;
        }

        public int ActiveTrustRegion(int regionId)
        {
            var region = regionRepository.Single(x => x.Id == regionId);

            if (region != null)
            {
                region.IsActive = true;

                return unitOfWork.Commit();
            }

            return 0;
        }

        public int InActiveTrustRegion(int regionId)
        {
            var region = regionRepository.Single(x => x.Id == regionId);

            if (region != null)
            {
                region.IsActive = false;

                return unitOfWork.Commit();
            }

            return 0;
        }

        public bool CheckExistedRegionName(string regionName)
        {
            return regionRepository.GetMany(x => x.RegionName.ToLower() == regionName.ToLower())
                                   .Count() > 0;
        }

        public int CreateTrustRegion(TrustRegionViewModel regionViewModel)
        {
            TrustRegion region = new TrustRegion();

            region.RegionName = regionViewModel.RegionName;
            region.Description = regionViewModel.Description;
            region.CountryId = regionViewModel.CountryId;
            region.IsActive = true;

            regionRepository.Add(region);

            return unitOfWork.Commit();
        }

        public int UpdateTrustRegion(TrustRegionViewModel regionViewModel)
        {
            TrustRegion region = regionRepository.Single(x => x.Id == regionViewModel.Id);

            region.RegionName = regionViewModel.RegionName;
            region.Description = regionViewModel.Description;
            region.CountryId = regionViewModel.CountryId;

            regionRepository.Update(region);

            return unitOfWork.Commit();
        }
    }
}