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
    public class RecipeAmountService : IRecipeAmountService
    {
        private readonly IRecipeAmountRepo _repo;
        private readonly IRecipeRepo _recipeRepo;
        private readonly IIngredientRepo _ingredientRepo;
        private readonly IMapper _mapper;

        public RecipeAmountService(IRecipeAmountRepo repo, IRecipeRepo recipeRepo, IIngredientRepo ingredientRepo, IMapper mapper)
        {
            _repo = repo;
            _recipeRepo = recipeRepo;
            _ingredientRepo = ingredientRepo;
            _mapper = mapper;
        }

        public ResponseObject CreateRecipeAmount(RecipeAmountCreateRequest amount)
        {
            var result = new PostRequestResponse();
            if (amount == null)
            {
                result.Status = 400;
                result.Message = "Trống thông tin";
                return result;
            }
            if (_recipeRepo.GetById(amount.RecipeId) == null)
            {
                result.Status = 400;
                result.Message = "Recipe Id không tồn tại";
                return result;
            }
            if (_ingredientRepo.GetById(amount.IngredientId) == null)
            {
                result.Status = 400;
                result.Message = "Ingredient Id không tồn tại";
                return result;
            }
            _repo.Create(_mapper.Map<RecipeAmount>(amount));
            result.Status = 200;
            result.Message = "OK";
            return result;
        }

        public ResponseObject DeleteRecipeAmountById(int id)
        {
            var result = new PostRequestResponse();
            var t = _repo.GetById(id);
            if(t == null)
            {
                result.Status = 400;
                result.Message = "Id không tồn tại";
                return result;
            }
            _repo.DetachEntity(t);
            _repo.Delete(_repo.GetById(id));
            result.Status = 400;
            result.Message = "Trống thông tin";
            return result;
        }

        public RecipeAmount GetRecipeAmountById(int id)
        {
            return _repo.GetById(id);
        }

        public List<RecipeAmount> GetRecipeAmounts()
        {
            return _repo.GetAll().ToList();
        }

        public ResponseObject UpdateRecipeAmount(RecipeAmountRequest amount)
        {
            var result = new PostRequestResponse();
            var t = _repo.GetById(amount.Id);
            if (t == null)
            {
                result.Status = 400;
                result.Message = "Id không tồn tại";
                return result;
            }
            _repo.DetachEntity(t);
            if (_recipeRepo.GetById(amount.RecipeId) == null)
            {
                result.Status = 400;
                result.Message = "Recipe Id không tồn tại";
                return result;
            }
            if (_ingredientRepo.GetById(amount.IngredientId) == null)
            {
                result.Status = 400;
                result.Message = "Ingredient Id không tồn tại";
                return result;
            }
            _repo.Update(_mapper.Map<RecipeAmount>(amount));
            result.Status = 200;
            result.Message = "OK";
            return result;
        }
    }
}
