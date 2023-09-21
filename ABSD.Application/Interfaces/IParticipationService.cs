using ABSD.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ABSD.Application.Interfaces
{
	public interface IParticipationService
	{
		List<ParticipationViewModel> GetParticipationViewModel();

		int CreateParticipation(ParticipationViewModel participationViewModel);

		//int UpdateParticipation(ParticipationViewModel participationViewModel);
	}
}
