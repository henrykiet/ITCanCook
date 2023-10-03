using ITCanCook_DataAcecss.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITCanCook_BusinessObject.Service.Interface
{
    public interface IHealthCategoryService
    {
        public List<HealthConditionCategory> GetRecipeCategories();
        public HealthConditionCategory GetRecipeCategoryById(int id);
        public bool CreateRecipeCategory(HealthConditionCategory rCategory);
        public bool UpdateRecipeCategory(HealthConditionCategory rCategory);
        public bool DeleteRecipeCategoryById(int id);
    }
}
