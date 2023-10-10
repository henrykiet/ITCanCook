using AutoMapper;
using ITCanCook_BusinessObject.ResponseObjects;
using ITCanCook_BusinessObject.ResponseObjects.Abstraction;
using ITCanCook_BusinessObject.Service.Interface;
using ITCanCook_BusinessObject.ServiceModel.RequestModel;
using ITCanCook_DataAcecss.Entities;
using ITCanCook_DataAcecss.Repository.Implement;
using Org.BouncyCastle.Bcpg.OpenPgp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITCanCook_BusinessObject.Service.Implement
{
    
    public class RecipeStepService : IRecipeStepService
    {
        private readonly IRecipeStepRepo _repo;
        private readonly IRecipeRepo _recipeRepo;
        private readonly IMapper _mapper;

        public RecipeStepService(IRecipeStepRepo repo, IRecipeRepo recipeRepo, IMapper mapper)
        {
            _repo = repo;
            _recipeRepo = recipeRepo;
            _mapper = mapper;
        }

        public ResponseObject CreateRecipeStep(RecipeStepCreateRequest step)
        {
            var result = new PostRequestResponse();
            if (_recipeRepo.GetById(step.RecipeId) == null)
            {
                result.Status = 400;
                result.Message = "Bad request! Không có recipe tương ứng";
                return result;
            }
            if (string.IsNullOrWhiteSpace(step.Description))
            {
                result.Status = 400;
                result.Message = "Bad request! Chuỗi trống";
                return result;
            }
            _repo.Create(_mapper.Map<RecipeStep>(step));
            result.Status = 200;
            result.Message = "OK!";
            return result;
        }

        public ResponseObject DeleteRecipeStepById(int id)
        {
            var result = new PostRequestResponse();
            var t = _repo.GetById(id);
            if (t == null)
            {
                result.Status = 404;
                result.Message = "Not found! ID không tồn tại";
                return result;
            }
            _repo.DetachEntity(t);
            _repo.Delete((_repo.GetById(id)));
            result.Status = 200;
            result.Message = "OK!";
            return result;
        }

        public RecipeStep GetRecipeStepById(int id)
        {
            return _repo.GetById(id);
        }

        public List<RecipeStep> GetRecipeSteps()
        {
            return _repo.GetAll().ToList();
        }

        public ResponseObject UpdateRecipeStep(RecipeStepRequest step)
        {
            var result = new PostRequestResponse();
            if (_repo.GetById(step.Id) == null)
            {
                result.Status = 400;
                result.Message = "Bad request! Id không tồn tại";
                return result;
            }
            if (_recipeRepo.GetById(step.RecipeId) == null)
            {
                result.Status = 400;
                result.Message = "Bad request! Không có recipe tương ứng";
                return result;
            }
            if (string.IsNullOrWhiteSpace(step.Description))
            {
                result.Status = 400;
                result.Message = "Bad request! chuỗi trống";
                return result;
            }
            result.Status = 200;
            result.Message = "OK!";
            return result;
        }
    }
}
