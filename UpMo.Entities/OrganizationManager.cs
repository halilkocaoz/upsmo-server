using System;

namespace UpMo.Entities
{
    public class OrganizationManager : BaseEntity<Guid>
    {
        public Guid OrganizationID { get; set; }
        public int UserID { get; set; }
        public bool Admin { get; set; }
        public bool Viewer { get; set; }

        public Organization Organization { get; set; }
    }
}