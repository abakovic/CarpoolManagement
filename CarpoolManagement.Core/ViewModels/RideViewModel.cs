using CarpoolManagement.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarpoolManagement.Core.ViewModels
{
    public class RideViewModel
    {
        public long Id { get; set; }
        public string StartLocation { get; set; }
        public string EndLocation { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public SelectListItem CarId { get; set; }
        public IEnumerable<SelectListItem> Cars { get; set; }
        public string CarName { get; set; }
        public SelectListItem[] EmployeeIds { get; set; }
        public IEnumerable<SelectListItem> Employees { get; set; }
        public string EmployeeNames { get; set; }
    }
}
