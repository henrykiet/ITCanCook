using AutoMapper;
using ITCanCook_BusinessObject.ServiceModel.RequestModel;
using ITCanCook_BusinessObject.ServiceModel.ResponseModel;
using ITCanCook_DataAcecss.Entities;

namespace ITCanCook.Mapping
{
    public class ModelMappers : Profile
    {
        public ModelMappers()
        {
            #region Data <-> Request
            CreateMap<CookingMethod, CookingMethodRequest>().ReverseMap();
            CreateMap<CookingMethod, CookingMethodCreateRequest>().ReverseMap();
            #endregion

            #region Data <-> response
            CreateMap<CookingMethod, CookingMethodResponse>().ReverseMap();
            #endregion

            #region Request <-> Response

            #endregion
        }
    }
}
