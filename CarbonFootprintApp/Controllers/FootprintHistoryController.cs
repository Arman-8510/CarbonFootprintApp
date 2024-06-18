using CarbonFootprintApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarbonFootprintApp.Controllers
{
    [Authorize(Roles = "User")]
    public class FootprintHistoryController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly CarbonFootprintDbContext _dbContext;

        public FootprintHistoryController(CarbonFootprintDbContext dbContext, SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
            _dbContext = dbContext;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var loginUserId = _signInManager.UserManager.GetUserId(User);
            var footprintHistories = await _dbContext.FootprintHistories
                                    .Where(footprintHistory => footprintHistory.ApplicationUserId == loginUserId)
                                    .ToListAsync();

            return View(footprintHistories);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(FootprintHistoryCreateModel footprintHistoryCreateModel)
        {
            footprintHistoryCreateModel.ApplicationUserId = _signInManager.UserManager.GetUserId(User)!;

            if (ModelState.IsValid)
            {
                var footprintHistory = new FootprintHistory
                {
                    TransportationMiles = footprintHistoryCreateModel.TransportationMiles,
                    EnergyUsage = footprintHistoryCreateModel.EnergyUsage,
                    // CarbonFootprintResult = footprintHistoryCreateModel.CarbonFootprintResult,
                    Note = footprintHistoryCreateModel.Note,
                    ApplicationUserId = footprintHistoryCreateModel.ApplicationUserId
                };

                // Sample carbon footprint ratios
                var transportationRatio = 0.00022; // lbs CO2 per mile
                var energyUsageRatio = 0.95; // lbs CO2 per kWh
                footprintHistory.CarbonFootprintResult = Math.Round((footprintHistory.TransportationMiles
                    * transportationRatio + footprintHistory.EnergyUsage * energyUsageRatio), 2);

                await _dbContext.FootprintHistories.AddAsync(footprintHistory);
                var saveFootprintHistory = await _dbContext.SaveChangesAsync();

                if (saveFootprintHistory > 0)
                {
                    TempData["FootprintHistoryCreateNotifaction"] = "Footprint History Create Successfull";
                    return RedirectToAction("Index", "FootprintHistory");
                }

                ModelState.AddModelError("", "Footprint History cannot saved! Please, try again.");
            }

            return View(footprintHistoryCreateModel);
        }

        [HttpGet]
        public async Task<ActionResult<FootprintHistoryEditModel>> Edit(int id)
        {
            var existFootprintHistory = await _dbContext.FootprintHistories
                                       .Where(footprintHistory => footprintHistory.Id == id)
                                       .FirstOrDefaultAsync();

            if (existFootprintHistory is null)
                return View(new FootprintHistoryEditModel());

            var footprintHistoryEditModel = new FootprintHistoryEditModel
            {
                TransportationMiles = existFootprintHistory.TransportationMiles,
                EnergyUsage = existFootprintHistory.EnergyUsage,
                CarbonFootprintResult = existFootprintHistory.CarbonFootprintResult,
                Note = existFootprintHistory.Note,
                ApplicationUserId = existFootprintHistory.ApplicationUserId
            };

            return View(footprintHistoryEditModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<FootprintHistoryEditModel>> Edit(FootprintHistoryEditModel footprintHistoryEditModel)
        {
            if (ModelState.IsValid)
            {
                var footprintHistory = new FootprintHistory
                {
                    Id = footprintHistoryEditModel.Id,
                    TransportationMiles = footprintHistoryEditModel.TransportationMiles,
                    EnergyUsage = footprintHistoryEditModel.EnergyUsage,
                    // CarbonFootprintResult = footprintHistoryCreateModel.CarbonFootprintResult,
                    Note = footprintHistoryEditModel.Note,
                    ApplicationUserId = footprintHistoryEditModel.ApplicationUserId
                };

                // Sample carbon footprint ratios
                var transportationRatio = 0.00022; // lbs CO2 per mile
                var energyUsageRatio = 0.95; // lbs CO2 per kWh
                footprintHistory.CarbonFootprintResult = Math.Round((footprintHistory.TransportationMiles
                    * transportationRatio + footprintHistory.EnergyUsage * energyUsageRatio), 2);

                _dbContext.FootprintHistories.Update(footprintHistory);
                var editProductCategory = await _dbContext.SaveChangesAsync() > 0;

                if (editProductCategory)
                {
                    TempData["FootprintHistoryEditNotifaction"] = "Footprint history Edit Successfull";
                    return RedirectToAction("Index", "FootprintHistory");
                }

                return View(footprintHistoryEditModel);
            }

            return View(footprintHistoryEditModel);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var existFootprintHistory = await _dbContext.FootprintHistories
                                        .Where(footprintHistory => footprintHistory.Id == id)
                                        .FirstOrDefaultAsync();

            if (existFootprintHistory is null)
                return BadRequest("Footprint histories cannot found! Please, try again.");

            _dbContext.FootprintHistories.Remove(existFootprintHistory);
            var deleteFootprintHistory = await _dbContext.SaveChangesAsync() > 0;

            if (deleteFootprintHistory)
            {
                TempData["FootprintHistoryDeleteNotifaction"] = "Footprint History Delete Successfull";
                return RedirectToAction("Index", "FootprintHistory");
            }

            return RedirectToAction("Index", "FootprintHistory");
        }
    }
}