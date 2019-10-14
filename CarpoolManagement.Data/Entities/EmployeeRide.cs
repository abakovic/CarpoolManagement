using System;
using System.Collections.Generic;
using System.Text;

namespace CarpoolManagement.Data.Entities
{
    public class EmployeeRide
    {
        public long EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public long RideId { get; set; }
        public RideSharing Ride { get; set; }

    }
}
