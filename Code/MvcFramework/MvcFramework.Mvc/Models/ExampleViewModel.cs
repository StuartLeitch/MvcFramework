using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Template.Helpers;

namespace Template.Models
{
    public class ExampleViewModel
    {
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Start Date:")]
        public DateTime? StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "End Date:")]
        public DateTime? EndDate { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(15, ErrorMessage = "Must be less than {1} Characters")]
        [PlaceHolder("ex. John Smith")]
        [Display(Name = "Full Name:")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [PlaceHolder("user@email.com")]
        [Display(Name = "Email:")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [PlaceHolder("123 456 789")]
        [Display(Name = "Phone:")]
        public string Phone { get; set; }

        [Required]
        [StringLength(150, ErrorMessage = "Must be less than {1} Characters")]
        [PlaceHolder("www.google.com")]
        [DataType(DataType.Text)]
        [Display(Name = "Website:")]
        public string URL { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Description:")]
        public string Description { get; set; }
    }

}

