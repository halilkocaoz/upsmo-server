using System;
using System.Collections.Generic;

namespace UpMo.Common.DTO.Response
{
    public class OrganizationResponse
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }

        public List<MonitorResponse> Monitors { get; set; }
    }
}