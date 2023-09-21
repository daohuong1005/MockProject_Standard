using ABSD.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ABSD.Application.Interfaces
{
	public interface IServiceTypeService
	{
		List<ServiceTypeViewModel> GetServiceTypes();
	}
}
