using System;
using System.Text.Json.Serialization;

namespace UpMo.Common.DTO.Request.Organization
{
    public class OrganizationUpdateRequest
    {
        [JsonIgnore]
        public Guid OrganizationID { get; set; }

        [JsonIgnore]
        public int AuthenticatedUserID { get; set; }
        
        public string Name { get; set; }
    }
}