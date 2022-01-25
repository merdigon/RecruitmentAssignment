using RecruitmentAssignment.DAL.Models;
using RecruitmentAssignment.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecruitmentAssignment.Services
{
    public interface IAccountService
    {
        /// <summary>
        /// Get account with specific id
        /// </summary>
        /// <param name="id">id of account</param>
        /// <returns></returns>
        Task<AccountDto> Get(int id);

        /// <summary>
        /// Get all account, filtered by filter object
        /// </summary>
        /// <param name="filter">filtering parameters</param>
        /// <returns></returns>
        Task<IEnumerable<AccountDto>> Get(AccountFilterModel filter);

        /// <summary>
        /// Delete account with specific id
        /// </summary>
        /// <param name="id">id of account to delete</param>
        /// <returns></returns>
        Task<bool> Delete(int id);

        /// <summary>
        /// Create new account, using data provided
        /// </summary>
        /// <param name="value">data of new account</param>
        /// <returns></returns>
        Task<int> Create(AccountDto value);

        /// <summary>
        /// Edit value of existing account
        /// </summary>
        /// <param name="id">id of account to edit</param>
        /// <param name="value">new data of account</param>
        /// <returns></returns>
        Task Edit(int id, AccountDto value);

        /// <summary>
        /// Convert model to dto representation
        /// </summary>
        /// <param name="model">model to convert</param>
        /// <returns></returns>
        AccountDto ConvertToDto(Account model);
    }
}
