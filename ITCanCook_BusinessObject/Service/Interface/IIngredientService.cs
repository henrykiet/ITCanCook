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
    public interface IIngredientService
    {
        public List<Ingredient> GetIngredients();
        public Ingredient GetIngredientById(int id);
        public ResponseObject CreateIngredient(IngredientCreateRequest ingredient);
        public ResponseObject UpdateIngredient(IngredientRequest ingredient);
        public ResponseObject DeleteIngredientById(int id);
    }
}
