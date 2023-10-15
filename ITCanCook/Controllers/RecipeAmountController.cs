using AutoMapper;
using ITCanCook_BusinessObject.Service.Interface;
using ITCanCook_BusinessObject.ServiceModel.RequestModel;
using ITCanCook_BusinessObject.ServiceModel.ResponseModel;
using ITCanCook_DataAcecss.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;

namespace ITCanCook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeAmountController : ControllerBase
    {
        private readonly IRecipeAmountService _service;
        private readonly IMapper _mapper;

        public RecipeAmountController(IRecipeAmountService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("get-all")]
        [Authorize]
        public List<RecipeAmountResponse> GetAll()
        {
            return _mapper.Map<List<RecipeAmountResponse>>(_service.GetRecipeAmounts());
        }

        [HttpGet("get-by-id/{amountId:int}")]
        [Authorize]
        public RecipeAmountResponse GetAmountById(int amountId)
        {
            return _mapper.Map<RecipeAmountResponse>(_service.GetRecipeAmountById(amountId)) ;
        }

        
        //[HttpGet("get-by-recipe-id/{recipeId:int}")]
        /// <summary>
        /// Lấy danh sách định lượng nguyên liệu dựa trên Recipe ID - trả về đầy đủ các thông tin
        /// </summary>
        /// <param name="recipeId">ID của recipe</param>
        /// <returns></returns>
        [HttpGet("recipes/{recipeId:int}/amounts")]
        [Authorize]
        // ingredent + amount
        public List<RecipeAmountResponse> GetAmountByRecipeId(int recipeId)
        {
            return _mapper.Map<List<RecipeAmountResponse>>(_service.GetAmountByRecipeId(recipeId));
        }

        [HttpPost("create")]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateRecipeAmount([FromBody] RecipeAmountCreateRequest amount)
        {
            var result = _service.CreateRecipeAmount(amount);
            var statusCode = (int)result.Status;
            var jsonResult = new JsonResult(result)
            {
                StatusCode = statusCode
            };
            return jsonResult;
        }

        [HttpPut("update")]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateRecipeAmount([FromBody] RecipeAmountRequest amount)
        {
            var result = _service.UpdateRecipeAmount(amount);
            var statusCode = (int)result.Status;
            var jsonResult = new JsonResult(result)
            {
                StatusCode = statusCode
            };
            return jsonResult;
        }

        [HttpDelete("delete/{amountId:int}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteRecipeAmount(int amountId)
        {
            var result = _service.DeleteRecipeAmountById(amountId);
            var statusCode = (int)result.Status;
            var jsonResult = new JsonResult(result)
            {
                StatusCode = statusCode
            };
            return jsonResult;
        }
    }
}
