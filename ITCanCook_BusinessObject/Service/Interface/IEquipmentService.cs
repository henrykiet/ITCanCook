using ITCanCook_DataAcecss.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITCanCook_BusinessObject.Service.Interface
{
    public interface IEquipmentService
    {
        public List<Equipment> GetRecipies();
        public Equipment GetRecipeById(int id);
        public bool CreateRecipe(Equipment recipe);
        public bool UpdateRecipe(Equipment recipe);
        public bool DeleteRecipeById(int id);
    }
}
