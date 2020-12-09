using System;
using System.IO;
using System.Threading.Tasks;
using Contracts;
using Contracts.Requests;
using EnsekMeter.Properties;
using Microsoft.AspNetCore.Mvc;

namespace EnsekMeter.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountsController( IAccountService accountService)
        {
            _accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
        }

        [HttpGet]
        [Route("{accountId}")]
        public async Task<IActionResult> GetAccount(int accountId)
        {
            var result = await _accountService.GetAccountAsync(new AccountRequest {AccountId = accountId});
            return Ok(result);
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAccounts()
        {
            var result = await _accountService.GetAccountsAsync(new AccountsRequest());
            return Ok(result.Accounts);
        }
        
        [HttpPost]
        [Route("/account-uploads")]
        public async Task<IActionResult> BulkUpload()
        {
            var request = new BulkUploadAccountsRequest { UploadFile = new MemoryStream(Resources.Test_Accounts) };
            var result = await _accountService.BuklUploadAccounts(request);
            return Ok(result);
        }
    }
}
