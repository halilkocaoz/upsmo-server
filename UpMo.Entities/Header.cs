using System;

namespace UpMo.Entities
{
    public class Header : BaseEntity<Guid>
    {
        public Guid MonitorID { get; set; }

        public string Key { get; set; }
        public string Value { get; set; }

        public Monitor Monitor { get; set; }
    }
}