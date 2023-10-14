using ITCanCook_BusinessObject.ResponseObjects.Abstraction;
using ITCanCook_BusinessObject.ServiceModel.RequestModel;
using ITCanCook_DataAcecss.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITCanCook_BusinessObject.Service.Interface
{
    public interface IRecipeAmountService
    {
        public List<RecipeAmount> GetRecipeAmounts();
        public RecipeAmount GetRecipeAmountById(int id);
        public ResponseObject CreateRecipeAmount(RecipeAmountCreateRequest amount);
        public ResponseObject UpdateRecipeAmount(RecipeAmountRequest amount);
        public ResponseObject DeleteRecipeAmountById(int id);
        public List<RecipeAmount> GetAmountByRecipeId(int recipeId);
    }
}
