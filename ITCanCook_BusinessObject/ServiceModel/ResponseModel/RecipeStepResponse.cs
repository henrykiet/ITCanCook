using ITCanCook_DataAcecss.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITCanCook_BusinessObject.ServiceModel.ResponseModel
{
    public class RecipeStepResponse
    {
        public int Id { get; set; }
        public int RecipeId { get; set; }
        public int Index { get; set; }
        public string MediaURl { get; set; }
        public string? Description { get; set; }
    }
}
