namespace RentAndCycleCodeFirst.Models
{
    using System;
    using System.Collections.Generic;
    
    public class CompanyBike
    {
        public int Id { get; set; }
        public int Count { get; set; }
        public double Price { get; set; }
        public int BikeId { get; set; }
        public int CompanyLocationId { get; set; }

        public float? Rating { get; set; }

        public virtual Bike Bike { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual CompanyLocation CompanyLocation { get; set; }
    }
}
