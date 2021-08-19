using System;
using System.Collections.Generic;

namespace UpsMo.Common.DTO.Response.Monitor
{
    public class MonitorResponse
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Host { get; set; }
        public string Method { get; set; }
        public string Region { get; set; }
        public int IntervalMs { get; set; }

        public List<PostFormResponse> PostForms { get; set; }
        public List<HeaderResponse> Headers { get; set; }
    }
}