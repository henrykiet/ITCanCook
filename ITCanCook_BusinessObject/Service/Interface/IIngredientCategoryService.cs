using ITCanCook_BusinessObject.ResponseObjects.Abstraction;
using ITCanCook_BusinessObject.ServiceModel.RequestModel;
using ITCanCook_DataAcecss.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITCanCook_BusinessObject.Service.Interface
{
    public interface IIngredientCategoryService
    {
        public List<IngredientCategory> GetIngredientCategories();
        public IngredientCategory GetIngredientCategoryById(int id);
        public ResponseObject CreateIngredientCategory(IngredientCategoryCreateRequest category);
        public ResponseObject UpdateIngredientCategory(IngredientCategoryRequest category);
        public ResponseObject DeleteIngredientCategoryById(int id);
    }
}
