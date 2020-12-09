using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using EnsekMeter.Web.Models;
using Newtonsoft.Json;

namespace EnsekMeter.Web.Services
{
    public class AccountService : IAccountService
    {
        private readonly HttpClient _httpClient;

        public AccountService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<List<AccountViewModel>> GetAccounts()
        {
            var response = await _httpClient.GetStringAsync("https://localhost:44377/api/Accounts");
            var accounts = JsonConvert.DeserializeObject<List<AccountViewModel>>(response);
            return accounts;
        }

        public async Task<AccountViewModel> GetAccount(int accountId)
        {
            var response = await _httpClient.GetStringAsync($"https://localhost:44377/api/Accounts/{accountId}");
            var accounts = JsonConvert.DeserializeObject<AccountViewModel>(response);
            return accounts;
        }

        public async Task GenerateAccounts()
        {
            await _httpClient.PostAsync("https://localhost:44377/account-uploads", null);
        }
    }
}
