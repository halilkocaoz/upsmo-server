using System;
using System.Text.Json.Serialization;

namespace UpMo.Common.DTO.Request
{
    public class OrganizationCreateRequest
    {
        [JsonIgnore]
        public Guid ID { get; private set; } = Guid.NewGuid();
        
        [JsonIgnore]
        public int CreatorUserID { get; set; }

        public string Name { get; set; }
    }
}