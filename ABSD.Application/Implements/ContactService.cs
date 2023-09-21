using ABSD.Application.Interfaces;
using ABSD.Application.ViewModels;
using ABSD.Data;
using ABSD.Data.IRepositories;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ABSD.Application.Implements
{
	public class ContactService: IContactService
	{
		private readonly IUnitOfWork unitOfWork;
		private readonly IContactRepository contactRepository;
		private readonly IMapper mapper;
		public ContactService(IUnitOfWork unitOfWork, IContactRepository contactRepository, IMapper mapper)
		{
			this.unitOfWork = unitOfWork;
			this.contactRepository = contactRepository;
			this.mapper = mapper;
		}

        public List<ContactViewModel> GetContacts()
        {
            var contacts = contactRepository.GetMany(x => x.IsActive == true).ToList();
            if (contacts.Count > 0)
            {
                List<ContactViewModel> contactViewModels = new List<ContactViewModel>();
                foreach (var item in contacts)
                {
                    contactViewModels.Add(new ContactViewModel()
                    {
                        Id = item.Id,
                        FirstName = item.FirstName,
                        SurName = item.FirstName,
                        MobilePhone = item.MobilePhone,
                        Email=item.Email,
                        ContactType=item.ContactType,
                        IsActive=item.IsActive
                    });
                }

                return contactViewModels;
            }

            return null;
        }
    }
}
