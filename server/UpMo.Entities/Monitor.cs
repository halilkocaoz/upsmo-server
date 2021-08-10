using System;

namespace UpMo.Entities
{
    public class Monitor : BaseEntity<Guid>
    {
        public Guid OrganizationID { get; set; }
    }
}