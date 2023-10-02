using ITCanCook_DataAcecss.Entities;
using ITCanCook_DataAcecss.Repository.Interface;

namespace ITCanCook_DataAcecss.Repository.Implement
{
	public interface ICookingMethodRepo : IBaseRepository<CookingMethod>
    {
    }

    public class CookingMethodRepository : BaseRepository<CookingMethod>, ICookingMethodRepo
    {
        public CookingMethodRepository(DbContext context) : base(context)
        {
        }
    }
}
