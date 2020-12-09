using System;
using System.Linq;
using System.Threading.Tasks;
using Common.Repository;
using Common.Services;
using Contracts.Requests;
using Contracts.Responses;
using Domain;

namespace Services.Handlers.AccountService
{
    public class GetAccountsHandler : QueryRequestHandler<AccountsRequest, AccountsResponse>
    {
        private readonly IDataSetFactory _repository;

        public GetAccountsHandler(IUnitOfWork context, IDataSetFactory repository) :
            base(context)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        protected override async Task<AccountsResponse> QueryAsync(AccountsRequest request)
        {
            var accounts = _repository.Resolve<Account>()
                .AsQueryable().Select(a => new AccountResponse
                {
                    AccountId = a.AccountId,
                    FirstName = a.FirstName,
                    LastName = a.LastName
                }).ToList();

            return new AccountsResponse {Accounts = accounts};
        }
    }
}