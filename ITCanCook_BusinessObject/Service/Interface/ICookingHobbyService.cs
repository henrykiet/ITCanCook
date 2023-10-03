using ITCanCook_DataAcecss.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITCanCook_BusinessObject.Service.Interface
{
    public interface ICookingHobbyService
	{
        public List<CookingHobby> GetCookingMethods();
        public CookingHobby GetCookingMethodById(int id);
        public bool CreateCookingHobby(CookingHobby method);
        public bool UpdateCookingHobby(CookingHobby method);
        public bool DeleteCookingHobbyById(int id);
    }
}
