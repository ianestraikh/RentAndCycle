namespace RentAndCycleCodeFirst.Models
{
    using System;
    using System.Collections.Generic;
    
    public class CompanyLocation
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string Street { get; set; }
        public string Town { get; set; }
        public string Postcode { get; set; }
        public string Lat { get; set; }
        public string Lon { get; set; }
        public string ImageFilename { get; set; }
    
        public virtual Company Company { get; set; }
        public virtual ICollection<CompanyBike> CompanyBikes { get; set; }
    }
}
