using ABSD.Application.Interfaces;
using ABSD.Application.ViewModels;
using ABSD.Common.Constants;
using ABSD.Common.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace ABSD.WebApp.Controllers
{
    public class TrustRegionController : Controller
    {
        private readonly ITrustRegionService regionService;

        public TrustRegionController(ITrustRegionService regionService)
        {
            this.regionService = regionService;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region AJAX API

        [HttpPost]
        public IActionResult Paging(int? page, string firstCharacters = "", bool includeInActive = false)
        {
            try
            {
                var pagedResult = regionService.GetTrustRegionsWithPaging(page, firstCharacters, includeInActive);
                return Ok(new AjaxResult()
                {
                    Success = true,
                    Code = ReturnCode.OK,
                    Data = pagedResult,
                    ErrorMessage = string.Empty
                });
            }
            catch (Exception ex)
            {
                return Ok(new AjaxResult()
                {
                    Success = false,
                    Code = ReturnCode.SystemError,
                    ErrorMessage = ErrorMessage.SystemErrorMessage
                });
            }
        }

        [HttpPost]
        public IActionResult Active(int regionId)
        {
            try
            {
                var pagedResult = regionService.ActiveTrustRegion(regionId);
                return Ok(new AjaxResult()
                {
                    Success = true,
                    Code = ReturnCode.OK,
                    ErrorMessage = string.Empty,
                    Data = pagedResult
                });
            }
            catch (Exception ex)
            {
                return Ok(new AjaxResult()
                {
                    Success = false,
                    Code = ReturnCode.SystemError,
                    ErrorMessage = ErrorMessage.SystemErrorMessage
                });
            }
        }

        [HttpPost]
        public IActionResult InActive(int regionId)
        {
            try
            {
                var pagedResult = regionService.InActiveTrustRegion(regionId);
                return Ok(new AjaxResult()
                {
                    Success = true,
                    Code = ReturnCode.OK,
                    Data = pagedResult,
                    ErrorMessage = string.Empty
                });
            }
            catch (Exception ex)
            {
                return Ok(new AjaxResult()
                {
                    Success = false,
                    Code = ReturnCode.SystemError,
                    ErrorMessage = ErrorMessage.SystemErrorMessage
                });
            }
        }

        [HttpPost]
        public IActionResult Details(int regionId)
        {
            try
            {
                var region = regionService.GetTrustRegionDetail(regionId);
                return Ok(new AjaxResult()
                {
                    Success = true,
                    Code = ReturnCode.OK,
                    Data = region,
                    ErrorMessage = string.Empty
                });
            }
            catch (Exception ex)
            {
                return Ok(new AjaxResult()
                {
                    Success = false,
                    Code = ReturnCode.SystemError,
                    ErrorMessage = ErrorMessage.SystemErrorMessage
                });
            }
        }

        [HttpGet]
        public IActionResult GetRegion(int regionId)
        {
            try
            {
                var region = regionService.GetRegion(regionId);
                return Ok(new AjaxResult()
                {
                    Success = true,
                    Code = ReturnCode.OK,
                    Data = region,
                    ErrorMessage = string.Empty
                });
            }
            catch (Exception ex)
            {
                return Ok(new AjaxResult()
                {
                    Success = false,
                    Code = ReturnCode.SystemError,
                    ErrorMessage = ErrorMessage.SystemErrorMessage
                });
            }
        }

        [HttpPost]
        public IActionResult Create(TrustRegionViewModel regionViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Ok(new AjaxResult()
                    {
                        Success = false,
                        Code = ReturnCode.ValidationError,
                        ErrorMessage = ModelState.Values.Select(x => x.Errors).ToString()
                    });

                if (regionViewModel.CountryId <= 0)
                    return Ok(new AjaxResult()
                    {
                        Success = false,
                        Code = ReturnCode.ValidationError,
                        ErrorMessage = "Please select the Nation/Country"
                    });

                bool isExistedRegionName = regionService.CheckExistedRegionName(regionViewModel.RegionName);

                if (isExistedRegionName)
                    return Ok(new AjaxResult()
                    {
                        Success = false,
                        Code = ReturnCode.ValidationError,
                        ErrorMessage = "The Region Name is existed. Please input the other",
                    });

                var result = regionService.CreateTrustRegion(regionViewModel);

                return Ok(new AjaxResult()
                {
                    Success = true,
                    Code = ReturnCode.OK,
                    Data = result,
                    ErrorMessage = string.Empty,
                });
            }
            catch (Exception ex)
            {
                return Ok(new AjaxResult()
                {
                    Success = false,
                    Code = ReturnCode.SystemError,
                    ErrorMessage = ErrorMessage.SystemErrorMessage
                });
            }
        }

        [HttpPost]
        public IActionResult Update(TrustRegionViewModel regionViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Ok(new AjaxResult()
                    {
                        Success = false,
                        Code = ReturnCode.ValidationError,
                        ErrorMessage = ModelState.Values.Select(x => x.Errors).ToString()
                    });

                if (regionViewModel.CountryId <= 0)
                    return Ok(new AjaxResult()
                    {
                        Success = false,
                        Code = ReturnCode.ValidationError,
                        ErrorMessage = "Please select the Nation/Country"
                    });

                bool isExistedRegionName = false;

                var currentRegion = regionService.GetTrustRegionDetail(regionViewModel.Id);

                if (currentRegion.RegionName != regionViewModel.RegionName)
                    isExistedRegionName = regionService.CheckExistedRegionName(regionViewModel.RegionName);

                if (isExistedRegionName)
                    return Ok(new AjaxResult()
                    {
                        Success = false,
                        Code = ReturnCode.ValidationError,
                        ErrorMessage = "The Region Name is existed. Please input the other",
                    });

                var result = regionService.UpdateTrustRegion(regionViewModel);

                return Ok(new AjaxResult()
                {
                    Success = true,
                    Code = ReturnCode.OK,
                    Data = result,
                    ErrorMessage = string.Empty,
                });
            }
            catch (Exception ex)
            {
                return Ok(new AjaxResult()
                {
                    Success = false,
                    Code = ReturnCode.SystemError,
                    ErrorMessage = ErrorMessage.SystemErrorMessage
                });
            }
        }

        #endregion AJAX API
    }
}