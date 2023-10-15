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
    public class IngredientController : ControllerBase
    {
        private readonly IIngredientService _service;
        private readonly IMapper _mapper;

        public IngredientController(IIngredientService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        [HttpGet("get-all")]
        [Authorize]
        public List<IngredientResponse> GetAllIgredients()
        {
            return _mapper.Map<List<IngredientResponse>>(_service.GetIngredients());
        }

        [HttpGet("get-by-id/{ingredientId:int}")]
        [Authorize]
        public IngredientResponse GetIngredientById(int ingredientId)
        {
            return  _mapper.Map<IngredientResponse>(_service.GetIngredientById(ingredientId));
        }

        [HttpGet("{categoryId:int}/details")]
        [Authorize]
        public List<IngredientResponse> GetIngredientsByCategoryId(int categoryId)
        {
            return _mapper.Map<List<IngredientResponse>>(_service.GetIngredientsByCategoryId(categoryId));
        }

        [HttpGet("{categoryId:int}/names")]
        [Authorize]
        public List<string> GetIngredientNamesByCategoryId(int categoryId)
        {
            return _mapper.Map<List<string>>(_service.GetIngredientNamesByCategoryId(categoryId));
        }

        [HttpPost("create")]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateIngredient([FromBody] IngredientCreateRequest ingredient)
        {
            var result = _service.CreateIngredient(ingredient);
            var statusCode = (int)result.Status;
            var jsonResult = new JsonResult(result)
            {
                StatusCode = statusCode
            };
            return jsonResult;
        }

        [HttpPut("update")]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateIngredient([FromBody] IngredientRequest ingredient)
        {
            var result =  _service.UpdateIngredient(ingredient);
            var statusCode = (int)result.Status;
            var jsonResult = new JsonResult(result)
            {
                StatusCode = statusCode
            };
            return jsonResult;

        }

        [HttpDelete("delete/{ingredientId:int}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteIngredientById(int ingredentId)
        {
            var result = _service.DeleteIngredientById(ingredentId);
            var statusCode = (int)result.Status;
            var jsonResult = new JsonResult(result)
            {
                StatusCode = statusCode
            };
            return jsonResult;
        }

    }
}
