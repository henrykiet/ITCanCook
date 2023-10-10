using ITCanCook_DataAcecss.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITCanCook_BusinessObject.ServiceModel.RequestModel
{
    public class RecipeCreateRequest
    {
        public int EquipmentId { get; set; }
        public int HealthConditionId { get; set; }
        public int CookingHobbyId { get; set; }
        public string ImgLink { get; set; }
        public int CookingTime { get; set; }
        public string? Description { get; set; }
        public int ServingSize { get; set; }
        public int Energy { get; set; }
        public string Name { get; set; }
        public string Meals { get; set; }
    }
}
