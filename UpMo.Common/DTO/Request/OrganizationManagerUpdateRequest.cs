using System;
using System.Text.Json.Serialization;

namespace UpMo.Common.DTO.Request
{
    public class OrganizationManagerUpdateRequest
    {
        [JsonIgnore]
        public Guid OrganizationManagerID { get; set; }
        [JsonIgnore]
        public int AuthenticatedUserID { get; set; }

        public bool Admin { get; set; }
        public bool Viewer { get; set; }
    }
}