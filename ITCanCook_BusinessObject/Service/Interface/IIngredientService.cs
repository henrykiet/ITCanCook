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
        public bool CreateIngredient(Ingredient ingredient);
        public bool UpdateIngredient(Ingredient ingredient);
        public bool DeleteIngredientById(int id);
    }
}
