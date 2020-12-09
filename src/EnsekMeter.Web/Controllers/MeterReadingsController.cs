using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EnsekMeter.Web.Models;
using EnsekMeter.Web.Services;
using EnsekMeter.Web.Extentions;

namespace EnsekMeter.Web.Controllers
{
    public class MeterReadingsController : Controller
    {
        private readonly IMeterReadingService _meterReadingService;

        public MeterReadingsController(IMeterReadingService meterReadingService)
        {
            _meterReadingService = meterReadingService ?? throw new ArgumentNullException(nameof(meterReadingService));
        }
        
        public async Task<IActionResult> Details(int id)
        {
            return View(await _meterReadingService.GetMeterReading(id));
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> Edit(int id)
        {
            return View(await _meterReadingService.GetMeterReading(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, MeterReadingViewModel meterReading)
        {
            try
            {
                await _meterReadingService.UpdateMeterReading(meterReading);
                return RedirectToAction("Details", "Accounts", new{id = meterReading.AccountId});
            }
            catch
            {
                return View();
            }
        }

        public IActionResult Create(int accountId)
        {
            return View(new MeterReadingViewModel{AccountId = accountId});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(MeterReadingViewModel meterReading)
        {
            try
            {
                await _meterReadingService.AddMeterReading(meterReading);
                return RedirectToAction("Details", "Accounts", new { id = meterReading.AccountId });
            }
            catch
            {
                return View();
            }
        }

        public async Task<IActionResult> Delete(int id, int accountId)
        {
            await _meterReadingService.DeleteMeterReading(id);
            return RedirectToAction("Details", "Accounts", new { id = accountId });
        }

        public IActionResult Upload()
        {
            var result = TempData.Get<BulkUploadMeterReadingsViewModel>("uploadErrors");
            return View(new FileUploadViewModel{Result = result});
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Upload(FileUploadViewModel fileUpload)
        {
            try
            {
                var result = await _meterReadingService.BulkUploadMeterReadings(fileUpload);
                TempData.Put("uploadErrors", result);
                return RedirectToAction("Upload", "MeterReadings");
            }
            catch
            {
                return View();
            }
        }

    }
}
