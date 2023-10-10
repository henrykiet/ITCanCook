using ITCanCook_DataAcecss.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITCanCook_BusinessObject.ServiceModel.ResponseModel
{
    public class HealthConditionResponse
    {
        public int Id { get; set; }
        public int HealthConditionCategoryId { get; set; }
        public string Name { get; set; }
        public bool IsHealthCondition { get; set; }
        public HealthConditionCategoryStatus Status { get; set; }
    }
}
