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
    public class CookingMethodService : ICookingMethodService
    {
        private readonly ICookingMethodRepo _repo;
        public CookingMethodService(ICookingMethodRepo repo)
        {
            _repo = repo;
        }

        public List<CookingMethod> GetCookingMethods()
        {
            return _repo.GetAll().ToList();
        }

        public CookingMethod GetCookingMethodById(int id)
        {
            return _repo.GetById(id);
        }

        public bool CreateCookingMethod(CookingMethod method)
        {
            if (_repo.GetById(method.Id) != null)
            {
                return false;
            }
            _repo.Create(method);
            return true;
        }
        public bool UpdateCookingMethod(CookingMethod method)
        {
            if (_repo.GetById(method.Id) == null)
            {
                return false;
            }
            _repo.Update(method);
            return true;
        }

        public bool DeleteCookingMethodById(int id)
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
