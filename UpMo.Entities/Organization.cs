using System;
using System.Collections.Generic;

namespace UpMo.Entities
{
    public class Organization : BaseEntity<Guid>
    {
        public Organization()
        {
            Monitors = new HashSet<Monitor>();
            Managers = new HashSet<Manager>();
        }
        
        public string Name { get; set; }
        public int CreatorUserID { get; set; }
        
        public virtual ICollection<Monitor> Monitors { get; set; }
        public virtual ICollection<Manager> Managers { get; set; }
    }
}