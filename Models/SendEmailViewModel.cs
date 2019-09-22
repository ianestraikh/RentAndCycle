using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RentAndCycleCodeFirst.Models
{
    public class SendEmailViewModel
    {
        [Display(Name = "Your email address")]
        [Required(ErrorMessage = "Please enter an email address.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string FromEmail { get; set; }

        [Required(ErrorMessage = "Please enter a subject.")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Please enter the contents")]
        public string Contents { get; set; }
    }
}