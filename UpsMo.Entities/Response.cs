using System;

namespace UpsMo.Entities
{
    public class Response
    {
        public int ID { get; set; }
        public Guid MonitorID { get; set; }
        public int StatusCode { get; set; }
    }
}