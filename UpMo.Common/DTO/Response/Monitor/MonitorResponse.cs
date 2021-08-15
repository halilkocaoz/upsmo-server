using System;
using System.Collections.Generic;
using UpMo.Common.Monitor;

namespace UpMo.Common.DTO.Response.Monitor
{
    public class MonitorResponse
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Host { get; set; }

        public MonitorMethodType Method { get; set; }
        public MonitorCheckIntervalMs IntervalMs { get; set; }

        public List<PostFormResponse> PostForms { get; set; }
        public List<HeaderResponse> Headers { get; set; }

    }
}