using ITCanCook_DataAcecss.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITCanCook_BusinessObject.Service.Interface
{
    internal interface IIngredientCategoryService
    {
        public List<IngredientCategory> GetIngredientCategories();
        public IngredientCategory GetIngredientCategoryById(int id);
        public bool CreateIngredientCategory(IngredientCategory category);
        public bool UpdateIngredientCategory(IngredientCategory category);
        public bool DeleteIngredientCategoryById(int id);
    }
}
