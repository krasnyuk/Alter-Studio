    using System;
    using System.Collections.Generic;

namespace AlterStudio.Models
{
    public class Positions
    {   
        public int PositionId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Note { get; set; }
        
        public virtual ICollection<Employees> Employees { get; set; }
    }
}
