using AutoMapper;
using ITCanCook_BusinessObject.Service.Interface;
using ITCanCook_BusinessObject.ServiceModel.RequestModel;
using ITCanCook_BusinessObject.ServiceModel.ResponseModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Data;

namespace ITCanCook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthConditionCategoryController : ControllerBase
    {
        private readonly IHealthConditionCategoryService _service;
        private readonly IMapper _mapper;
        public HealthConditionCategoryController(IHealthConditionCategoryService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("get-all")]
        public List<HealthConditionCategoryResponse> GetAllCategory()
        {
            return _mapper.Map<List<HealthConditionCategoryResponse>>(_service.GetHealthConditionCategories());
        }

        [HttpGet("get-by-id/{categoryId:int}")]
        public HealthConditionCategoryResponse GetCategoryById(int categoryId)
        {
            return _mapper.Map<HealthConditionCategoryResponse>(categoryId);
        }

        [HttpPost("create")]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateHealthCategory([FromBody] HealthConditionCategoryCreateRequest category)
        {
            var result = _service.CreateHealthConditionCategory(category);
            var statusCode = (int)result.Status;
            var jsonResult = new JsonResult(result)
            {
                StatusCode = statusCode
            };
            return jsonResult;
        }

        [HttpPut("update")]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateHealthCategory([FromBody] HealthConditionCategoryRequest category)
        {
            var result = _service.UpdateHealthConditionCategory(category);
            var statusCode = (int)result.Status;
            var jsonResult = new JsonResult(result)
            {
                StatusCode = statusCode
            };
            return jsonResult;
        }

        [HttpDelete("delete/{categoryId:int}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteHealthCategory(int categoryId)
        {
            var result = _service.DeleteHealthConditionCategoryById(categoryId);
            var statusCode = (int)result.Status;
            var jsonResult = new JsonResult(result)
            {
                StatusCode = statusCode
            };
            return jsonResult;
        }
    }
}
