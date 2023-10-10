using AutoMapper;
using ITCanCook_BusinessObject.Service.Interface;
using ITCanCook_BusinessObject.ServiceModel.RequestModel;
using ITCanCook_BusinessObject.ServiceModel.ResponseModel;
using ITCanCook_DataAcecss.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Ocsp;

namespace ITCanCook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientCategoryController : ControllerBase
    {
        private readonly IIngredientCategoryService _service;
        private readonly IMapper _mapper;
        public IngredientCategoryController(IIngredientCategoryService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        [HttpGet("get-all")]
        public List<IngredientCategoryResponse> GetAllIngredientCategories()
        {
            return _mapper.Map<List<IngredientCategoryResponse>>(_service.GetIngredientCategories());
        }

        [HttpGet("get-by-id/{categoryId:int}")]
        public IngredientCategoryResponse GetIngredientCategoryById(int categoryId)
        {
            return _mapper.Map<IngredientCategoryResponse>(_service.GetIngredientCategoryById(categoryId));
        }

        [HttpPost("create")]
        public IActionResult CreateIngredientCategory([FromBody] IngredientCategoryCreateRequest category)
        {
            var result = _service.CreateIngredientCategory(category);
            var statusCode = (int)result.Status;
            var jsonResult = new JsonResult(result)
            {
                StatusCode = statusCode
            };
            return jsonResult;
        }

        [HttpPut("update")]
        public IActionResult UpdateIngredientCategory([FromBody] IngredientCategoryRequest category)
        {
            var result = _service.UpdateIngredientCategory(category);
            var statusCode = (int)result.Status;
            var jsonResult = new JsonResult(result)
            {
                StatusCode = statusCode
            };
            return jsonResult;
        }

        [HttpDelete("delete/{categoryId:int}")]
        public IActionResult DeleteIngredientCategoryById(int categoryId)
        {
            var result = _service.DeleteIngredientCategoryById(categoryId);
            var statusCode = (int)result.Status;
            var jsonResult = new JsonResult(result)
            {
                StatusCode = statusCode
            };
            return jsonResult;
        }
    }
}
