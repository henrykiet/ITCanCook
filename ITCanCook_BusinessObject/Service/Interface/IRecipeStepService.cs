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
    public interface IRecipeStepService
    {
        public List<RecipeStep> GetRecipeSteps();
        public RecipeStep GetRecipeStepById(int id);
        public ResponseObject CreateRecipeStep(RecipeStepCreateRequest step);
        public ResponseObject UpdateRecipeStep(RecipeStepRequest step);
        public ResponseObject DeleteRecipeStepById(int id);
    }
}
