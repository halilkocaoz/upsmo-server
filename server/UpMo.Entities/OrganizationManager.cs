using System;

namespace UpMo.Entities
{
    public class OrganizationManager : BaseEntity<Guid>
    {
        public Guid OrganizationID { get; set; }
        public uint UserID { get; set; }

        // todo: user what may do about Organization, for example create, update, delete monitor

        //public virtual Organization Organization { get; set; }
        
        //public virtual AppUser User { get; set; }
    }
}