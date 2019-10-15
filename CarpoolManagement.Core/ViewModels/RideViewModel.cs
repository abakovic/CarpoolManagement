using CarpoolManagement.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarpoolManagement.Models
{
    public class RideViewModel
    {
        public long Id { get; set; }
        public string StartLocation { get; set; }
        public string EndLocation { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public long CarId { get; set; }
        public SelectList Cars { get; set; }
        public long[] EmployeeIds { get; set; }
        public MultiSelectList Employees { get; set; }
    }
}
