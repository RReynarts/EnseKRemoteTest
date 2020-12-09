using System.Threading.Tasks;
using Contracts.Requests;
using Contracts.Responses;

namespace Contracts
{
    public interface IAccountService
    {
        Task<AccountResponse> GetAccountAsync(AccountRequest request);
        Task<BulkUploadAccountsResponse> BuklUploadAccounts(BulkUploadAccountsRequest request);
        Task<AccountsResponse> GetAccountsAsync(AccountsRequest accountsRequest);
    }
}
