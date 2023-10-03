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
        public List<HealthCondition> GetRecipeStyles();
        public HealthCondition GetRecipeStyleById(int id);
        public bool CreateRecipeStyle(HealthCondition style);
        public bool UpdateRecipeStyle(HealthCondition style);
        public bool DeleteRecipeStyleById(int id);
    }
}
