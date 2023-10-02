using ITCanCook_DataAcecss.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITCanCook_DataAcecss.Repository
{
    public interface IRecipeRepo : IBaseRepository<Recipe>
    {

    }
    public class RecipeRepository : BaseRepository<Recipe>, IRecipeRepo
    {
        public RecipeRepository(DbContext context) : base(context)
        {
        }
    }
}
