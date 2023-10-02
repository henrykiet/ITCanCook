using ITCanCook_DataAcecss.Entities;
using ITCanCook_DataAcecss.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITCanCook_BusinessObject.Service
{
    public interface IRecipeService
    {
        public List<Recipe> GetRecipies();
        public Recipe GetRecipeById(int id);
        public bool CreateRecipe(Recipe recipe);
        public bool UpdateRecipe(Recipe recipe);
        public bool DeleteRecipeById(int id);
    }
    internal class RecipeService : IRecipeService
    {
        private readonly IRecipeRepo _repo;
        public RecipeService(IRecipeRepo repo)
        {
            _repo = repo;
        }

        public bool CreateRecipe(Recipe recipe)
        {
            if(_repo.GetById(recipe.Id) != null)
            {
                return false;
            }
            _repo.Create(recipe);
            return true;
        }

        public bool DeleteRecipeById(int id)
        {
            if (_repo.GetById(id) == null)
            {
                return false;
            }
            _repo.Delete(_repo.GetById(id));
            return true;
        }

        public Recipe GetRecipeById(int id)
        {
            return _repo.GetById(id);
        }

        public List<Recipe> GetRecipies()
        {
            return _repo.GetAll().ToList();
        }

        public bool UpdateRecipe(Recipe recipe)
        {
            if (_repo.GetById(recipe.Id) == null)
            {
                return false;
            }
            _repo.Update(recipe);
            return true;
        }
    }
}
