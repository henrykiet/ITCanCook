using ITCanCook_DataAcecss.Entities;
using ITCanCook_DataAcecss.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace ITCanCook_DataAcecss.Repository.Implement
{
	public interface ICookingHobbyRepo : IBaseRepository<CookingHobby>
    {
    }

    public class CookingHobbyRepository : BaseRepository<CookingHobby>, ICookingHobbyRepo
	{
        public CookingHobbyRepository(DbContext context) : base(context)
        {
        }
    }
}
