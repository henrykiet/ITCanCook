using AutoMapper;
using ITCanCook_BusinessObject.Service.Interface;
using ITCanCook_BusinessObject.ServiceModel.RequestModel;
using ITCanCook_BusinessObject.ServiceModel.ResponseModel;
using ITCanCook_DataAcecss.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ITCanCook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CookingHobbyController : ControllerBase
    {
        private readonly ICookingHobbyService _service;
        private readonly IMapper _mapper;
        public CookingHobbyController(ICookingHobbyService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("get-all")]
        public List<CookingHobbyResponse> GetAllCookingHobbys()
        {
            return _mapper.Map<List<CookingHobbyResponse>>(_service.GetCookingHobbys());
        }

        [HttpGet("get-by-id/{hobbyId:int}")]
        public CookingHobbyResponse GetCookingHobbyById(int hobbyId)
        {
            return _mapper.Map<CookingHobbyResponse>(_service.GetCookingHobbyById(hobbyId));
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("create")]
        public IActionResult CreateCookingHobby([FromBody] CookingHobbyCreateRequest request)
        {
            var result = _service.CreateCookingHobby(request);
            var statusCode = (int)result.Status;
            var jsonResult = new JsonResult(result)
            {
                StatusCode = statusCode
            };
            return jsonResult;
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("delete/{hobbyId:int}")]
        public IActionResult DeleteCookingHobbyById(int hobbyId)//lưu ý hàm này sẽ xoá luôn record, khuyến cáo dùng update để chỉnh status thay vì delete
        {
            var result = _service.DeleteCookingHobbyById(hobbyId);
            var statusCode = (int)result.Status;
            var jsonResult = new JsonResult(result)
            {
                StatusCode = statusCode
            };
            return jsonResult;
        }

        [HttpPut("update")]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateCookingHobby([FromBody] CookingHobbyRequest request)
        {
            var result = _service.UpdateCookingHobby(request);
            var statusCode = (int)result.Status;
            var jsonResult = new JsonResult(result)
            {
                StatusCode = statusCode
            };
            return jsonResult;
        }

    }
}
