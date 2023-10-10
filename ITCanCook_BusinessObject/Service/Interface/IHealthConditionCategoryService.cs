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
    public interface IHealthConditionCategoryService
    {
        public List<HealthConditionCategory> GetHealthConditionCategories();
        public HealthConditionCategory GetHealthConditionCategoryById(int id);
        public ResponseObject CreateHealthConditionCategory(HealthConditionCategoryCreateRequest category);
        public ResponseObject UpdateHealthConditionCategory(HealthConditionCategoryRequest category);
        public ResponseObject DeleteHealthConditionCategoryById(int id);
    }
}
