using System;
using System.Text.Json.Serialization;

namespace UpMo.Common.DTO.Request.Organization
{
    public class OrganizationUpdateRequest
    {
        [JsonIgnore]
        public Guid ID { get; set; }

        [JsonIgnore]
        public int AuthenticatedUserID { get; set; }
        
        public string Name { get; set; }
    }
}