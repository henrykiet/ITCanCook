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
    public class IngredientCategoryService : IIngredientCategoryService
    {
		private readonly IIngredientCategoryRepo _repo;
		private readonly IMapper _mapper;
		public IngredientCategoryService(IIngredientCategoryRepo repo, IMapper mapper)
		{
			_repo = repo;
			_mapper = mapper;
		}

		public ResponseObject CreateIngredientCategory(IngredientCategoryCreateRequest category)
		{
            var result = new PostRequestResponse();
            category.name.Trim();
			if (string.IsNullOrEmpty(category.name))
			{
                result.Status = 400;
                result.Message = "Bad request! Không nhận chuỗi trống";
                return result;
            }
			if (_repo.Get(c => c.name.Contains(category.name)).Count() > 0)
			{
                result.Status = 400;
                result.Message = "Bad request! Không trùng lặp tên";
                return result;
            }
			_repo.Create(_mapper.Map<IngredientCategory>(category));
            result.Status = 200;
            result.Message = "OK! Tạo thành công";
            return result;
        }

		public ResponseObject DeleteIngredientCategoryById(int id)
		{
			var result = new PostRequestResponse();

            if (_repo.GetById(id) == null)
			{
                result.Status = 400;
                result.Message = "Not found! Không tìm thấy category cần xoá";
                return result;
			}
			_repo.Delete(_repo.GetById(id));
            result.Status = 200;
            result.Message = "OK! Đã xoá";
            return result;
		}

		public IngredientCategory GetIngredientCategoryById(int id)
		{
			return _repo.GetById(id);
		}

		public List<IngredientCategory> GetIngredientCategories()
		{
			return _repo.GetAll().ToList();
		}

		public ResponseObject UpdateIngredientCategory(IngredientCategoryRequest category)
		{
			var result = new PostRequestResponse();
			category.name.Trim();
			var t = _repo.GetById(category.Id);
			if (t == null)
			{
                result.Status = 404;
                result.Message = "Not found! Không tìm thấy category cần cập nhật";
                return result;
            }
            _repo.DetachEntity(t);
            if (string.IsNullOrWhiteSpace(category.name))
			{
                result.Status = 400;
                result.Message = "Không nhận chuỗi trống";
                return result;
            }
			var t2 = _repo.Get(c => c.name.Equals(category.name)).FirstOrDefault();
			if(t2 != null && t2.Id != category.Id)
			{
                result.Status = 400;
                result.Message = "Trùng tên với category id "+t2.Id;
                return result;
            }
			_repo.Update(_mapper.Map<IngredientCategory>(category));
            result.Status = 200;
			result.Message = "OK! đã cập nhật category";// + category.Id;
            return result;
        }
	}
}
