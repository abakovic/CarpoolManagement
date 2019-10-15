using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CarpoolManagement.Data.Entities
{
    public class Carpool : IEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string CarType { get; set; }
        public string Color { get; set; }
        public string Plates { get; set; }
        public int NumberOfSeats { get; set; }

        public ICollection<RideSharing> Rides { get; set; }
    }
}
