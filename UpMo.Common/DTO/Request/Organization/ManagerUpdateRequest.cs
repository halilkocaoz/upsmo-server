using System;
using System.Text.Json.Serialization;

namespace UpMo.Common.DTO.Request.Organization
{
    public class ManagerUpdateRequest
    {
        [JsonIgnore]
        public Guid ID { get; set; }
        [JsonIgnore]
        public int AuthenticatedUserID { get; set; }

        public bool Admin { get; set; }
        public bool Viewer { get; set; }
    }
}