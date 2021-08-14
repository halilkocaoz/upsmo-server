using System;

namespace UpMo.Common.DTO.Response.Organization
{
    public class OrganizationManagerResponse
    {
        public Guid ID { get; set; }
        public bool Admin { get; set; }
        public bool Viewer { get; set; }

        public AppUserResponse User { get; set; }
    }
}