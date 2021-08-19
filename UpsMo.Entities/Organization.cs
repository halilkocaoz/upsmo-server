using System;
using System.Collections.Generic;

namespace UpsMo.Entities
{
    public class Organization : BaseEntity<Guid>
    {
        public Organization()
        {
            Monitors = new HashSet<Monitor>();
            Managers = new HashSet<Manager>();
        }
        
        public string Name { get; set; }
        public int FounderUserID { get; set; }
        
        public virtual ICollection<Monitor> Monitors { get; set; }
        public virtual ICollection<Manager> Managers { get; set; }
    }
}