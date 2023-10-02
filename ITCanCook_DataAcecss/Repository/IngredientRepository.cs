using ITCanCook_DataAcecss.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITCanCook_DataAcecss.Repository
{
    public interface IIngredientRepo : IBaseRepository<Ingredient>
    {

    }
    internal class IngredientRepository : BaseRepository<Ingredient>, IIngredientRepo
    {
        public IngredientRepository(DbContext context) : base(context)
        {
        }
    }
}
