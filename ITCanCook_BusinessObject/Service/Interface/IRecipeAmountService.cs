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
        public bool CreateRecipeAmount(RecipeAmount amount);
        public bool UpdateRecipeAmount(RecipeAmount amount);
        public bool DeleteRecipeAmountById(int id);
    }
}
