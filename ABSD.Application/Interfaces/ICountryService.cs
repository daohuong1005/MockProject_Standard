using ABSD.Application.ViewModels;
using System.Collections.Generic;

namespace ABSD.Application.Interfaces
{
    public interface ICountryService
    {
        List<CountryViewModel> GetCountries();
    }
}