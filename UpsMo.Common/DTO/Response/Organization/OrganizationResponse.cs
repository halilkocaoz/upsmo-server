using System;
using System.Collections.Generic;
using UpsMo.Common.DTO.Response.Monitor;

namespace UpsMo.Common.DTO.Response.Organization
{
    public class OrganizationResponse
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }

        public List<MonitorResponse> Monitors { get; set; }
    }
}