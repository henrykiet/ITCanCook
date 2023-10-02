using ITCanCook_DataAcecss.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITCanCook_DataAcecss.Repository
{
    public interface IIngredientCategoryRepo: IBaseRepository<IngredientCategory>
    {

    }
    internal class IngredientCategoryRepository : BaseRepository<IngredientCategory>, IIngredientCategoryRepo
    {
        public IngredientCategoryRepository(DbContext context) : base(context)
        {
        }
    }
}
