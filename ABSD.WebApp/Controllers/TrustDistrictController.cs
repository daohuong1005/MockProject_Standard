using ABSD.Application.Interfaces;
using ABSD.Application.ViewModels;
using ABSD.Common.Constants;
using ABSD.Common.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ABSD.WebApp.Controllers
{
    public class TrustDistrictController : Controller
    {
        private readonly ITrustDistrictService districtService;

        public TrustDistrictController(ITrustDistrictService districtService)
        {
            this.districtService = districtService;
        }

        [HttpPost]
        public IActionResult Get(int regionId, int? page, string firstCharacters = "", bool includeInActive = false)
        {
            try
            {
                var pagedResult = districtService.GetTrustDistrictsWithPaging(regionId, page, firstCharacters, includeInActive);
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
        public IActionResult Active(int districtId)
        {
            try
            {
                var pagedResult = districtService.ActiveTrustDistrict(districtId);
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
        public IActionResult InActive(int districtId)
        {
            try
            {
                var pagedResult = districtService.InActiveTrustDistrict(districtId);
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
        public IActionResult Details(int districtId)
        {
            try
            {
                var district = districtService.GetTrustDistrictDetail(districtId);
                return Ok(new AjaxResult()
                {
                    Success = true,
                    Code = ReturnCode.OK,
                    Data = district,
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
        public IActionResult Create(TrustDistrictViewModel districtViewModel)
        {
            try
            {
                if (string.IsNullOrEmpty(districtViewModel.DistrictName))
                    return Ok(new AjaxResult()
                    {
                        Success = false,
                        Code = ReturnCode.ValidationError,
                        ErrorMessage = "Please input the District Name"
                    });

                if (districtViewModel.Region.Id <= 0)
                    return Ok(new AjaxResult()
                    {
                        Success = false,
                        Code = ReturnCode.ValidationError,
                        ErrorMessage = "Region Name is empty"
                    });

                bool isExistedDistrictName = districtService.CheckExistedDistrictName(districtViewModel.Region.Id, districtViewModel.DistrictName);

                if (isExistedDistrictName)
                    return Ok(new AjaxResult()
                    {
                        Success = false,
                        Code = ReturnCode.ValidationError,
                        ErrorMessage = "The District Name is existed. Please input the other",
                    });

                var result = districtService.CreateTrustDistrict(districtViewModel);

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
        public IActionResult Update(TrustDistrictViewModel districtViewModel)
        {
            try
            {
                if (string.IsNullOrEmpty(districtViewModel.DistrictName))
                    return Ok(new AjaxResult()
                    {
                        Success = false,
                        Code = ReturnCode.ValidationError,
                        ErrorMessage = "Please input the District Name"
                    });

                if (districtViewModel.Region.Id <= 0)
                    return Ok(new AjaxResult()
                    {
                        Success = false,
                        Code = ReturnCode.ValidationError,
                        ErrorMessage = "Trust District is invalid"
                    });

                bool isExistedDistrictName = false;

                var currentDistrict = districtService.GetTrustDistrictDetail(districtViewModel.Id);

                if (currentDistrict.DistrictName != districtViewModel.DistrictName)
                    isExistedDistrictName = districtService.CheckExistedDistrictName(districtViewModel.Region.Id, districtViewModel.DistrictName);

                if (isExistedDistrictName)
                    return Ok(new AjaxResult()
                    {
                        Success = false,
                        Code = ReturnCode.ValidationError,
                        ErrorMessage = "The District Name is existed. Please input the other",
                    });

                var result = districtService.UpdateTrustDistrict(districtViewModel);

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
    }
}