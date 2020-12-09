using System.Collections.Generic;
using System.Threading.Tasks;
using EnsekMeter.Web.Models;

namespace EnsekMeter.Web.Services
{
    public interface IAccountService
    {
        Task<List<AccountViewModel>> GetAccounts();
        Task<AccountViewModel> GetAccount(int accountId);
        Task GenerateAccounts();
    }
}