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
		private readonly IIngredientCategoryRepo _categoryRepo;
		public IngredientService(IIngredientRepo repo, IIngredientCategoryRepo categoryRepo)
		{
			_repo = repo;
			_categoryRepo = categoryRepo;
		}

		public string CreateIngredient(Ingredient ingredient)
		{
			//if (_repo.GetById(ingredient.Id) != null||
			//    _repo.GetById(ingredient.IngredientCategoryId) == null||
			//    string.IsNullOrWhiteSpace(ingredient.name))
			//{
			//    return false;
			//}
			//_repo.Create(ingredient);
			//return true;
			var t = ingredient;
			if (_categoryRepo.GetById(t.IngredientCategoryId) == null)
			{
				return "Category của ingredient không tồn tại";
			}
			if (string.IsNullOrWhiteSpace(t.name))
			{
				return "Tên ingrident là chuỗi trống, nhập lại";
			}
			_repo.DetachEntity(t);
			_repo.Create(ingredient);
			return "Tạo thành công";
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

		public string UpdateIngredient(Ingredient ingredient)
		{

			var t = ingredient;
			if (_repo.GetById(t.Id) == null)
			{
				return "Id của ingredient không tồn tại!";
			}
			if (_categoryRepo.GetById(t.IngredientCategoryId) == null)
			{
				return "Category của ingredient không tồn tại";
			}
			if (string.IsNullOrWhiteSpace(t.name))
			{
				return "Tên ingrident là chuỗi trống, nhập lại";
			}
			_repo.DetachEntity(t);
			_repo.Update(ingredient);
			return "Cập nhật thành công";
		}
	}
}
