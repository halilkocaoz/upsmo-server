using System;

namespace UpsMo.Entities
{
    public class Manager : BaseEntity<Guid>
    {
        public Guid OrganizationID { get; set; }
        public int UserID { get; set; }
        public bool Admin { get; set; }
        public bool Viewer { get; set; }

        public Organization Organization { get; set; }
        public AppUser User { get; set; }
    }
}