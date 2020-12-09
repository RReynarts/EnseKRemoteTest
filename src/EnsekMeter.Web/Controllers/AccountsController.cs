using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EnsekMeter.Web.Models;
using EnsekMeter.Web.Services;

namespace EnsekMeter.Web.Controllers
{
    public class AccountsController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
        }

        public async Task<IActionResult> Index()
        {
            return View(await _accountService.GetAccounts());
        }

        public async Task<IActionResult> Details(int id)
        {
            return View(await _accountService.GetAccount(id));
        }

        public async Task<IActionResult> Create()
        {
            await _accountService.GenerateAccounts();
            return RedirectToAction("Index");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
