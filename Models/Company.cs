namespace RentAndCycleCodeFirst.Models
{
    using System;
    using System.Collections.Generic;
    
    public class Company
    {   
        public int Id { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public string ImageFilename { get; set; }
    
        public virtual ICollection<CompanyLocation> CompanyLocations { get; set; }
    }
}
