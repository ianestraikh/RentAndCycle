namespace RentAndCycleCodeFirst.Models
{
    using System;
    using System.Collections.Generic;
    
    public class Bike
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public int BrandId { get; set; }
        public string ImageFilename { get; set; }
    
        public virtual ICollection<CompanyBike> CompanyBikes { get; set; }
        public virtual Brand Brand { get; set; }
    }
}
