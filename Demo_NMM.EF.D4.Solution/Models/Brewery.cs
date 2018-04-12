using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Demo_NMM.EF.Models
{
    public class Brewery
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "The {0} is required.")]
        [Display(Name = "Brewery Name")]
        [StringLength(100)]
        public string Name { get; set; }

        [Required(ErrorMessage = "The {0} is required.")]
        [StringLength(50)]
        public string Address { get; set; }

        [Required(ErrorMessage = "The {0} is required.")]
        [StringLength(50)]
        public string City { get; set; }

        [Required(ErrorMessage = "The {0} is required.")]
        [StringLength(50)]
        public string State { get; set; }

        [Required(ErrorMessage = "The {0} is required.")]
        [StringLength(50)]
        public string Zip { get; set; }

        [Required(ErrorMessage = "The {0} is required.")]
        [Display(Name = "Phone Number")]
        [StringLength(50)]
        [RegularExpression(@"\(?\d{3}\)?-? *\d{3}-? *-?\d{4}", 
            ErrorMessage = "This does not appear to be a valid phone number (555-555-5555).")]
        public string Phone { get; set; }

        public string URL { get; set; }

        public ICollection<BreweryReview> Reviews { get; set; }
    }
}