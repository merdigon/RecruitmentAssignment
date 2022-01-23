using RecruitmentAssignment.DAL.Models;
using RecruitmentAssignment.DAL.Repositories;
using RecruitmentAssignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecruitmentAssignment.Services
{
    public class AccountService : IAccountService
    {
        string[] _possibleAccountTypes = Enum.GetValues(typeof(AccountType)).Cast<AccountType>().Select(p => p.ToString()).ToArray();
        string[] _possibleAccountSummary = Enum.GetValues(typeof(AccountSummary)).Cast<AccountSummary>().Select(p => p.ToString()).ToArray();
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<int> Create(AccountDto value)
        {
            Validate(value);

            var newAccount = new Account
            {
                ExternalId = value.Id,
                Amount = value.Amount,
                ClearedDate = value.ClearedDate,
                IsCleared = value.IsCleared,
                PostingDate = value.PostingDate,
                Summary = Enum.Parse<AccountSummary>(value.Summary),
                Type = Enum.Parse<AccountType>(value.Type)
            };

            return (int)await _accountRepository.Create(newAccount);
        }

        public async Task<bool> Delete(int id)
        {
            var result = _accountRepository.Delete(id);

            return await result;
        }

        public async Task<bool> Edit(int id, AccountDto value)
        {
            Validate(value);

            var newAccount = new Account
            {
                ExternalId = value.Id,
                Amount = value.Amount,
                ClearedDate = value.ClearedDate,
                IsCleared = value.IsCleared,
                PostingDate = value.PostingDate,
                Summary = Enum.Parse<AccountSummary>(value.Summary),
                Type = Enum.Parse<AccountType>(value.Type)
            };

            return await _accountRepository.Edit(id, newAccount);
        }

        public async Task<AccountDto> Get(int id)
        {
            var account = await _accountRepository.Get(id);

            return ConvertToDto(account);
        }

        public async Task<IEnumerable<AccountDto>> Get()
        {
            var accounts = await _accountRepository.Get();

            return accounts.Select(model => ConvertToDto(model)).ToList();
        }

        public AccountDto ConvertToDto(Account model)
        {
            if(model == null)
                return null;

            return new AccountDto
            {
                Amount = model.Amount,
                ApplicationId = model.Id,
                ClearedDate = model.ClearedDate,
                IsCleared = model.IsCleared,
                PostingDate = model.PostingDate,
                Id = model.ExternalId,
                Type = model.Type.ToString(),
                Summary = model.Summary.ToString()
            };
        }

        public void Validate(AccountDto model)
        {
            if(model == null)
                throw new ArgumentException("Object is empty.");

            if (!_possibleAccountSummary.Contains(model.Summary))
                throw new ArgumentException($"Invalid value of {nameof(model.Summary)}.");

            if (!_possibleAccountTypes.Contains(model.Type))
                throw new ArgumentException($"Invalid value of {nameof(model.Type)}.");
        }
    }
}
