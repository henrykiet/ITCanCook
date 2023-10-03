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
			CreateMap<CookingHobby, CookingHobbyRequest>().ReverseMap();
			CreateMap<CookingHobby, CookingHobbyCreateRequest>().ReverseMap();
			CreateMap<IngredientCategory, IngredientCategoryRequest>().ReverseMap();
			CreateMap<IngredientCategory, IngredientCategoryCreateRequest>().ReverseMap();
			#endregion

			#region Data <-> response
			CreateMap<CookingHobby, CookingHobbyResponse>().ReverseMap();
			CreateMap<IngredientCategory, IngredientCategoryResponse>().ReverseMap();
			#endregion

			#region Request <-> Response

			#endregion
		}
	}
}
