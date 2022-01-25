using FluentAssertions;
using Moq;
using NUnit.Framework;
using RecruitmentAssignment.DAL.Models;
using RecruitmentAssignment.DAL.Repositories;
using RecruitmentAssignment.Models;
using RecruitmentAssignment.Services;
using System;
using System.Threading.Tasks;

namespace RecruitmentAssignment.Tests.Services
{
    [TestFixture]
    public class AccountServiceTests
    {
        [Test]
        public void Create_WhenModelIsNull_ThrowArgumentException()
        {
            var accountService = new Mock<IAccountRepository>();

            var sut = CreateSut(accountService.Object);
            Func<Task> result = async () => await sut.Create(null);

            result.Should().ThrowAsync<ArgumentException>();    
        }

        [Test]
        public void Create_WhenAccountTypeIsInvalid_ThrowArgumentException()
        {
            var inputAccount = new AccountDto
            {
                Type = "InvalidType"
            };
            var accountRepository = new Mock<IAccountRepository>();

            var sut = CreateSut(accountRepository.Object);
            Func<Task> result = async () => await sut.Create(inputAccount);

            result.Should().ThrowAsync<ArgumentException>();
        }

        [Test]
        public void Create_WhenAccountSummaryIsInvalid_ThrowArgumentException()
        {
            var inputAccount = new AccountDto
            {
                Summary = "InvalidSummary"
            };
            var accountRepository = new Mock<IAccountRepository>();

            var sut = CreateSut(accountRepository.Object);
            Func<Task> result = async () => await sut.Create(inputAccount);

            result.Should().ThrowAsync<ArgumentException>();
        }

        [Test]
        public async Task Get_WhenAccountTypeInFilterIsValid_ShouldPassAccountTypeAsEnumToRepository()
        {
            var inputFilter = new AccountFilterModel { Type = "Credit" };
            var accountRepository = new Mock<IAccountRepository>();

            var sut = CreateSut(accountRepository.Object);
            await sut.Get(inputFilter);

            accountRepository.Verify(x => x.GetBy(null, AccountType.Credit, null, null,null,null));
        }

        //TODO Add more tests

        private AccountService CreateSut(IAccountRepository repository)
        {
            return new AccountService(repository);
        }
    }
}
