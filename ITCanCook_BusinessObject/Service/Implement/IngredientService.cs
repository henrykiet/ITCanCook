using AutoMapper;
using ITCanCook_BusinessObject.ResponseObjects;
using ITCanCook_BusinessObject.ResponseObjects.Abstraction;
using ITCanCook_BusinessObject.Service.Interface;
using ITCanCook_BusinessObject.ServiceModel.RequestModel;
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
		private readonly IMapper _mapper;
		public IngredientService(IIngredientRepo repo, IIngredientCategoryRepo categoryRepo, IMapper mapper)
		{
			_repo = repo;
			_categoryRepo = categoryRepo;
			_mapper = mapper;
		}

		public ResponseObject CreateIngredient(IngredientCreateRequest ingredient)
		{
			var t = _repo.Get(i => i.name.Equals(ingredient)).FirstOrDefault();
			var result = new PostRequestResponse();
			if(t != null)
			{
                result.Status = 400;
                result.Message = "Trùng tên với ingredient ID "+t.Id;
				_repo.DetachEntity(t);
                return result;
            }
			if (_categoryRepo.GetById(ingredient.IngredientCategoryId) == null)
			{
                result.Status = 400;
                result.Message = "Category của ingredient không tồn tại";
                return result;
			}
			if (string.IsNullOrWhiteSpace(ingredient.name))
			{
                result.Status = 400;
                result.Message = "Tên ingrident là chuỗi trống, nhập lại";
                return result;
			}
            //_repo.DetachEntity(_mapper.Map<Ingredient>(t)); //đã sửa chỗ này
			_repo.Create(_mapper.Map<Ingredient>(ingredient));
            result.Status = 200;
            result.Message = "OK";
            return result;
        }

		public ResponseObject DeleteIngredientById(int id)
		{
			var result = new PostRequestResponse();
			var ingredient = _repo.GetById(id);
			if (ingredient == null)
			{
                result.Status = 400;
                result.Message = "Không tìm thấy";
                return result;
            }
			_repo.Delete(ingredient);
            result.Status = 200;
            result.Message = "OK! Tạo thành công";
            return result;
        }

		public Ingredient GetIngredientById(int id)
		{
			return _repo.GetById(id);
		}

		public List<Ingredient> GetIngredients()
		{
			return _repo.GetAll().ToList();
		}

		public ResponseObject UpdateIngredient(IngredientRequest ingredient)
		{
			var t = _repo.GetById(ingredient.Id);
			var result = new PostRequestResponse();
			if (_repo.GetById(t.Id) == null)
			{
                result.Status = 400;
                result.Message = "Id của ingredient không tồn tại!";
                return result;
			}
            _repo.DetachEntity(t);
            if (_categoryRepo.GetById(t.IngredientCategoryId) == null)
			{
                result.Status = 400;
                result.Message = "Category của ingredient không tồn tại";
                return result;
			}
			if (string.IsNullOrWhiteSpace(t.name))
			{
                result.Status = 400;
                result.Message = "Tên ingrident là chuỗi trống, nhập lại";
                return result;
			}
			var t2 = _repo.Get(i => i.name.Equals(ingredient.name)).FirstOrDefault();
			if (t2 != null && t2.Id != ingredient.Id)
			{
                result.Status = 400;
                result.Message = "Trùng tên với ingredient id "+t2.Id;
				_repo.DetachEntity(t2);
                return result;
            }
			_repo.Update(_mapper.Map<Ingredient>(ingredient));
            result.Status = 200;
            result.Message = "OK! Tạo thành công";
            return result;
        }
	}
}
