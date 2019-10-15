using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarpoolManagement.Data.CarpoolManagementContext;
using CarpoolManagement.Data.Entities;
using CarpoolManagement.Core.Services;
using CarpoolManagement.Models;
using CarpoolManagement.Core.ViewModels;

namespace CarpoolManagement.Controllers
{
    public class RideSharingController : Controller
    {
        private IRideService rideService;
        public RideSharingController(IRideService rideService)
        {
            this.rideService = rideService;
        }

        public async Task<IActionResult> Index(DateTime? date)
        {
            var now = date ?? DateTime.UtcNow;
            var rides = rideService.GetRidesByDate(now);
            return View(rides);
        }

        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rideSharing = await rideService.GetViewModelByIdAsync(id??0);
            if (rideSharing == null)
            {
                return NotFound();
            }

            return View(rideSharing);
        }

        public IActionResult Create()
        {
            var viewModel = rideService.GetEmptyVM();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StartLocation,EndLocation,StartDate,EndDate,CarId,EmployeeIds")] RideViewModel rideSharing)
        {
            if (ModelState.IsValid)
            {
                await rideService.CreateAsync(rideSharing);
                return RedirectToAction(nameof(Index));
            }
            return View("Index");
        }

        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rideSharing = await rideService.GetViewModelByIdAsync(id ?? 0);
            if (rideSharing == null)
            {
                return NotFound();
            }
            return View(rideSharing);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,StartLocation,EndLocation,StartDate,EndDate,CarId,EmployeeIds")] RideViewModel rideSharing)
        {
            if (id != rideSharing.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await rideService.UpdateAsync(rideSharing);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!(await RideSharingExists(rideSharing.Id)))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View("Index");
        }

        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rideSharing = await rideService.GetViewModelByIdAsync(id ?? 0);
            if (rideSharing == null)
            {
                return NotFound();
            }

            return View(rideSharing);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var rideSharing = await rideService.GetViewModelByIdAsync(id);
            await rideService.DeleteAsync(rideSharing);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> RideSharingExists(long id)
        {
            var ride = await rideService.GetViewModelByIdAsync(id);
            return ride.Id>0;
        }
    }
}
