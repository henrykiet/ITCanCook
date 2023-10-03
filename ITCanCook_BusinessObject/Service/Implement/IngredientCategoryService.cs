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
    public class IngredientCategoryService : IIngredientCategoryService
    {
		private readonly IIngredientCategoryRepo _repo;
		public IngredientCategoryService(IIngredientCategoryRepo repo)
		{
			_repo = repo;
		}

		public bool CreateIngredientCategory(IngredientCategory category)
		{
			category.name.Trim();
			if (_repo.Get(c => c.name.Contains(category.name)).Count() > 0 ||
				string.IsNullOrEmpty(category.name))
			{
				return false;
			}
			_repo.Create(category);
			return true;
		}

		public bool DeleteIngredientCategoryById(int id)
		{
			if (_repo.GetById(id) == null)
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
			category.name.Trim();
			var t = _repo.GetById(category.Id);
			if (t == null || string.IsNullOrWhiteSpace(category.name))
			{
				return false;
			}
			_repo.DetachEntity(t);
			_repo.Update(category);
			return true;
		}
	}
}
