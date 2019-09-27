using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RentAndCycleCodeFirst.Models
{
    public class BulkEmailViewModel
    {
        [Display(Name = "Email List")]
        [Required(ErrorMessage = "Please select emails")]
        public string ToEmails { get; set; }
        // To keep row selection in datatable consistent with model state
        public string SelectedRows { get; set; }

        [Required(ErrorMessage = "Please enter a subject.")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Please enter the contents")]
        public string Contents { get; set; }

        //https://stackoverflow.com/a/7163720/7587466
        [Required, Microsoft.Web.Mvc.FileExtensions(Extensions = "pdf",
             ErrorMessage = "Specify a PDF file.")]
        public HttpPostedFileBase File { get; set; }
    }
}