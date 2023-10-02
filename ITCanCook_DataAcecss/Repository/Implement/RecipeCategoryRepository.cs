using ITCanCook_DataAcecss.Entities;
using ITCanCook_DataAcecss.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITCanCook_DataAcecss.Repository.Implement
{
    public interface IRecipeCategoryRepo : IBaseRepository<RecipeCategory> { }
    public class RecipeCategoryRepository : BaseRepository<RecipeCategory>, IRecipeCategoryRepo
    {
        public RecipeCategoryRepository(DbContext context) : base(context)
        {
        }
    }
}
