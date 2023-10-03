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
    //public interface ICookingMethodService
    //{
    //    public List<CookingMethod> GetCookingMethods();
    //    public CookingMethod GetCookingMethodById(int id);
    //    public bool CreateCookingMethod(CookingMethod method);
    //    public bool UpdateCookingMethod(CookingMethod method);
    //    public bool DeleteCookingMethodById(int id);

    //}
    public class CookingHobbyService : ICookingHobbyService
    {
        private readonly ICookingHobbyRepo _repo;
        public CookingHobbyService(ICookingHobbyRepo repo)
        {
            _repo = repo;
        }

        public List<CookingHobby> GetCookingMethods()
        {
            return _repo.GetAll().ToList();
        }

        public CookingHobby GetCookingMethodById(int id)
        {
            return _repo.GetById(id);
        }

		public bool CreateCookingHobby(CookingHobby method)
		{
			method.Name.Trim();//loại bỏ các khoảng trống hai đầu
			if (_repo.Get(c => c.Name.Contains(method.Name)).Count() > 0 ||
				string.IsNullOrWhiteSpace(method.Name))//kiểm tra trùng tên và chuỗi rỗng
			{
				return false;
			}
			_repo.Create(method);
			return true;
		}
		public bool UpdateCookingHobby(CookingHobby method)
		{
			method.Name.Trim();
			var t = _repo.GetById(method.Id);
			if (t == null || string.IsNullOrWhiteSpace(t.Name))
			{
				return false;
			}
			_repo.DetachEntity(t);
			_repo.Update(method);
			return true;
		}

		public bool DeleteCookingHobbyById(int id)
		{
			var method = _repo.GetById(id);
			if (method == null)
			{
				return false;
			}
			_repo.Delete(method);
			return true;
		}
	}
}
