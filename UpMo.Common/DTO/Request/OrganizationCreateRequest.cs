using System;
using System.Text.Json.Serialization;

namespace UpMo.Common.DTO.Request
{
    public class OrganizationCreateRequest
    {
        [JsonIgnore]
        public Guid ID { get; private set; } = Guid.NewGuid();
        public string Name { get; set; }
        [JsonIgnore]
        public int CreatorUserID { get; set; }
    }
}