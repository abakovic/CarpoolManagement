﻿using CarpoolManagement.Core.ViewModels;
using CarpoolManagement.Data.CarpoolManagementContext;
using CarpoolManagement.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarpoolManagement.Core.Services
{
    public interface IRideService
    {
        RideViewModel GetEmptyVM();
        List<RideViewModel> GetRidesByDate(DateTime date);
        List<RideSharing> GetRidesByCarIdAndDates(long carId, DateTime startDate, DateTime endDate);
        Task<bool> CheckCarSeats(long id, int peopleCount);
        bool CheckEmployeesDrivingLicence(long[] ids);
        List<RideSharing> GetRidesByEmployeeIdsAndDates(long[] employeeIds, DateTime startDate, DateTime endDate);
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

        public RideViewModel GetEmptyVM()
        {
            var dbModel = GetEntity();
            var dbCars = dbContext.Set<Carpool>().AsNoTracking().ToList();
            var selectCars = dbCars.Select(x =>
            {
                return new SelectListItem { Text = x.Name, Value = x.Id.ToString() };
            }).ToList();
            var dbEmployees = dbContext.Set<Employee>().AsNoTracking().ToList();
            var selectEmployees = dbEmployees.Select(x =>
            {
                return new SelectListItem { Text = x.EmployeeName, Value = x.Id.ToString() };
            }).ToList();

            return MapRide(dbModel, selectCars, selectEmployees);
        }
        public List<RideViewModel> GetRidesByDate (DateTime date)
        {
            var monthStart = new DateTime(date.Year, date.Month, 1);
            var monthEnd = monthStart.AddMonths(1).AddDays(-1);
            var rides = GetAll().Include(x => x.Car).Include(x => x.EmployeeRides).ThenInclude(x => x.Employee).Where(x => x.StartDate.Date <= monthEnd && x.EndDate.Date >= monthStart).ToList();
            var ridesVM = rides.Select(x => MapRide(x, new List<SelectListItem>(), new List<SelectListItem>())).ToList();

            return ridesVM;
        }

        public List<RideSharing> GetRidesByCarIdAndDates(long carId, DateTime startDate, DateTime endDate)
        {
            return GetAll().Where(x => x.CarId == carId && x.EndDate >= startDate && x.StartDate <= endDate).ToList();
        }

        public async Task<bool> CheckCarSeats(long id, int peopleCount)
        {
            var car = await dbContext.Carpools.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (car != null) return car.NumberOfSeats >= peopleCount;
            return true;
        }

        public bool CheckEmployeesDrivingLicence(long[] ids)
        {
            return dbContext.Employees.AsNoTracking().Where(x => ids.Contains(x.Id) && x.IsDriver).ToList().Count > 0;
        }

        public List<RideSharing> GetRidesByEmployeeIdsAndDates(long[] employeeIds, DateTime startDate, DateTime endDate)
        {
            return GetAll().Include(x => x.EmployeeRides).Where(x => x.EmployeeRides.Any(y => employeeIds.Contains(y.EmployeeId)) && x.EndDate >= startDate && x.StartDate <= endDate).ToList();
        }

        public async Task<RideViewModel> GetViewModelByIdAsync(long id)
        {
            var ride = await dbContext.Set<RideSharing>()
               .AsNoTracking()
               .Include(x => x.Car)
               .Include(x => x.EmployeeRides).ThenInclude(x => x.Employee)
               .FirstOrDefaultAsync(e => e.Id == id);
            var selectCars = dbContext.Set<Carpool>().AsNoTracking().ToList().Select(x =>
            {
                return new SelectListItem { Text = x.Name, Value = x.Id.ToString() };
            }).ToList();
            var selectEmployees = dbContext.Set<Employee>().AsNoTracking().ToList().Select(x =>
            {
                return new SelectListItem { Text = x.EmployeeName, Value = x.Id.ToString() };
            }).ToList();
            if (ride != null)
            {
                return MapRide(ride, selectCars, selectEmployees);
            }
            return new RideViewModel();
        }

        public async Task CreateAsync(RideViewModel rideVM)
        {
            var ride = MapRideVM(rideVM);
            await AddAsync(ride);
        }

        public async Task UpdateAsync(RideViewModel rideVM)
        {
            var ride = MapRideVM(rideVM);
            await UpdateAsync(ride);
        }

        public async Task DeleteAsync(RideViewModel rideVM)
        {
            var ride = MapRideVM(rideVM);
            await DeleteAsync(ride);
        }

        //TODO: AutoMapper
        private RideSharing MapRideVM(RideViewModel rideVm)
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

        private RideViewModel MapRide(RideSharing ride, List<SelectListItem> cars, List<SelectListItem> employees)
        {
            var employeeNames = ride?.EmployeeRides?.Select(x => x.Employee.EmployeeName) ?? new List<string> { "" };
            return ride.Id > 0 ? new RideViewModel
            {
                Id = ride.Id,
                StartLocation = ride.StartLocation,
                EndLocation = ride.EndLocation,
                StartDate = ride.StartDate,
                EndDate = ride.EndDate,
                CarId = ride.CarId,
                CarName = ride.Car.Name,
                EmployeeIds = ride.EmployeeRides.Select(x => x.EmployeeId).ToArray(),
                EmployeeNames = string.Join(", ", employeeNames),
                Cars = cars,
                Employees = employees
            } : new RideViewModel { Cars = cars, Employees = employees };
        }

    }
}