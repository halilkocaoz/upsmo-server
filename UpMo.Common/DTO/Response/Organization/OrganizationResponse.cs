using System;
using System.Collections.Generic;
using UpMo.Common.DTO.Response.Monitor;

namespace UpMo.Common.DTO.Response.Organization
{
    public class OrganizationResponse
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }

        public List<MonitorResponse> Monitors { get; set; }
    }
}