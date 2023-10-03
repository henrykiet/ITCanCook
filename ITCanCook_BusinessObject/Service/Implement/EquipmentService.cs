using ITCanCook_BusinessObject.Service.Interface;
using ITCanCook_DataAcecss.Entities;
using ITCanCook_DataAcecss.Repository.Implement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITCanCook_BusinessObject.Service.Implement
{

    internal class EquipmentService : IEquipmentService
    {
        private readonly IEquipmentRepo _repo;
        public EquipmentService(IEquipmentRepo repo)
        {
            _repo = repo;
        }

        public bool CreateRecipe(Equipment recipe)
        {
            if (_repo.GetById(recipe.Id) != null)
            {
                return false;
            }
            _repo.Create(recipe);
            return true;
        }

        public bool DeleteRecipeById(int id)
        {
            if (_repo.GetById(id) == null)
            {
                return false;
            }
            _repo.Delete(_repo.GetById(id));
            return true;
        }

        public Equipment GetRecipeById(int id)
        {
            return _repo.GetById(id);
        }

        public List<Equipment> GetRecipies()
        {
            return _repo.GetAll().ToList();
        }

        public bool UpdateRecipe(Equipment recipe)
        {
            if (_repo.GetById(recipe.Id) == null)
            {
                return false;
            }
            _repo.Update(recipe);
            return true;
        }
    }
}
