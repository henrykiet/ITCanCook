using ITCanCook_DataAcecss.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITCanCook_BusinessObject.Service.Interface
{
    public interface IRecipeService
    {
        public List<Recipe> GetRecipies();
        public Recipe GetRecipeById(int id);
        public bool CreateRecipe(Recipe recipe);
        public bool UpdateRecipe(Recipe recipe);
        public bool DeleteRecipeById(int id);
    }
}
