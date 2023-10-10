using ITCanCook_BusinessObject.ResponseObjects.Abstraction;
using ITCanCook_BusinessObject.ServiceModel.RequestModel;
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
        public List<Equipment> GetEquipments();
        public Equipment GetEquipmentById(int id);
        public ResponseObject CreateEquipment(EquipmentCreateRequest recipe);
        public ResponseObject UpdateEquipment(EquipmentRequest recipe);
        public ResponseObject DeleteEquipmentById(int id);
    }
}
