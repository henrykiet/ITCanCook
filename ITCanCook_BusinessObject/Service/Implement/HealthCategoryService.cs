using AutoMapper;
using ITCanCook_BusinessObject.ResponseObjects;
using ITCanCook_BusinessObject.ResponseObjects.Abstraction;
using ITCanCook_BusinessObject.Service.Interface;
using ITCanCook_BusinessObject.ServiceModel.RequestModel;
using ITCanCook_DataAcecss.Entities;
using ITCanCook_DataAcecss.Repository.Implement;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITCanCook_BusinessObject.Service.Implement
{
    public class HealthCategoryService : IHealthConditionCategoryService
    {
        private readonly IHealthConditionCategoryRepo _repo;
        private readonly IMapper _mapper;
        public HealthCategoryService(IHealthConditionCategoryRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public ResponseObject DeleteHealthConditionCategoryById(int id)
        {
            var result = new PostRequestResponse();
            if (_repo.GetById(id) == null)
            {
                result.Status = 404;
                result.Message = "Không tìm thấy Id";
                return result;
            }
            _repo.Delete(_repo.GetById(id));
            result.Status = 200;
            result.Message = "OK";
            return result;
        }

        public HealthConditionCategory GetHealthConditionCategoryById(int id)
        {
            return _repo.GetById(id);
        }

        public List<HealthConditionCategory> GetHealthConditionCategories()
        {
            return _repo.GetAll().ToList();
        }

        public ResponseObject CreateHealthConditionCategory(HealthConditionCategoryCreateRequest category)
        {
            var result = new PostRequestResponse();
            category.Name.Trim();
            if (string.IsNullOrWhiteSpace(category.Name))
            {
                result.Status = 400;
                result.Message = "Tên để trống";
                return result;
            }
            var t = _repo.Get(c => c.Name.Equals(category.Name)).FirstOrDefault();
            if (t != null)
            {
                result.Status = 400;
                result.Message = "Trùng dữ liệu với category id "+t.Id;
                _repo.DetachEntity(t);
                return result;
            }
            List<HealthConditionCategory> list  = GetHealthConditionCategories();
            if(list.Count() > 0)
            {
                int currentMaxIndexDisplay = list.Max(c => c.IndexDisplay);
                if ( currentMaxIndexDisplay >= category.IndexDisplay)
                {
                    result.Status = 400;
                    result.Message = "index display bé hơn hoặc bằng index display hiện tại : "+currentMaxIndexDisplay;
                    return result;
                }
            }
            _repo.Create(_mapper.Map<HealthConditionCategory>(category));
            result.Status = 200;
            result.Message = "OK";
            return result;
        }

        public ResponseObject UpdateHealthConditionCategory(HealthConditionCategoryRequest category)
        {
            category.Name.Trim();
            var t = _repo.GetById(category.Id);
            var result = new PostRequestResponse();
            if (t == null)
            {
                result.Status = 400;
                result.Message = "Không nhận thông tin trống";
                return result;
            }
            _repo.DetachEntity(t);
            if (string.IsNullOrWhiteSpace(category.Name))
            {
                result.Status = 400;
                result.Message = "Không nhận chuỗi trống";
                return result;
            }
            var t2 = _repo.Get(c => c.Name.Equals(category.Name)).FirstOrDefault();
            if (t2 != null && t2.Id == category.Id)
            {
                result.Status = 200;
                result.Message = "Trùng tên với category id " + t2.Id;
                _repo.DetachEntity(t2);
                return result;
            }
            List<HealthConditionCategory> list = GetHealthConditionCategories();
            if (list.Count()>0)
            {
                int currentMaxIndexDisplay = list.Max(c => c.IndexDisplay);
                if (currentMaxIndexDisplay > category.IndexDisplay)
                {
                    result.Status = 400;
                    result.Message = "index display bé hơn index display hiện tại : " + currentMaxIndexDisplay;
                    return result;
                }
            }
            _repo.Update(_mapper.Map<HealthConditionCategory>(category));
            result.Status = 200;
            result.Message = "OK";
            return result;
        }
    }
}
