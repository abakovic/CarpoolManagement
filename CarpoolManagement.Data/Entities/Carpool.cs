using System.Collections.Generic;

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
