using ITCanCook_DataAcecss.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITCanCook_BusinessObject.Service.Interface
{
    public interface IRecipeStepService
    {
        public List<RecipeStep> GetRecipeSteps();
        public RecipeStep GetRecipeStepById(int id);
        public bool CreateRecipeStep(CookingMethod method);
        public bool UpdateRecipeStep(CookingMethod method);
        public bool DeleteRecipeStepById(int id);
    }
}
