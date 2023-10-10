using ITCanCook_DataAcecss.Entities;
using ITCanCook_DataAcecss.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITCanCook_BusinessObject.ServiceModel.RequestModel
{
    public class HealthConditionCreateRequest
    {
        public int HealthConditionCategoryId { get; set; }
        public string Name { get; set; }
        public bool IsHealthCondition { get; set; }
        public HealthConditionCategoryStatus Status { get; set; }
    }
}
