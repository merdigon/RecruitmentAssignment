using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using RecruitmentAssignment.Controllers;
using RecruitmentAssignment.Models;
using RecruitmentAssignment.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecruitmentAssignment.Tests.Controllers
{
    [TestFixture]
    public class AccountControllerTests
    {
        [Test]
        public async Task Get_WhenServiceThrowsArgumentException_ShouldReturnBadRequestActionResult()
        {
            var accountService = new Mock<IAccountService>();
            accountService.Setup(x => x.Get(It.IsAny<AccountFilterModel>()))
                .Throws<ArgumentException>();

            var sut = CreateSut(accountService.Object);

            var result = await sut.Get(new AccountFilterModel());

            result.Result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Test]
        public async Task Get_WhenServiceReturnListOfAccounts_ShouldReturnOkActionResultWithObjects()
        {
            IEnumerable<AccountDto> account = new List<AccountDto> { new AccountDto(), new AccountDto() };
            var accountService = new Mock<IAccountService>();
            accountService.Setup(x => x.Get(It.IsAny<AccountFilterModel>()))
                .Returns(Task.FromResult(account));

            var sut = CreateSut(accountService.Object);

            var result = await sut.Get(new AccountFilterModel());

            result.Result.Should().BeOfType<OkObjectResult>();
            (result.Result as OkObjectResult).Value.Should().BeEquivalentTo(account);
        }

        [Test]
        public async Task Get_WhenServiceReturnNullForId_ShouldReturnNotFoundActionResult()
        {
            IEnumerable<AccountDto> account = new List<AccountDto> { new AccountDto(), new AccountDto() };
            var accountService = new Mock<IAccountService>();
            accountService.Setup(x => x.Get(It.IsAny<int>()))
                .Returns(Task.FromResult<AccountDto>(null));

            var sut = CreateSut(accountService.Object);

            var result = await sut.Get(0);

            result.Result.Should().BeOfType<NotFoundResult>();
        }


        //TODO write tests for rest of methods

        private AccountController CreateSut(IAccountService service)
        {
            return new AccountController(service);
        }
    }
}
