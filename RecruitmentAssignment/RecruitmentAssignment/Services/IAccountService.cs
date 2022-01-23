using RecruitmentAssignment.DAL.Models;
using RecruitmentAssignment.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecruitmentAssignment.Services
{
    public interface IAccountService
    {
        Task<AccountDto> Get(int id);

        Task<IEnumerable<AccountDto>> Get();

        Task<bool> Delete(int id);

        Task<int> Create(AccountDto value);

        Task<bool> Edit(int id, AccountDto value);

        AccountDto ConvertToDto(Account model);
    }
}
