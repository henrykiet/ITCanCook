using AutoMapper;
using ITCanCook_BusinessObject.ResponseObjects;
using ITCanCook_BusinessObject.ResponseObjects.Abstraction;
using ITCanCook_BusinessObject.Service.Interface;
using ITCanCook_BusinessObject.ServiceModel.RequestModel;
using ITCanCook_BusinessObject.ServiceModel.ResponseModel;
using ITCanCook_DataAcecss.Entities;
using ITCanCook_DataAcecss.Repository.Implement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITCanCook_BusinessObject.Service.Implement
{
    public interface IRecipeService
    {
        public Recipe GetRecipe(int id);
        public List<Recipe> GetRecipes();
        public ResponseObject CreateRecipe(RecipeCreateRequest recipe);
        public ResponseObject UpdateRecipe(RecipeRequest recipe);
        public ResponseObject DeleteRecipe(int id);
        public List<RecipeResponse> FilterToMenu(MenuFilterRequest request);
        public List<Recipe> GetRecipesByEquipmentId(int equipmentId);
        public List<Recipe> GetRecipesByHealthConditionId(int healthConditionId);
        public List<Recipe> GetRecipesByCookingHobbyId(int cookingHobbyId);
        //public List<Recipe> GetRecipesWithListAndMeal(List<Recipe> list, string meal);
        public List<Recipe> GetRecipesWithMeal(string meal);
        public List<IngredientAmountResponse> GetIngredientAndAmountByRecipeId(int recipeId);

        public List<Recipe> GetRecipeWithIngredientsList(List<int> ingredientsInputList);



    }
    public class RecipeService : IRecipeService
    {
        private readonly IRecipeRepo _repo;
        private readonly IMapper _mapper;
        private readonly IEquipmentRepo _eRepo;
        private readonly IHealthConditionRepo _hRepo;
        private readonly ICookingHobbyRepo _cRepo;
        private readonly IIngredientRepo _iRepo;
        private readonly IRecipeAmountRepo _aRepo;
        public RecipeService(IRecipeRepo repo, IMapper mapper, IEquipmentRepo eRepo, IHealthConditionRepo hRepo, ICookingHobbyRepo cRepo, IIngredientRepo iRepo, IRecipeAmountRepo aRepo)
        {
            _repo = repo;
            _mapper = mapper;
            _eRepo = eRepo;
            _hRepo = hRepo;
            _cRepo = cRepo;
            _iRepo = iRepo;
            _aRepo = aRepo;
        }
        public Recipe GetRecipe(int id)
        {
            return _repo.GetById(id);
        }

        public List<Recipe> GetRecipes()
        {
            List<Recipe> list = _repo.GetAll().ToList();
            if (list.Count == 0)
            {
                return null;
            }
            return list;
        }
        public ResponseObject CreateRecipe(RecipeCreateRequest recipe)
        {
            var result = new PostRequestResponse();
            if (_eRepo.GetById(recipe.EquipmentId) == null)
            {
                result.Status = 400;
                result.Message = "Equipment Id không tồn tại";
                return result;
            }
            if (_hRepo.GetById(recipe.HealthConditionId) == null)
            {
                result.Status = 400;
                result.Message = "HealthCondition Id không tồn tại";
                return result;
            }
            if (_cRepo.GetById(recipe.CookingHobbyId) == null)
            {
                result.Status = 400;
                result.Message = "Cooking hobby id không tồn tại";
                return result;
            }
            if (string.IsNullOrWhiteSpace(recipe.Name))
            {
                result.Status = 400;
                result.Message = "Tên trống";
                return result;
            }
            var t = _repo.Get(r => r.Name.Equals(recipe.Name)).FirstOrDefault();
            if (t != null)
            {
                result.Status = 400;
                result.Message = "Trùng tên với Recipe id " + t.Id;
                _repo.DetachEntity(t);
                return result;
            }
            result.Status = 200;
            result.Message = "OK";
            return result;
        }

        public ResponseObject UpdateRecipe(RecipeRequest recipe)
        {
            var result = new PostRequestResponse();

            if (_eRepo.GetById(recipe.EquipmentId) == null)
            {
                result.Status = 400;
                result.Message = "Equipment Id không tồn tại";
                return result;
            }
            if (_hRepo.GetById(recipe.HealthConditionId) == null)
            {
                result.Status = 400;
                result.Message = "HealthCondition Id không tồn tại";
                return result;
            }
            if (_cRepo.GetById(recipe.CookingHobbyId) == null)
            {
                result.Status = 400;
                result.Message = "Cooking hobby id không tồn tại";
                return result;
            }
            if (string.IsNullOrWhiteSpace(recipe.Name))
            {
                result.Status = 400;
                result.Message = "Tên trống";
                return result;
            }
            var t2 = _repo.Get(r => r.Name.Equals(recipe.Name)).FirstOrDefault();
            if (t2 != null && t2.Id != recipe.Id)
            {
                result.Status = 400;
                result.Message = "Trùng tên với recipe id " + t2.Id;
                _repo.DetachEntity(t2);
                return result;
            }
            result.Status = 200;
            result.Message = "OK";
            return result;
        }

        public ResponseObject DeleteRecipe(int id)
        {
            var result = new ResponseObject();
            if (_repo.GetById(id) == null)
            {
                result.Status = 404;
                result.Message = "Không tìm thấy id";
                return result;
            }
            _repo.Delete(_repo.GetById(id));
            result.Status = 200;
            result.Message = "OK";
            return result;
        }

        public List<RecipeResponse> FilterToMenu(MenuFilterRequest request)
        {
            #region cũ
            //int equipmentId = request.EquipmentId;
            //int healthConditionId = request.HealthConditionId;
            //int cookingHobbyId = request.CookingHobbyId;
            //List<string> mealsOrder = new List<string>{"Sáng", "Trưa", "Tối" };
            //List<Recipe> recipes = _repo.GetAll().ToList();
            //List<Recipe> filteredRecipes = new List<Recipe>();//list kết quả

            //foreach (var meal in mealsOrder)
            //{
            //    // Lọc danh sách công thức theo bữa ăn
            //    var mealRecipes = recipes.Where(r => r.Meals.Split(", ").Contains(meal));

            //    // Lọc danh sách công thức theo các tiêu chí khác
            //    var filteredMealRecipes = mealRecipes.Where(r => r.EquipmentId == equipmentId && r.HealthConditionId == healthConditionId && r.CookingHobbyId == cookingHobbyId);

            //    // Sắp xếp danh sách công thức theo thứ tự bữa ăn
            //    var sortedMealRecipes = filteredMealRecipes.OrderBy(r => r.Meals.Split(", ").ToList().IndexOf(meal));

            //    // Thêm công thức thích hợp vào danh sách kết quả
            //    filteredRecipes.AddRange(sortedMealRecipes.Take(3));
            //}
            //return _mapper.Map<List<RecipeResponse>>(filteredRecipes);
            #endregion
            #region cũ 2.0
            /*
            List<Recipe> recipes = _repo.GetAll().ToList();
            List<Recipe> recommendedRecipes = new List<Recipe>();
            int equipmentId = request.EquipmentId;
            int healthConditionId = request.HealthConditionId;
            int cookingHobbyId = request.CookingHobbyId;
            // Lọc công thức đáp ứng đủ 3 id
            var filteredRecipes = recipes.Where(r =>
                r.EquipmentId == equipmentId &&
                r.HealthConditionId == healthConditionId &&
                r.CookingHobbyId == cookingHobbyId).ToList();

            // Lọc công thức đáp ứng được 2 id
            if (filteredRecipes.Count < 3)
            {
                var twoIdRecipes = recipes.Where(r =>
                    (r.EquipmentId == equipmentId && r.HealthConditionId == healthConditionId) ||
                    (r.EquipmentId == equipmentId && r.CookingHobbyId == cookingHobbyId) ||
                    (r.HealthConditionId == healthConditionId && r.CookingHobbyId == cookingHobbyId)).ToList();

                filteredRecipes.AddRange(twoIdRecipes.Take(3 - filteredRecipes.Count));
            }

            // Lọc công thức đáp ứng được 1 id
            if (filteredRecipes.Count < 3)
            {
                var oneIdRecipes = recipes.Where(r =>
                    r.EquipmentId == equipmentId ||
                    r.HealthConditionId == healthConditionId ||
                    r.CookingHobbyId == cookingHobbyId).ToList();

                filteredRecipes.AddRange(oneIdRecipes.Take(3 - filteredRecipes.Count));
            }

            // Sắp xếp công thức theo thứ tự bữa ăn
            recommendedRecipes.AddRange(filteredRecipes.OrderBy(r =>
            {
                if (r.Meals.Contains("Sáng"))
                    return 0;
                if (r.Meals.Contains("Trưa"))
                    return 1;
                if (r.Meals.Contains("Tối"))
                    return 2;
                return 3;
            }));
            */
            #endregion
            List<Recipe> recipes = _repo.GetAll().ToList();
            List<Recipe> recommendedRecipes = new List<Recipe>();
            int equipmentId = request.EquipmentId;
            int healthConditionId = request.HealthConditionId;
            int cookingHobbyId = request.CookingHobbyId;

            HashSet<int> usedRecipes = new HashSet<int>(); // Lưu trữ các công thức đã sử dụng

            // Lọc công thức đáp ứng đủ 3 id
            var filteredRecipes = recipes.Where(r =>
                r.EquipmentId == equipmentId &&
                r.HealthConditionId == healthConditionId &&
                r.CookingHobbyId == cookingHobbyId).ToList();

            // Lọc công thức đáp ứng được 2 id
            if (filteredRecipes.Count < 3)
            {
                var twoIdRecipes = recipes.Where(r =>
                    (r.EquipmentId == equipmentId && r.HealthConditionId == healthConditionId) ||
                    (r.EquipmentId == equipmentId && r.CookingHobbyId == cookingHobbyId) ||
                    (r.HealthConditionId == healthConditionId && r.CookingHobbyId == cookingHobbyId)).ToList();

                foreach (var recipe in twoIdRecipes)
                {
                    if (filteredRecipes.Count >= 3)
                        break;

                    if (!usedRecipes.Contains(recipe.Id))
                    {
                        filteredRecipes.Add(recipe);
                        usedRecipes.Add(recipe.Id);
                    }
                }
            }

            // Lọc công thức đáp ứng được 1 id
            if (filteredRecipes.Count < 3)
            {
                var oneIdRecipes = recipes.Where(r =>
                    r.EquipmentId == equipmentId ||
                    r.HealthConditionId == healthConditionId ||
                    r.CookingHobbyId == cookingHobbyId).ToList();

                foreach (var recipe in oneIdRecipes)
                {
                    if (filteredRecipes.Count >= 3)
                        break;

                    if (!usedRecipes.Contains(recipe.Id))
                    {
                        filteredRecipes.Add(recipe);
                        usedRecipes.Add(recipe.Id);
                    }
                }
            }

            // Sắp xếp công thức theo thứ tự bữa ăn
            recommendedRecipes.AddRange(filteredRecipes.OrderBy(r =>
            {
                if (r.Meals.Contains("Sáng"))
                    return 0;
                if (r.Meals.Contains("Trưa"))
                    return 1;
                if (r.Meals.Contains("Tối"))
                    return 2;
                return 3;
            }));
            return _mapper.Map<List<RecipeResponse>>(recommendedRecipes.Take(3).ToList());
        }

        public List<Recipe> GetRecipesByEquipmentId(int equipmentId)
        {
            return _repo.Get(r => r.EquipmentId == equipmentId).ToList();
        }

        public List<Recipe> GetRecipesByHealthConditionId(int healthConditionId)
        {
            return _repo.Get(r => r.HealthConditionId == healthConditionId).ToList();
        }

        public List<Recipe> GetRecipesByCookingHobbyId(int cookingHobbyId)
        {
            return _repo.Get(r => r.CookingHobbyId == cookingHobbyId).ToList();
        }

        //public List<Recipe> GetRecipesWithListAndMeal(List<Recipe> list, string meal)//trả về 1 list từ 1 list cho trước và lọc ra các bữa ăn tương ứng
        //{
        //    return list.Where(r => r.Meals.Split(", ").Contains(meal)).ToList();
        //    //ví dụ meal là Trưa thì sẽ trả lại các recipe trong Meals của nó có chứ từ "Trưa"
        //}

        public List<Recipe> GetRecipesWithMeal(string meal)//chỉ trả lại
        {
            List<Recipe> list = _repo.GetAll().ToList();
            if (list.Count == 0)
            {
                return null;
            }
            return list.Where(r => r.Meals.Split(", ").Contains(meal)).ToList();
        }

        public List<IngredientAmountResponse> GetIngredientAndAmountByRecipeId(int recipeId)
        {
            List<RecipeAmount> listAmount = _aRepo.Get(a => a.RecipeId == recipeId).ToList();//list này chứa id của recipe, ingredientId và amount phù hợp vs recipeId nhập vào
            List<Ingredient> ingredients = _iRepo.GetAll().ToList();
            List<IngredientAmountResponse> list = new List<IngredientAmountResponse>();
            foreach (RecipeAmount amount in listAmount)
            {
                Ingredient? ingre = ingredients.FirstOrDefault(i => i.Id == amount.IngredientId);
                list.Add(new IngredientAmountResponse() { IngredientName = ingre.name, Amount = amount.Amount });
            }
            return list;

        }

        public List<Recipe> GetRecipeWithIngredientsList(List<int> ingredientsInputList)
        {
            List<RecipeAmount> listIngredients = _aRepo.GetAll().ToList();
            List<Recipe> listResult = new List<Recipe>();
            List<int> listIdResult = new List<int>();
            var recipeCounts = new Dictionary<int, int>();
            foreach (var ingredient in listIngredients)
            {
                if (ingredientsInputList.Contains(ingredient.IngredientId))
                {
                    if (!recipeCounts.ContainsKey(ingredient.RecipeId))
                    {
                        recipeCounts[ingredient.RecipeId] = 0;
                    }
                    recipeCounts[ingredient.RecipeId]++;
                }
            }
            listIdResult  = recipeCounts
        .OrderByDescending(pair => pair.Value)
        .ThenBy(pair => pair.Key)
        .Select(pair => pair.Key)
        .ToList();
            foreach(var sorted in listIdResult)
            {
                listResult.Add(_repo.GetById(sorted));
            }
            return listResult;
        }
    }
}
