using CarpoolManagement.Data.CarpoolManagementContext;
using CarpoolManagement.Data.Entities;
using CarpoolManagement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarpoolManagement.Core.Services
{
    public interface IRideService
    {
        List<RideSharing> GetRidesByDate(DateTime date);
        Task<RideViewModel> GetViewModelByIdAsync(long id);
        Task CreateAsync(RideViewModel rideVM);
        Task UpdateAsync(RideViewModel rideVM);
        Task DeleteAsync(RideViewModel rideVM);
    }

    public class RideService : Service<RideSharing>, IRideService
    {
        private readonly CarpoolContext dbContext;
        public RideService(CarpoolContext dbContext)
            :base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<RideSharing> GetRidesByDate (DateTime date)
        {
            var monthStart = new DateTime(date.Year, date.Month, 1);
            var monthEnd = monthStart.AddMonths(1).AddDays(-1);

            return GetAll().Where(x => x.StartDate.Date <= monthEnd && x.EndDate.Date >= monthStart).ToList();
        }

        public async Task<RideViewModel> GetViewModelByIdAsync(long id)
        {
            var ride = await GetByIdAsync(id);
            var vM = ride != null ? mapRide(ride) : new RideViewModel();
            return vM;
        }

        public async Task CreateAsync(RideViewModel rideVM)
        {
            var ride = mapRideVM(rideVM);
            await AddAsync(ride);
        }

        public async Task UpdateAsync(RideViewModel rideVM)
        {
            var ride = mapRideVM(rideVM);
            await UpdateAsync(ride);
        }

        public async Task DeleteAsync(RideViewModel rideVM)
        {
            var ride = mapRideVM(rideVM);
            await DeleteAsync(ride);
        }

        //TODO: AutoMapper
        private RideSharing mapRideVM(RideViewModel rideVm)
        {
            var ride = GetEntity();
            ride.Id = rideVm.Id;
            ride.StartLocation = rideVm.StartLocation;
            ride.EndLocation = rideVm.EndLocation;
            ride.StartDate = rideVm.StartDate;
            ride.EndDate = rideVm.EndDate;
            ride.CarId = rideVm.CarId;
            ride.EmployeeRides = rideVm.EmployeeIds.Select(x =>
            {
                return new EmployeeRide
                {
                    EmployeeId = x,
                    RideId = rideVm.Id
                };
            }).ToList();
            return ride;
        }

        private RideViewModel mapRide(RideSharing ride)
        {
            return new RideViewModel
            {
                Id = ride.Id,
                StartLocation = ride.StartLocation,
                EndLocation = ride.EndLocation,
                StartDate = ride.StartDate,
                EndDate = ride.EndDate,
                CarId = ride.CarId,
                EmployeeIds = ride.EmployeeRides.Select(x => x.EmployeeId).ToArray()
            };
        }

    }
}
