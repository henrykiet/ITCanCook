using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITCanCook_BusinessObject.ServiceModel.RequestModel
{
    public class HealthConditionCategoryCreateRequest
    {
        public string Name { get; set; }
        public int IndexDisplay { get; set; }
    }
}
