using System.ComponentModel.DataAnnotations;

namespace CarbonFootprintApp.Models
{
    public class FootprintHistory
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please, provied transportation (miles).")]
        public double TransportationMiles { get; set; }

        [Required(ErrorMessage = "Please, provied energy usage (kWh).")]
        public double EnergyUsage { get; set; }

        [Required(ErrorMessage = "Please, calculate carbon footprint result.")]
        public double CarbonFootprintResult { get; set; }

        [Required(ErrorMessage = "Please, select application user.")]
        public string ApplicationUserId { get; set; }

        public string Note { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
    }
}