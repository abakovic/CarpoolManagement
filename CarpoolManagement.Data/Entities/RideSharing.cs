using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CarpoolManagement.Data.Entities
{
    public class RideSharing
    {
        public long Id { get; set; }
        public string StartLocation { get; set; }
        public string EndLocation { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public long CarId { get; set; }
        public Carpool Car { get; set; }
        public IList<EmployeeRide> EmployeeRides { get; set; }
    }
}
