using ITCanCook_DataAcecss.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITCanCook_BusinessObject.ServiceModel.ResponseModel
{
    public class EquipmentResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsEquipment { get; set; }
        public EquipmentStatus Status { get; set; }
    }
}
