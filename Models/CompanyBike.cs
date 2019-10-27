namespace RentAndCycleCodeFirst.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class CompanyBike
    {
        public int Id { get; set; }
        [DisplayName("Bike Count")]
        [Required(ErrorMessage = "Bike Count is required")]
        [Range(1, 100, ErrorMessage = "Number must be within the range from 1 to 100")]
        public int Count { get; set; }
        [DisplayName("Price")]
        [Required(ErrorMessage = "Price is required")]
        [Range(1, 1000, ErrorMessage = "Price must be within the range from 1 to 1000")]
        public double Price { get; set; }
        public int BikeId { get; set; }
        public int CompanyLocationId { get; set; }

        public double? Rating { get; set; }

        public virtual Bike Bike { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual CompanyLocation CompanyLocation { get; set; }
    }
}
