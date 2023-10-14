using AutoMapper;
using ITCanCook_BusinessObject.Service.Interface;
using ITCanCook_BusinessObject.ServiceModel.RequestModel;
using ITCanCook_BusinessObject.ServiceModel.ResponseModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ITCanCook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeStepController : ControllerBase
    {
        private readonly IRecipeStepService _service;
        private readonly IMapper _mapper;
        public RecipeStepController(IRecipeStepService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("get-all")]
        public List<RecipeStepResponse> GetAllSteps()
        {
            return _mapper.Map<List<RecipeStepResponse>>(_service.GetRecipeSteps());
        }
        [HttpGet("get-by-id/{stepId:int}")]
        public RecipeStepResponse GetById(int stepId)
        {
            return _mapper.Map<RecipeStepResponse>(_service.GetRecipeStepById(stepId));
        }

        [HttpPost("create")]
        public IActionResult CreateStep([FromBody] RecipeStepCreateRequest step)
        {
            var result = _service.CreateRecipeStep(step);
            var statusCode = (int)result.Status;
            var jsonResult = new JsonResult(result)
            {
                StatusCode = statusCode
            };
            return jsonResult;
        }

        [HttpPost("update")]
        public IActionResult UpdateStep([FromBody] RecipeStepRequest step)
        {
            var result = _service.UpdateRecipeStep(step);
            var statusCode = (int)result.Status;
            var jsonResult = new JsonResult(result)
            {
                StatusCode = statusCode
            };
            return jsonResult;
        }

        [HttpPost("delete/{stepId:int}")]
        public IActionResult DeleteStep(int stepId)
        {
            var result = _service.DeleteRecipeStepById(stepId);
            var statusCode = (int)result.Status;
            var jsonResult = new JsonResult(result)
            {
                StatusCode = statusCode
            };
            return jsonResult;
        }

        [HttpGet("get-by-recipe-id/{recipeId:int}")]
        public List<RecipeStepResponse> GetStepsByRecipeId(int recipeId)
        {
            return _mapper.Map<List<RecipeStepResponse>>(_service.GetStepByRecipeId(recipeId));
        }
    }
}
