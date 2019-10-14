namespace RentAndCycleCodeFirst.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Booking : IValidatableObject
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "The start date is required")]
        [Display(Name = "Start Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        //[DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "The end date is required")]
        [Display(Name = "End Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        //[DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        [Required(ErrorMessage = "The user id is required")]
        public string UserId { get; set; }
        [Required(ErrorMessage = "The company id is required")]
        public int CompanyBikeId { get; set; }
        public int? Feedback { get; set; }
    
        public virtual CompanyBike CompanyBike { get; set; }

        IEnumerable<ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            if (EndDate < StartDate)
            {
                yield return new ValidationResult("EndDate must be greater than StartDate");
            }
        }
    }
}
