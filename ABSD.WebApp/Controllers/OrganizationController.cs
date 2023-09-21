using ABSD.Application.Interfaces;
using ABSD.Common.Constants;
using ABSD.Common.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ABSD.WebApp.Controllers
{
    public class OrganizationController : Controller
    {
        private readonly IOrganizationService orgService;

        public OrganizationController(IOrganizationService orgService)
        {
            this.orgService = orgService;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region AJAX API

        [HttpPost]
        public IActionResult GetOrganization(int serviceId, int? page)
        {
            try
            {
                var pagedResult = orgService.GetOrganizationWithPaging(serviceId, page);
                return Ok(new AjaxResult() { Code = 0, ErrorMessage = string.Empty, Success = true, Data = pagedResult });
            }
            catch (Exception ex)
            {
                return Ok(new AjaxResult() { Code = 1, ErrorMessage = "System Error. Please try later", Success = false });
            }
        }

        [HttpPost]
        public IActionResult GetRoleByOrganization(int organizationId)
        {
            try
            {
                var pagedResult = orgService.GetRoleByOrganization(organizationId);
                return Ok(new AjaxResult() { Code = 0, ErrorMessage = string.Empty, Success = true, Data = pagedResult });
            }
            catch (Exception ex)
            {
                return Ok(new AjaxResult() { Code = 1, ErrorMessage = "System Error. Please try later", Success = false });
            }
        }

        [HttpPost]
        public IActionResult UpdateRoleOrganization(int organizationId, int[] roleIds)
        {
            try
            {
                var result = orgService.UpdateRoleOrganization(organizationId, roleIds);
                return Ok(new AjaxResult()
                {
                    Success = true,
                    Code = ReturnCode.OK,
                    ErrorMessage = string.Empty,
                    Data = result
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