using ITCanCook_DataAcecss.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITCanCook_BusinessObject.Service.Interface
{
    public interface IRecipeCategoryService
    {
        public List<RecipeCategory> GetRecipeCategories();
        public RecipeCategory GetRecipeCategoryById(int id);
        public bool CreateRecipeCategory(RecipeCategory rCategory);
        public bool UpdateRecipeCategory(RecipeCategory rCategory);
        public bool DeleteRecipeCategoryById(int id);
    }
}
