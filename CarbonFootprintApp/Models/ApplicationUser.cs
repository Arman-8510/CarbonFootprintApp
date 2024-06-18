using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CarbonFootprintApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            FootprintHistories = new HashSet<FootprintHistory>();
        }

        [Required(ErrorMessage = "Please, provied first name.")]
        [StringLength(20, MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please, provied last name.")]
        [StringLength(20, MinimumLength = 2)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please, provied city.")]
        [StringLength(20, MinimumLength = 2)]
        public string City { get; set; }

        public ICollection<FootprintHistory> FootprintHistories { get; set; }
    }
}