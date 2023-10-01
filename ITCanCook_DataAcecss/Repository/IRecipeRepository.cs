using ITCanCook_DataAcecss.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITCanCook_DataAcecss.Repository
{
	public interface IRecipeRepository
	{
		public Task<Recipe> GetRecipeAsync(int id);
		public Task<List<Recipe>> GetAllRecipeAsync();
	}
}
