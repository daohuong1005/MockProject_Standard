using ABSD.Data.Entities;
using ABSD.Data.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ABSD.Data.EF.Repositories
{
	public class ParticipationRepository : Repository<Participation>, IParticipationRepository
	{
		public ParticipationRepository(AppDbContext context) : base(context)
		{

		}
	}
}
