using RecruitmentAssignment.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruitmentAssignment.DAL.Repositories
{
    public interface IAccountRepository: IRepository<Account>
    {
        /// <summary>
        /// Get accounts filtered by passed arguments
        /// </summary>
        /// <param name="summary"></param>
        /// <param name="type"></param>
        /// <param name="amount"></param>
        /// <param name="postingDate"></param>
        /// <param name="isCleared"></param>
        /// <param name="clearedDate"></param>
        /// <returns></returns>
        Task<IEnumerable<Account>> GetBy(AccountSummary? summary, AccountType? type, decimal? amount, 
            DateTime? postingDate, bool? isCleared, DateTime? clearedDate);
    }
}
