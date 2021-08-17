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
        public string Region { get; set; }
        public string BasicAuthUser { get; set; }
        public string BasicAuthPassword { get; set; }

        public MonitorMethodType Method { get; set; }
        public int IntervalMs { get; set; }

        public List<PostFormResponse> PostForms { get; set; }
        public List<HeaderResponse> Headers { get; set; }

    }
}