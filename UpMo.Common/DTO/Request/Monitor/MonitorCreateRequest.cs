using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using UpMo.Common.Monitor;

namespace UpMo.Common.DTO.Request.Monitor
{
    public class MonitorCreateRequest
    {
        [JsonIgnore]
        public Guid ID { get; private set; } = Guid.NewGuid();
        [JsonIgnore]
        public int AuthenticatedUserID { get; set; }
        [JsonIgnore]
        public Guid OrganizationID { get; set; }

        public string Name { get; set; }
        public string Host { get; set; }

        public MonitorMethodType Method { get; set; }

        public MonitorCheckIntervalMs IntervalMs { get; set; }

        public List<PostFormCreateRequest> PostForms { get; set; }
    }
}