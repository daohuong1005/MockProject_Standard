using ABSD.Application.Interfaces;
using ABSD.Common.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ABSD.WebApp.Controllers
{
    public class CountryController : Controller
    {
        private readonly ICountryService countryService;

        public CountryController(ICountryService countryService)
        {
            this.countryService = countryService;
        }

        #region AJAX API

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var countries = countryService.GetCountries();
                if (countries != null)
                    return Ok(new AjaxResult() { Code = 0, ErrorMessage = string.Empty, Success = true, Data = countries });
                else
                    return Ok(new AjaxResult() { Code = 1, ErrorMessage = "Data is empty", Success = false });
            }
            catch (Exception ex)
            {
                return Ok(new AjaxResult() { Code = 1, ErrorMessage = "System Error. Please try later", Success = false });
            }
        }

        #endregion AJAX API
    }
}