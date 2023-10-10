using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITCanCook_BusinessObject.ServiceModel.RequestModel
{
    public class MenuFilterRequest //lấy request là 3 id và lọc trả về 3 recipe tương ứng
    {
        public int EquipmentId { get; set; }
        public int HealthConditionId { get; set; }
        public int CookingHobbyId { get; set; }
    }
}
