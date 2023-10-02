using ITCanCook_DataAcecss.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITCanCook_BusinessObject.Service.Interface
{
    public interface ICookingMethodService
    {
        public List<CookingMethod> GetCookingMethods();
        public CookingMethod GetCookingMethodById(int id);
        public bool CreateCookingMethod(CookingMethod method);
        public bool UpdateCookingMethod(CookingMethod method);
        public bool DeleteCookingMethodById(int id);
    }
}
