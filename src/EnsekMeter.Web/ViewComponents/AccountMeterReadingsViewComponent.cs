using System;
using EnsekMeter.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace EnsekMeter.Web.ViewComponents
{
    public class AccountMeterReadingsViewComponent : ViewComponent
    {
        private readonly IMeterReadingService _meterReadingService;
        public AccountMeterReadingsViewComponent(IMeterReadingService meterReadingService)
        {
            _meterReadingService = meterReadingService ?? throw new ArgumentNullException(nameof(meterReadingService));
        }
        public IViewComponentResult Invoke(int accountId)
        {
            ViewBag.AccountId = accountId;
            return View(_meterReadingService.GetMeterReadings(accountId).Result);
        }
    }
}