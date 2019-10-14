using CarpoolManagement.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarpoolManagement.Data.CarpoolManagementContext
{
    public static class SeedData
    {
        public static void SeedInitialData()
        {
            using (var dbContext = new CarpoolContext())
            {
                dbContext.Database.EnsureCreated();
                if (!dbContext.Carpools.Any())
                {
                    var cars = new List<Carpool>
                    {
                        new Carpool{ CarType = "VW GOLF VII", Color = "Dark blue", NumberOfSeats = 5, Plates = "ZG 1560-AB", Name = "Dark blue Golf for travel and city rides" },
                        new Carpool{ CarType = "Mazda 3", Color = "Grey", NumberOfSeats = 5, Plates = "ZG 1560-AC", Name = "Grey Mazda 3 for travel" },
                        new Carpool{ CarType = "Skoda Octavia", Color = "White", NumberOfSeats = 5, Plates = "ZG 1560-AD", Name = "White Octavia for travel" },
                        new Carpool{ CarType = "VW POLO", Color = "Silver", NumberOfSeats = 5, Plates = "ZG 1560-AE", Name = "Silver POLO for travel and city rides" },
                        new Carpool{ CarType = "VW Caddy", Color = "White", NumberOfSeats = 3, Plates = "ZG 1560-AF", Name = "Caddy for transport" },
                        new Carpool{ CarType = "Ford Fiesta", Color = "Purple", NumberOfSeats = 5, Plates = "ZG 1560-AG", Name = "Purple Fiesta for travel and city rides" },
                        new Carpool{ CarType = "Renault Megane", Color = "Dark blue", NumberOfSeats = 5, Plates = "ZG 1560-AH", Name = "Megane for travel" },
                        new Carpool{ CarType = "Honda Civic", Color = "Silver", NumberOfSeats = 5, Plates = "ZG 1560-AI", Name = "Civic for travel" },
                        new Carpool{ CarType = "Toyota Land Cruiser", Color = "Black", NumberOfSeats = 5, Plates = "ZG 1560-AJ", Name = "Land cruiser for travel" },
                        new Carpool{ CarType = "Smart", Color = "White", NumberOfSeats = 2, Plates = "ZG 1560-AK", Name = "Smart for food deliveries" }
                    };
                    dbContext.Carpools.AddRange(cars);
                }
                if (!dbContext.Employees.Any())
                {
                    var employees = new List<Employee>
                    {
                        new Employee{ EmployeeName = "Ivana Horvat", IsDriver = true},
                        new Employee{ EmployeeName = "Ivan Maric", IsDriver = true},
                        new Employee{ EmployeeName = "Josipa Anic", IsDriver = true},
                        new Employee{ EmployeeName = "Josip Brekalo", IsDriver = true},
                        new Employee{ EmployeeName = "Ana Milicic", IsDriver = true},
                        new Employee{ EmployeeName = "Bob Rock", IsDriver = true},
                        new Employee{ EmployeeName = "Thomas English", IsDriver = true},
                        new Employee{ EmployeeName = "Michelle Seaman", IsDriver = true},
                        new Employee{ EmployeeName = "Eve Blake", IsDriver = true},
                        new Employee{ EmployeeName = "Mike Fish", IsDriver = true},
                        new Employee{ EmployeeName = "Nikola Knezevic", IsDriver = false},
                        new Employee{ EmployeeName = "Luka Kovacevic", IsDriver = false},
                        new Employee{ EmployeeName = "Marina Pavlovic", IsDriver = false},
                        new Employee{ EmployeeName = "David Babic", IsDriver = false},
                        new Employee{ EmployeeName = "Kristina Lukac", IsDriver = false},
                        new Employee{ EmployeeName = "Stjepan Bagaric", IsDriver = false},
                        new Employee{ EmployeeName = "Ines Jaksic", IsDriver = false},
                        new Employee{ EmployeeName = "Tea Milic", IsDriver = false},
                        new Employee{ EmployeeName = "Jakov Jakovljevic", IsDriver = false},
                        new Employee{ EmployeeName = "Steve Mill", IsDriver = false},
                        new Employee{ EmployeeName = "Daniel Gibson", IsDriver = false},
                        new Employee{ EmployeeName = "Rachel Rose", IsDriver = false},
                        new Employee{ EmployeeName = "Jude Hunt", IsDriver = true},
                        new Employee{ EmployeeName = "Lisa Law", IsDriver = true},
                        new Employee{ EmployeeName = "Andrea Bargnani", IsDriver = true}
                    };
                    dbContext.Employees.AddRange(employees);
                }
                dbContext.SaveChanges();
            }
        }
    }
}
