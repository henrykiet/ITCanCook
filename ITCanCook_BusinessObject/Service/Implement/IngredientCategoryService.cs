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
    public class IngredientCategoryCategoryService : IIngredientCategoryService
    {
        private readonly IIngredientCategoryRepo _repo;
        public IngredientCategoryCategoryService(IIngredientCategoryRepo repo)
        {
            _repo = repo;
        }

        public bool CreateIngredientCategory(IngredientCategory category)
        {
            _repo.Create(category);
            return true;
        }

        public bool DeleteIngredientCategoryById(int id)
        {
            if (_repo.GetById(id) != null)
            {
                return false;
            }
            _repo.Delete(_repo.GetById(id));
            return true;
        }

        public IngredientCategory GetIngredientCategoryById(int id)
        {
            return _repo.GetById(id);
        }

        public List<IngredientCategory> GetIngredientCategories()
        {
            return _repo.GetAll().ToList();
        }

        public bool UpdateIngredientCategory(IngredientCategory category)
        {
            if (_repo.GetById(category.Id) != null)
            {
                return false;
            }
            _repo.Update(category);
            return true;
        }
    }
}
