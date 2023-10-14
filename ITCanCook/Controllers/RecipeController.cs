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

        [HttpGet("get-all")]
        public List<RecipeResponse> GetAllRecipe()
        {
            return _mapper.Map<List<RecipeResponse>>(_service.GetRecipes());
        }

        [HttpGet("get-by-id/{recipeId:int}")]
        public RecipeResponse GetRecipeById(int recipeId)
        {
            return _mapper.Map<RecipeResponse>(_service.GetRecipe(recipeId));
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


        /// <summary>
        /// Tạo menu 3 món cho 3 bữa dựa trên 3 loại ID nhập vào 
        /// </summary>
        /// <param name="request">Chứa ID cũa equipment, ID của health condition, ID của cooking hobby</param>
        /// <returns></returns>
        [HttpPost("menu")]
        public List<RecipeResponse> GetMenu([FromBody] MenuFilterRequest request)
        {
            return _service.FilterToMenu(request);
        }


        /// <summary>
        /// Lấy danh sách recipe dựa theo Id của equipment
        /// </summary>
        /// <param name="equipmentId">1 ID của equipment</param>
        /// <returns></returns>
        [HttpGet("equipment/{equipmentId:int}")]
        public List<RecipeResponse> GetRecipesByEquipmentId(int equipmentId)
        {
            return _mapper.Map<List<RecipeResponse>>(_service.GetRecipesByEquipmentId(equipmentId));
        }

        // recipes?healthId=1
        // recipes => List
        // recipes?pram => filter List HttpGet() 
        // recipes/{id} => get one by id
        // recipes/{recipeId}/ingredients => list ingredient
        /// <summary>
        /// Lấy danh sách recipe dựa theo Id của Health condition
        /// </summary>
        /// <param name="healthId">1 Id của health condition</param>
        /// <returns></returns>
        [HttpGet("health/{healthId:int}")]
        public List<RecipeResponse> GetRecipesByHealthConditionId(int healthId)//, [FromQuery] int heal)
        {
            return _mapper.Map<List<RecipeResponse>>(_service.GetRecipesByHealthConditionId(healthId));
        }
        /// <summary>
        /// Lấy danh sách recipe theo cooking hobby
        /// </summary>
        /// <param name="hobbyId">1 Id của cooking hobby</param>
        /// <returns></returns>
        [HttpGet("hobby/{hobbyId:int}")]
        public List<RecipeResponse> GetRecipesByCookingHobbyId(int hobbyId)
        {
            return _mapper.Map<List<RecipeResponse>>(_service.GetRecipesByCookingHobbyId(hobbyId));
        }


        /// <summary>
        /// Lấy trả về định lượng nguyên liệu của 1 công thức nhất định
        /// </summary>
        /// <param name="recipeId">1 Id của recipe</param>
        /// <returns></returns>
        [HttpGet("{recipeId:int}/recipeamount")]//cái này trả về thông tin tên thành phần và định lượng cho các recipe tương ứng
        public List<IngredientAmountResponse> GetRecipeIngredientDetailsByRecipeId(int recipeId)
        {
            return _service.GetIngredientAndAmountByRecipeId(recipeId);
        }
        /// <summary>
        /// Lấy món ăn theo các bữa trong ngày
        /// </summary>
        /// <param name="meal">Tên bữa ăn. vd: Sáng</param>
        /// <returns></returns>
        [HttpGet("meals")]
        public List<RecipeResponse> GetRecipesByMeal([FromQuery] string meal)
        {
            return _mapper.Map<List<RecipeResponse>>(_service.GetRecipesWithMeal(meal));
        }
        /// <summary>
        /// Lấy recipe theo nguyên liệu
        /// </summary>
        /// <param name="inputList">List id các nguyên liệu trong món ăn</param>
        /// <returns>list recipe</returns>
        [HttpPost("ingredients/list")]
        public List<RecipeResponse> GetRecipesByListIngredients([FromBody] List<int> inputList)
        {
            return _mapper.Map<List<RecipeResponse>>(_service.GetRecipeWithIngredientsList(inputList));
        }
    }
}
