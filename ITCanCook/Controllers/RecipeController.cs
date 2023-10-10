using AutoMapper;
using ITCanCook_BusinessObject.Service.Implement;
using ITCanCook_BusinessObject.ServiceModel.RequestModel;
using ITCanCook_BusinessObject.ServiceModel.ResponseModel;
using ITCanCook_DataAcecss.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ITCanCook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly IRecipeService _service;
        private readonly IMapper _mapper;
        public RecipeController(IRecipeService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        [HttpPost("create")]
        public IActionResult CreateRecipe([FromBody] RecipeCreateRequest recipe)
        {
            var result = _service.CreateRecipe(recipe);
            var statusCode = (int)result.Status;
            var jsonResult = new JsonResult(result)
            {
                StatusCode = statusCode
            };
            return jsonResult;
        }

        [HttpPut("update")]
        public IActionResult UpdateRecipe([FromBody] RecipeRequest recipe)
        {
            var result = _service.UpdateRecipe(recipe);
            var statusCode = (int)result.Status;
            var jsonResult = new JsonResult(result)
            {
                StatusCode = statusCode
            };
            return jsonResult;
        }

        [HttpDelete("delete/{recipeId:int}")]
        public IActionResult DeleteRecipe(int recipeId)
        {
            var result = _service.DeleteRecipe(recipeId);
            var statusCode = (int)result.Status;
            var jsonResult = new JsonResult(result)
            {
                StatusCode = statusCode
            };
            return jsonResult;
        }

        [HttpPost("menu")]
        public List<RecipeResponse> GetMenu([FromBody] MenuFilterRequest request)
        {
            return _service.FilterToMenu(request);
        }

        [HttpGet("get-all-recipes")]
        public List<RecipeResponse> GetAllRecipe()
        {
            return _mapper.Map<List<RecipeResponse>>(_service.GetRecipes());
        }


    }
}
