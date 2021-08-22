using System;

namespace UpsMo.Entities
{
    public class Response : BaseEntity<int>
    {
        public Guid MonitorID { get; set; }
        public int StatusCode { get; set; }
    }
}