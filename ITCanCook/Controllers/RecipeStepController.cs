using AutoMapper;
using ITCanCook_BusinessObject.Service.Interface;
using ITCanCook_BusinessObject.ServiceModel.RequestModel;
using ITCanCook_BusinessObject.ServiceModel.ResponseModel;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        public List<RecipeStepResponse> GetAllSteps()
        {
            return _mapper.Map<List<RecipeStepResponse>>(_service.GetRecipeSteps());
        }
        [HttpGet("get-by-id/{stepId:int}")]
        [Authorize]
        public RecipeStepResponse GetById(int stepId)
        {
            return _mapper.Map<RecipeStepResponse>(_service.GetRecipeStepById(stepId));
        }
        /// <summary>
        /// Trả về danh sách các bước nấu ăn dựa trên recipe ID - trả về đầy đủ thông tin step và các step được sắp xếp tăng dần sẵn
        /// </summary>
        /// <param name="recipeId"></param>
        /// <returns></returns>
        [HttpGet("get-by-recipe-id/{recipeId:int}")]
        [Authorize]
        public List<RecipeStepResponse> GetStepsByRecipeId(int recipeId)
        {
            return _mapper.Map<List<RecipeStepResponse>>(_service.GetStepByRecipeId(recipeId));
        }

        [HttpPost("create")]
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles ="Admin")]
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
    }
}
