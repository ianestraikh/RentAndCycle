using System;
using System.Collections.Generic;

namespace RentAndCycleCodeFirst.Models
{  
    public class Brand
    {
        public int Id { get; set; }
        public string Name { get; set; }
    
        public virtual ICollection<Bike> Bikes { get; set; }
    }
}
