using System.ComponentModel.DataAnnotations;

namespace CarbonFootprintApp.Models
{
    public class FootprintHistoryCreateModel
    {
        [Required(ErrorMessage = "Please, provied transportation (miles).")]
        public double TransportationMiles { get; set; }

        [Required(ErrorMessage = "Please, provied energy usage (kWh).")]
        public double EnergyUsage { get; set; }

        public double CarbonFootprintResult { get; set; }
        public string? ApplicationUserId { get; set; }
        public string? Note { get; set; }
    }
}