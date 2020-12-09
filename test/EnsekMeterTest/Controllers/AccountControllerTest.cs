using EnsekMeter.Controllers;
using System;
using System.Threading.Tasks;
using Contracts;
using Contracts.Requests;
using Contracts.Responses;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace EnsekMeterTest.Controllers
{
    public class AccountControllerTest
    {

        [Fact]
        public void When_Ctor_Accountservice_Is_Null()
        {
            //Arrange
            //Act
            //Assert
            var error = Assert.Throws<ArgumentNullException>(() => new AccountsController(null));
            error.Message.Should().Contain("accountService");
        }

        [Fact]
        public async Task When_GetAccount_Returns_Account()
        {
            //Arrange
            var accountservice = A.Fake<IAccountService>();
            var sut = new AccountsController(accountservice);
            var accountresponse = new AccountResponse
            {
                AccountId = 1,
                FirstName = "firstName",
                LastName = "LastName"
            };
            A.CallTo(() => accountservice.GetAccountAsync(A<AccountRequest>.Ignored)).Returns(accountresponse);

            //Act
            var result = await sut.GetAccount(1);

            //Assert
            result.Should().NotBeNull();
            result.GetType().Should().Be<OkObjectResult>();
            A.CallTo(() => accountservice.GetAccountAsync(A<AccountRequest>.Ignored)).MustHaveHappenedOnceExactly();
            var objectResult = result.As<OkObjectResult>();
            objectResult.Should().NotBeNull();
            objectResult.Value.Should().NotBeNull();
            objectResult.Value.GetType().Should().Be<AccountResponse>();
            var value = objectResult.Value.As<AccountResponse>();
            value.AccountId.Should().Be(1);
            value.Should().BeEquivalentTo(accountresponse);
        }

        [Fact]
        public async Task When_GetAccount_Returns_Null()
        {
            //Arrange
            var accountservice = A.Fake<IAccountService>();
            var sut = new AccountsController(accountservice);
            A.CallTo(() => accountservice.GetAccountAsync(A<AccountRequest>.Ignored)).Returns((AccountResponse)null);

            //Act
            var result = await sut.GetAccount(1);

            //Assert
            result.Should().NotBeNull();
            result.GetType().Should().Be<OkObjectResult>();
            A.CallTo(() => accountservice.GetAccountAsync(A<AccountRequest>.Ignored)).MustHaveHappenedOnceExactly();
            var objectResult = result.As<OkObjectResult>();
            objectResult.Should().NotBeNull();
            objectResult.Value.Should().BeNull();
        }
    }
}
