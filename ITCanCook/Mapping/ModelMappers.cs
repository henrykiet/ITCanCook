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

            CreateMap<Ingredient, IngredientRequest>().ReverseMap();
            CreateMap<Ingredient, IngredientCreateRequest>().ReverseMap();

            CreateMap<Equipment, EquipmentRequest>().ReverseMap();
            CreateMap<Equipment, EquipmentCreateRequest>().ReverseMap();

            CreateMap<HealthCondition, HealthConditionRequest>().ReverseMap();
            CreateMap<HealthCondition, HealthConditionCreateRequest>().ReverseMap();

            CreateMap<HealthConditionCategory, HealthConditionCategoryRequest>().ReverseMap();
            CreateMap<HealthConditionCategory, HealthConditionCategoryCreateRequest>().ReverseMap();

            CreateMap<RecipeAmount, RecipeAmountRequest>().ReverseMap();
            CreateMap<RecipeAmount, RecipeAmountCreateRequest>().ReverseMap();

            CreateMap<RecipeStep, RecipeStepRequest>().ReverseMap();
            CreateMap<RecipeStep, RecipeCreateRequest>().ReverseMap();
            CreateMap<Recipe, RecipeRequest>().ReverseMap();
            CreateMap<Recipe, RecipeCreateRequest>().ReverseMap();
            #endregion

            #region Data <-> response
            CreateMap<CookingHobby, CookingHobbyResponse>().ReverseMap();
			CreateMap<IngredientCategory, IngredientCategoryResponse>().ReverseMap();
            CreateMap<Ingredient, IngredientResponse>().ReverseMap();
            CreateMap<Equipment, EquipmentResponse>().ReverseMap();
            CreateMap<HealthCondition, HealthConditionResponse>().ReverseMap();
            CreateMap<HealthConditionCategory, HealthConditionCategoryResponse>().ReverseMap();
            CreateMap<RecipeAmount, RecipeAmountResponse>().ReverseMap();
            CreateMap<RecipeStep, RecipeStepResponse>().ReverseMap();
            CreateMap<Recipe,RecipeResponse>().ReverseMap();
            #endregion

            #region Request <-> Response

            #endregion
        }
	}
}
