using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CarpoolManagement.Data.Entities
{
    public class Employee
    {
        public long EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public bool IsDriver { get; set; }

        public IList<EmployeeRide> EmployeeRides { get; set; }
    }
}
