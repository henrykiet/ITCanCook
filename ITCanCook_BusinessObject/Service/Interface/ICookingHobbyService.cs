using ITCanCook_BusinessObject.ResponseObjects.Abstraction;
using ITCanCook_BusinessObject.ServiceModel.RequestModel;
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
        public List<CookingHobby> GetCookingHobbys();
        public CookingHobby GetCookingHobbyById(int id);
        public ResponseObject CreateCookingHobby(CookingHobbyCreateRequest hobby);
        public ResponseObject UpdateCookingHobby(CookingHobbyRequest hobby);
        public ResponseObject DeleteCookingHobbyById(int id);
    }
}
