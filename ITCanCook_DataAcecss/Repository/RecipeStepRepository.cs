using ITCanCook_DataAcecss.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITCanCook_DataAcecss.Repository
{
    public interface IRecipeStepRepo : IBaseRepository<RecipeStep> { }
    public class RecipeStepRepository : BaseRepository<RecipeStep>, IRecipeStepRepo
    {
        public RecipeStepRepository(DbContext context) : base(context)
        {
        }
    }
}
