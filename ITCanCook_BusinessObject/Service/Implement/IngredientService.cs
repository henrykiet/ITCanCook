using ITCanCook_BusinessObject.Service.Interface;
using ITCanCook_DataAcecss.Entities;
using ITCanCook_DataAcecss.Repository.Implement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITCanCook_BusinessObject.Service.Implement
{

    public class IngredientService : IIngredientService
    {
        private readonly IIngredientRepo _repo;
        public IngredientService(IIngredientRepo repo)
        {
            _repo = repo;
        }

        public bool CreateIngredient(Ingredient ingredient)
        {
            if (_repo.GetById(ingredient.Id) != null)
            {
                return false;
            }
            _repo.Create(ingredient);
            return true;
        }

        public bool DeleteIngredientById(int id)
        {
            var ingredient = _repo.GetById(id);
            if (ingredient == null)
            {
                return false;
            }
            _repo.Delete(ingredient);
            return true;
        }

        public Ingredient GetIngredientById(int id)
        {
            return _repo.GetById(id);
        }

        public List<Ingredient> GetIngredients()
        {
            return _repo.GetAll().ToList();
        }

        public bool UpdateIngredient(Ingredient ingredient)
        {
            if (_repo.GetById(ingredient.Id) == null)
            {
                return false;
            }
            _repo.Update(ingredient);
            return true;
        }
    }
}
