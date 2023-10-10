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

    public class EquipmentService : IEquipmentService
    {
        private readonly IEquipmentRepo _repo;
        private readonly IMapper _mapper;
        public EquipmentService(IEquipmentRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public ResponseObject CreateEquipment(EquipmentCreateRequest equipment)
        {
            var result = new PostRequestResponse();
            var t = _repo.Get(e => e.Name.Equals(equipment.Name)).FirstOrDefault();
            if (t != null)
            {
                result.Status = 400;
                result.Message = "Tên equipment trùng với equipment có id " + _mapper.Map<Equipment>(t).Id;
                _repo.DetachEntity(t);
                return result;
            }
            _repo.Create(_mapper.Map<Equipment>(equipment));
            result.Status = 200;
            result.Message = "OK";
            return result;
        }

        public ResponseObject DeleteEquipmentById(int id)
        {
            var result = new PostRequestResponse();
            if (_repo.GetById(id) == null)
            {
                result.Status = 400;
                result.Message = "Không tìm thấy ID";
                return result;
            }
            _repo.Delete(_repo.GetById(id));
            result.Status = 200;
            result.Message = "Đã xoá";
            return result;
        }

        public Equipment GetEquipmentById(int id)
        {
            return _repo.GetById(id);
        }

        public List<Equipment> GetEquipments()
        {
            return _repo.GetAll().ToList();
        }

        public ResponseObject UpdateEquipment(EquipmentRequest equipment)
        {
            var result = new PostRequestResponse();
            var t = _repo.GetById(equipment.Id);
            if (t == null)
            {
                result.Status = 404;
                result.Message = "Không tìm thấy id";
                //_repo.DetachEntity(t);
                return result;
            }
            _repo.DetachEntity(t);
            if (string.IsNullOrWhiteSpace(equipment.Name))
            {
                result.Status = 400;
                result.Message = "Tên trống";
                return result;
            }
            var t2 = _repo.Get(e => e.Name.Equals(equipment.Name)).FirstOrDefault();
            if (t2!= null && t2.Id != t.Id)
            {
                result.Status = 200;
                result.Message = "Trùng tên với equipment id "+t2.Id;
                _repo.DetachEntity(t2);
                return result;
            }
            _repo.Update(_mapper.Map<Equipment>(equipment));
            result.Status = 200;
            result.Message = "Đã cập nhật";
            return result;
        }
    }
}
