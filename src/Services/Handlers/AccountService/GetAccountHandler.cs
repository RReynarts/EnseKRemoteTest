using System;
using System.Threading.Tasks;
using Common.ErrorHandling;
using Common.Repository;
using Common.Services;
using Contracts.Requests;
using Contracts.Responses;
using Domain;
using Services.Validators.AccountService;

namespace Services.Handlers.AccountService
{
    public class GetAccountHandler : QueryRequestHandler<AccountRequest, AccountResponse>
    {
        private readonly IDataSetFactory _repository;
        private readonly GetAccountValidator _validator;

        public GetAccountHandler(IUnitOfWork context, IDataSetFactory repository, GetAccountValidator validator) :
            base(context)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        protected override async Task<AccountResponse> QueryAsync(AccountRequest request)
        {
            _validator.Validate(request);
            var account = await _repository.Resolve<Account>()
                .FirstOrDefaultAsync(a => a.AccountId == request.AccountId);
            if (account == null)
            {
                throw new ValidationException($"Account with accountId {request.AccountId} not found!");
            }

            return new AccountResponse
                {AccountId = account.AccountId, FirstName = account.FirstName, LastName = account.LastName};
        }
    }
}