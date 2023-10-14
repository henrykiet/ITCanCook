using AutoMapper;
using ITCanCook_BusinessObject.Service.Interface;
using ITCanCook_BusinessObject.ServiceModel.RequestModel;
using ITCanCook_BusinessObject.ServiceModel.ResponseModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Collections.Generic;

namespace ITCanCook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthConditionController : ControllerBase
    {
        private readonly IHealthConditionService _service;
        private readonly IMapper _mapper;

        public HealthConditionController(IHealthConditionService service, IMapper mapper)
        {
            _mapper = mapper;
            _service = service;
        }

        [HttpGet("get-all")]
        public List<HealthConditionResponse> GetAllHealthCondition()
        {
            return _mapper.Map<List<HealthConditionResponse>>(_service.GetHealthConditions());
        }

        [HttpGet("get-by-id/{healthId:int}")]
        public HealthConditionResponse GetHealthConditionById(int healthId)
        {
            return _mapper.Map<HealthConditionResponse>(_service.GetHealthConditionById(healthId));
        }

        [HttpPost("create")]
        public IActionResult CreateRecipe([FromBody] HealthConditionCreateRequest request)
        {
            var result = _service.CreateHealthCondition(request);
            var statusCode = (int)result.Status;
            var jsonResult = new JsonResult(result)
            {
                StatusCode = statusCode
            };
            return jsonResult;
        }

        [HttpPut("update")]
        public IActionResult UpdateHealthCondition([FromBody] HealthConditionRequest request)
        {
            var result = _service.UpdateHealthCondition(request);
            var statusCode = (int)result.Status;
            var jsonResult = new JsonResult(result)
            {
                StatusCode = statusCode
            };
            return jsonResult;
        }

        [HttpDelete("delete/{healthId:int}")]
        public IActionResult DeleteHealthCondition(int healthId)
        {
            var result = _service.DeleteHealthConditionById(healthId);
            var statusCode = (int)result.Status;
            var jsonResult = new JsonResult(result)
            {
                StatusCode = statusCode
            };
            return jsonResult;
        }

        [HttpGet("get-by-category-id/{categoryId:int}")]
        public List<HealthConditionResponse> GetHealthConditionsByCategoryId(int categoryId)
        {
            return _mapper.Map<List<HealthConditionResponse>>(categoryId);
        }
    }
}
