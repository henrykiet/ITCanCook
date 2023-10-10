using AutoMapper;
using ITCanCook_BusinessObject.Service.Interface;
using ITCanCook_BusinessObject.ServiceModel.ResponseModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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
        public List<RecipeAmountResponse> GetAll()
        {
            return _mapper.Map<List<RecipeAmountResponse>>(_service.GetRecipeAmounts());
        }

        [HttpGet("get-by-id/{amountId:int}")]
        public RecipeAmountResponse GetAmountById(int amountId)
        {
            return _mapper.Map<RecipeAmountResponse>(_service.GetRecipeAmountById(amountId)) ;
        }

        //[HttpPost]
    }
}
