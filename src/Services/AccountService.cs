using System;
using System.Threading.Tasks;
using Common.Services;
using Contracts;
using Contracts.Requests;
using Contracts.Responses;

namespace Services
{
    public class AccountService : IAccountService
    {
        private readonly IRequestHandlerMediator _mediator;
        public AccountService(IRequestHandlerMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task<AccountResponse> GetAccountAsync(AccountRequest request)
        {
            return await _mediator.ExecuteAsync<AccountRequest, AccountResponse>(request);
        }

        public async Task<BulkUploadAccountsResponse> BuklUploadAccounts(BulkUploadAccountsRequest request)
        {
            return await _mediator.ExecuteAsync<BulkUploadAccountsRequest, BulkUploadAccountsResponse>(request);
        }

        public async Task<AccountsResponse> GetAccountsAsync(AccountsRequest accountsRequest)
        {
            return await _mediator.ExecuteAsync<AccountsRequest, AccountsResponse>(accountsRequest);
        }
    }
}
