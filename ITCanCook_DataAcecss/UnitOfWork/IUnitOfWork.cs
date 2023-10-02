using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITCanCook_DataAcecss.UnitOfWork
{
	public interface IUnitOfWork
	{
		bool Commit();
		Task<bool> CommitAsync();
	}
}
