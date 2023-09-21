using ABSD.Application.Interfaces;
using ABSD.Application.ViewModels;
using ABSD.Common.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace ABSD.WebApp.Controllers
{
    public class PremiseController : Controller
    {
        private readonly IPremiseService premiseService;

        public PremiseController(IPremiseService premiseService)
        {
            this.premiseService = premiseService;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region AJAX API

        [HttpPost]
        public IActionResult Get(int serviceId)
        {
            try
            {
                var pagedResult = premiseService.GetByServiceId(serviceId, null);
                return Ok(new AjaxResult() { Code = 0, ErrorMessage = string.Empty, Success = true, Data = pagedResult });
            }
            catch (Exception ex)
            {
                return Ok(new AjaxResult() { Code = 1, ErrorMessage = "System Error. Please try later", Success = false });
            }
        }

        [HttpPost]
        public IActionResult GetPremiseNotLink(int serviceId)
        {
            try
            {
                var pagedResult = premiseService.GetPremiseNotLink(serviceId);
                return Ok(new AjaxResult() { Code = 0, ErrorMessage = string.Empty, Success = true, Data = pagedResult });
            }
            catch (Exception ex)
            {
                return Ok(new AjaxResult() { Code = 1, ErrorMessage = "System Error. Please try later", Success = false });
            }
        }

        [HttpPost]
        public IActionResult AssociateNewPremise(List<ServicePremiseViewModel> servicePremiseVM)
        {
            try
            {
                premiseService.AddServicePremise(servicePremiseVM);
                return Ok(new AjaxResult() { Code = 0, ErrorMessage = string.Empty, Success = true });
            }
            catch (Exception ex)
            {
                return Ok(new AjaxResult() { Code = 1, ErrorMessage = "System Error. Please try later", Success = false });
            }
        }

        [HttpPost]
        public IActionResult RemoveServicePrimise(int premiseId, int serviceId)
        {
            try
            {
                var result = premiseService.RemoveServicePremise(premiseId, serviceId);
                return Ok(new AjaxResult() { Code = 0, ErrorMessage = string.Empty, Success = true, Data = result });
            }
            catch (Exception ex)
            {
                return Ok(new AjaxResult() { Code = 1, ErrorMessage = "System Error. Please try later", Success = false });
            }
        }

        #endregion AJAX API
    }
}