using ITCanCook_DataAcecss.Entities;
using ITCanCook_DataAcecss.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITCanCook_DataAcecss.Repository.Implement
{
	public interface ITransactionRepository : IBaseRepository<Transaction> { }
	public class TransactionRepository : BaseRepository<Transaction> , ITransactionRepository
	{
        public TransactionRepository(DbContext context) : base(context)
        {
            
        }
    }
}
