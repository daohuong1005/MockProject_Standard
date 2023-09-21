using ABSD.Application.Interfaces;
using ABSD.Application.ViewModels;
using ABSD.Data;
using ABSD.Data.IRepositories;
using System.Collections.Generic;
using System.Linq;

namespace ABSD.Application.Implements
{
    public class CountryService : ICountryService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ICountryRepository countryRepository;

        public CountryService(IUnitOfWork unitOfWork, ICountryRepository countryRepository)
        {
            this.unitOfWork = unitOfWork;
            this.countryRepository = countryRepository;
        }

        public List<CountryViewModel> GetCountries()
        {
            var countries = countryRepository.GetMany(x => x.IsActive == true).ToList();
            if (countries.Count > 0)
            {
                List<CountryViewModel> countryViewModels = new List<CountryViewModel>();
                foreach (var country in countries)
                {
                    countryViewModels.Add(new CountryViewModel()
                    {
                        Id = country.Id,
                        CountryName = country.CountryName,
                        Description = country.Description
                    });
                }

                return countryViewModels;
            }

            return null;
        }
    }
}