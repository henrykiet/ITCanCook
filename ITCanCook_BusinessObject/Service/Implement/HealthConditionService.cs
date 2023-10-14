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
    public class HealthConditionService : IHealthConditionService
    {
        private readonly IHealthConditionRepo _repo;
        private readonly IHealthConditionCategoryRepo _cRepo;
        private readonly IMapper _mapper;

        public HealthConditionService(IHealthConditionRepo repo, IMapper mapper, IHealthConditionCategoryRepo cRepo)
        {
            _repo = repo;
            _mapper = mapper;
            _cRepo = cRepo;
        }

        public ResponseObject CreateHealthCondition(HealthConditionCreateRequest health)
        {
            var result = new PostRequestResponse();
            if (_cRepo.GetById(health.HealthConditionCategoryId)==null)
            {
                result.Status = 400;
                result.Message = "Bad request! Category ID không tồn tại";
                return result;
            }
            if (string.IsNullOrWhiteSpace(health.Name))
            {
                result.Status = 400;
                result.Message = "Bad request! Chuỗi trống";
                return result;
            }
            var t = _repo.Get(h => h.Name.Equals(health.Name)).FirstOrDefault();
            if (t != null)
            {
                result.Status = 400;
                result.Message = "Trùng tên với health Id "+t.Id;
                _repo.DetachEntity(t);
                return result;
            }
            _repo.Create(_mapper.Map<HealthCondition>(health));
            result.Status = 200;
            result.Message = "OK! Tạo thành công";
            return result;
        }

        public ResponseObject DeleteHealthConditionById(int id)
        {
            var result = new PostRequestResponse();
            var t = _repo.GetById(id);
            if ( t == null)
            {
                result.Status = 404;
                result.Message = "Bad request! ID không tồn tại";
                return result;
            }
            result.Status = 200;
            result.Message = "OK!";
            return result;
        }

        public HealthCondition GetHealthConditionById(int id)
        {
            return _repo.GetById(id);
        }

        public List<HealthCondition> GetHealthConditions()
        {
            return _repo.GetAll().ToList();
        }

        public ResponseObject UpdateHealthCondition(HealthConditionRequest health)
        {
            var result = new PostRequestResponse();
            var t = _repo.GetById(health.Id);
            if(t == null)
            {
                result.Status = 400;
                result.Message = "Bad request! ID không tồn tại";
                return result;
            }
            _repo.DetachEntity(t);
            if (_cRepo.GetById(t.HealthConditionCategoryId) == null)
            {
                result.Status = 400;
                result.Message = "Bad request! Category ID không tồn tại";
                return result;
            }
            if (string.IsNullOrWhiteSpace(t.Name))
            {
                result.Status = 400;
                result.Message = "Bad request! Chuỗi trống";
                return result;
            }
            var t2 = _repo.Get(c => c.Name.Equals(health.Name)).FirstOrDefault();
            if (t2 != null && t2.Id != health.Id)
            {
                result.Status = 400;
                result.Message = "Trùng dữ liệu với category id " + t.Id;
                _repo.DetachEntity(t2);
                return result;
            }
            health.Name.Trim();
            _repo.Update(_mapper.Map<HealthCondition>(health));
            result.Status = 200;
            result.Message = "OK! Cập nhật thành công";
            return result;
        }

        public List<HealthCondition> GetConditionByCategoryId(int categoryId)
        {
            return _repo.Get(h => h.HealthConditionCategoryId == categoryId).ToList();
        }
    }
}
