using ITCanCook_DataAcecss.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITCanCook_DataAcecss.Repository
{
    public interface IRecipeAmountRepo: IBaseRepository<RecipeAmount>{}
    public class RecipeAmountRepository : BaseRepository<RecipeAmount>, IRecipeAmountRepo
    {
        public RecipeAmountRepository(DbContext context) : base(context)
        {
        }
    }
}
