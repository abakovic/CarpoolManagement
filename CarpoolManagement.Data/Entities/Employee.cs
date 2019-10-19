using System.Collections.Generic;

namespace CarpoolManagement.Data.Entities
{
    public class Employee : IEntity
    {
        public long Id { get; set; }
        public string EmployeeName { get; set; }
        public bool IsDriver { get; set; }

        public IList<EmployeeRide> EmployeeRides { get; set; }
    }
}
