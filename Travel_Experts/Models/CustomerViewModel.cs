using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Travel_Experts.Models
{
    public class CustomerViewModel 
    {
        public List<Booking> cBookings { get; set; }
        public List<PackageDTO> packages { get; set; }
        public Customer Customer { get; set; }
        public string ActiveCategory { get; set; }

        public string LoadMode { get; set; }
        public string CheckActiveCategory(string currentView) =>
            currentView == ActiveCategory ? "active" : "";//decides which view is active - Active/Past bookings
    }
}
