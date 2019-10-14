using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RentAndCycleCodeFirst.Models
{
    public class CompanyBikeViewModel : IValidatableObject
    {
        [Required(ErrorMessage = "The start date is required")]
        [Display(Name = "Start Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "The end date is required")]
        [Display(Name = "End Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        public List<CompanyBike> CompanyBikes { get; set; }
        public bool IsValid { get; set; }

        IEnumerable<ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            if (EndDate <= StartDate)
            {
                yield return new ValidationResult("End date must be greater than Start date");
            }

            if ((EndDate - StartDate).TotalDays > 7)
            {
                yield return new ValidationResult("The period must be equal or less than 7 days");
            }

            if (StartDate <= DateTime.Today)
            {
                yield return new ValidationResult("Start date must be after today");
            }
        }
    }
}