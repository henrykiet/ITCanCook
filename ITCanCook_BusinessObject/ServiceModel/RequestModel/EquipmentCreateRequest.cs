using ITCanCook_DataAcecss.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITCanCook_BusinessObject.ServiceModel.RequestModel
{
    public class EquipmentCreateRequest
    {
        public string Name { get; set; }
        public bool IsEquipment { get; set; }
        public EquipmentStatus Status { get; set; }
    }
}
