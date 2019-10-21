using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarpoolManagement.Core.Services;
using CarpoolManagement.Core.ViewModels;
using System.Globalization;

namespace CarpoolManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RideSharingController : Controller
    {
        private readonly IRideService rideService;
        public RideSharingController(IRideService rideService)
        {
            this.rideService = rideService;
        }

        [HttpGet]
        public IActionResult Index(string date)
        {
            var now = string.IsNullOrEmpty(date) ? DateTime.UtcNow : DateTime.ParseExact(date, "yyyy/MM/dd hh:mm", CultureInfo.InvariantCulture); ;
            var rides = rideService.GetRidesByDate(now);
            return Ok(rides);
        }

        [HttpGet("details")]
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null) return NotFound();

            var rideSharing = await rideService.GetViewModelByIdAsync(id??0);
            if (rideSharing == null) return NotFound();

            return Ok(rideSharing);
        }

        [HttpGet("add")]
        public IActionResult Create()
        {
            var viewModel = rideService.GetEmptyVM();
            return Ok(viewModel);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Create([Bind("Id,StartLocation,EndLocation,StartDate,EndDate,CarId,EmployeeIds")] RideViewModel rideSharing)
        {
            if (ModelState.IsValid)
            {
                await rideService.CreateAsync(rideSharing);
                return Ok(nameof(Index));
            }
            return Ok("Index");
        }

        [HttpGet("edit")]
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null) return NotFound();

            var rideSharing = await rideService.GetViewModelByIdAsync(id ?? 0);
            if (rideSharing == null) return NotFound();

            return Ok(rideSharing);
        }

        [HttpPut("edit")]
        public async Task<IActionResult> Edit([Bind("Id,StartLocation,EndLocation,StartDate,EndDate,CarId,EmployeeIds")] RideViewModel rideSharing)
        {
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
            }
            return Ok("Index");
        }

        [HttpGet("delete")]
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null) return NotFound();

            var rideSharing = await rideService.GetViewModelByIdAsync(id ?? 0);
            if (rideSharing == null) return NotFound();

            return Ok(rideSharing);
        }

        [HttpPost("delete")]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var rideSharing = await rideService.GetViewModelByIdAsync(id);
            await rideService.DeleteAsync(rideSharing);
            return Ok("Index");
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(long id)
        {
            var rideSharing = await rideService.GetViewModelByIdAsync(id);
            await rideService.DeleteAsync(rideSharing);
            return Ok("Index");
        }

        private async Task<bool> RideSharingExists(long id)
        {
            var ride = await rideService.GetViewModelByIdAsync(id);
            return ride.Id>0;
        }
    }
}
