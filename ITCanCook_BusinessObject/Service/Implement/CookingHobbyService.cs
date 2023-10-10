using AutoMapper;
using ITCanCook_BusinessObject.ResponseObjects;
using ITCanCook_BusinessObject.ResponseObjects.Abstraction;
using ITCanCook_BusinessObject.Service.Interface;
using ITCanCook_BusinessObject.ServiceModel.RequestModel;
using ITCanCook_DataAcecss.Entities;
using ITCanCook_DataAcecss.Repository.Implement;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITCanCook_BusinessObject.Service.Implement
{
    public class CookingHobbyService : ICookingHobbyService
    {
        private readonly ICookingHobbyRepo _repo;
		private readonly IMapper _mapper;
        public CookingHobbyService(ICookingHobbyRepo repo, IMapper mapper)
        {
            _repo = repo;
			_mapper = mapper;
        }

        public List<CookingHobby> GetCookingHobbys()
        {
            return _repo.GetAll().ToList();
        }

        public CookingHobby GetCookingHobbyById(int id)
        {
            return _repo.GetById(id);
        }
        #region Tạo mới
        public ResponseObject CreateCookingHobby(CookingHobbyCreateRequest hobby)
		{
			var result = new PostRequestResponse();
			hobby.Name.Trim();//loại bỏ các khoảng trống hai đầu
            if (string.IsNullOrWhiteSpace(hobby.Name))//kiểm tra chuỗi rỗng
            {
                result.Status = 400;
                result.Message = "Bad request! Không nhận chuỗi trống";
                return result;
            }
            if (_repo.Get(c => c.Name.Contains(hobby.Name)).Count() > 0)
			{
				result.Status = 400;
				result.Message = "Bad request! Không trùng lặp tên sở thích";
				return result;
			}
			_repo.Create(_mapper.Map<CookingHobby>(hobby));
            result.Status = 200;
            result.Message = "OK! Tạo thành công";
            return result;
        }
        #endregion
        #region Cập nhật
        public ResponseObject UpdateCookingHobby(CookingHobbyRequest hobby)
		{
			var result=new PostRequestResponse();
			hobby.Name.Trim();
			var t = _repo.GetById(hobby.Id);
			if (t == null)
			{
                result.Status = 404;
                result.Message = "Not found! Không tồn tại ID cần cập nhật";
                //_repo.DetachEntity(t);
                return result;
            }
            if (string.IsNullOrWhiteSpace(hobby.Name))
            {
                result.Status = 400;
                result.Message = "Bad request! Không nhận chuỗi trống";
                return result;
            }
            var t2 = _repo.Get(h => h.Name.Equals(hobby.Name)).FirstOrDefault();
            if(t2 != null && t2.Id != t.Id)
            {
                result.Status = 400;
                result.Message = "Bad request! Không trùng lặp tên sở thích "+t2.Id;
                _repo.DetachEntity(t2);
                return result;
            }
			_repo.DetachEntity(t);
			_repo.Update(_mapper.Map<CookingHobby>(hobby));
            result.Status = 200;
            result.Message = "OK! Cập nhật thành công";
            return result;
        }
        #endregion
        #region Xoá - lưu ý khi dùng, chỉ nên dùng update thay đổi status
        public ResponseObject DeleteCookingHobbyById(int id)
		{
            var result = new PostRequestResponse();
			var hobby = _repo.GetById(id);
			if (hobby == null)
			{
                result.Status = 404;
                result.Message = "Not found! Không tồn tại ID cần xoá";
                return result;
            }
			_repo.Delete(hobby);
            result.Status = 200;
            result.Message = "OK! Đã xoá";
            return result;
        }
        #endregion
    }
}
