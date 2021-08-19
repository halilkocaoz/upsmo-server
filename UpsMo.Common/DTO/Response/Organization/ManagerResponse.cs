using System;

namespace UpsMo.Common.DTO.Response.Organization
{
    public class ManagerResponse
    {
        public Guid ID { get; set; }
        public bool Admin { get; set; }
        public bool Viewer { get; set; }

        public AppUserResponse User { get; set; }
    }
}