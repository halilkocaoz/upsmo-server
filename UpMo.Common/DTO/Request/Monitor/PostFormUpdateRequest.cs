using System;
using System.Text.Json.Serialization;

namespace UpMo.Common.DTO.Request.Monitor
{
    public class PostFormUpdateRequest
    {
        [JsonIgnore]
        public Guid ID { get; set; }
        [JsonIgnore]
        public Guid OrganizationID { get; set; }
        [JsonIgnore]
        public Guid MonitorID { get; set; }
        [JsonIgnore]
        public int AuthenticatedUserID { get; set; }

        public string Key { get; set; }
        public string Value { get; set; }
    }
}