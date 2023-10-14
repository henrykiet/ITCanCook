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
    public interface IHealthConditionService
    {
        public List<HealthCondition> GetHealthConditions();
        public HealthCondition GetHealthConditionById(int id);
        public ResponseObject CreateHealthCondition(HealthConditionCreateRequest style);
        public ResponseObject UpdateHealthCondition(HealthConditionRequest style);
        public ResponseObject DeleteHealthConditionById(int id);
        public List<HealthCondition> GetConditionByCategoryId(int categoryId);
    }
}
