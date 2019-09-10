namespace RentAndCycleCodeFirst.Models
{
    using System;
    using System.Collections.Generic;
    
    public class Booking
    {
        public int Id { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Date { get; set; }
        public int UserId { get; set; }
        public int CompanyBikeId { get; set; }
        public int Feedback { get; set; }
    
        public virtual CompanyBike CompanyBike { get; set; }
    }
}
