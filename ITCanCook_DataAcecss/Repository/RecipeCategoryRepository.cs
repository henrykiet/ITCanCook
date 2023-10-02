using ITCanCook_DataAcecss.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITCanCook_DataAcecss.Repository
{
    public interface IRecipeCategoryRepo : IBaseRepository<RecipeCategory> { }
    public class RecipeCategoryRepository : BaseRepository<RecipeCategory>, IRecipeCategoryRepo
    {
        public RecipeCategoryRepository(DbContext context) : base(context)
        {
        }
    }
}
