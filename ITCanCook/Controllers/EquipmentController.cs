using AutoMapper;
using ITCanCook_BusinessObject.Service.Interface;
using ITCanCook_BusinessObject.ServiceModel.RequestModel;
using ITCanCook_BusinessObject.ServiceModel.ResponseModel;
using ITCanCook_DataAcecss.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace ITCanCook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentController : ControllerBase
    {
        private readonly IEquipmentService _service;
        private readonly IMapper _mapper;
        public EquipmentController(IEquipmentService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("get-all")]
        public List<EquipmentResponse> GetAllEquipments()
        {
            return _mapper.Map<List<EquipmentResponse>>(_service.GetEquipments());
        }

        [HttpGet("get-by-id/{hobbyId:int}")]
        public EquipmentResponse GetEquipmentById(int hobbyId)
        {
            return _mapper.Map<EquipmentResponse>(_service.GetEquipmentById(hobbyId));
        }

        [HttpPost("create")]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateEquipment([FromBody] EquipmentCreateRequest request)
        {
            var result = _service.CreateEquipment(request);
            var statusCode = (int)result.Status;
            var jsonResult = new JsonResult(result)
            {
                StatusCode = statusCode
            };
            return jsonResult;
        }

        [HttpDelete("delete/{hobbyId:int}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteEquipmentById(int hobbyId)//lưu ý hàm này sẽ xoá luôn record, khuyến cáo dùng update để chỉnh status thay vì delete
        {
            var result = _service.DeleteEquipmentById(hobbyId);
            var statusCode = (int)result.Status;
            var jsonResult = new JsonResult(result)
            {
                StatusCode = statusCode
            };
            return jsonResult;
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("update")]
        public IActionResult UpdateEquipment([FromBody] EquipmentRequest request)
        {
            var result = _service.UpdateEquipment(request);
            var statusCode = (int)result.Status;
            var jsonResult = new JsonResult(result)
            {
                StatusCode = statusCode
            };
            return jsonResult;
        }
    }
}
