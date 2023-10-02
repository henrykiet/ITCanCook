using ITCanCook_DataAcecss.Entities;
using ITCanCook_DataAcecss.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITCanCook_BusinessObject.Service
{
    public interface IRecipeStepService
    {
        public List<RecipeStep> GetRecipies();
        public RecipeStep GetRecipeStepById(int id);
        public bool CreateRecipeStep(RecipeStep step);
        public bool UpdateRecipeStep(RecipeStep step);
        public bool DeleteRecipeStepById(int id);
    }
    internal class RecipeStepService:IRecipeStepService
    {
        private readonly IRecipeStepRepo _repo;

        public RecipeStepService(IRecipeStepRepo repo)
        {
            _repo = repo;
        }

        public bool CreateRecipeStep(RecipeStep step)
        {
            
            return true;
        }

        public bool DeleteRecipeStepById(int id)
        {
            throw new NotImplementedException();
        }

        public RecipeStep GetRecipeStepById(int id)
        {
            throw new NotImplementedException();
        }

        public List<RecipeStep> GetRecipies()
        {
            throw new NotImplementedException();
        }

        public bool UpdateRecipeStep(RecipeStep step)
        {
            throw new NotImplementedException();
        }
    }
}
