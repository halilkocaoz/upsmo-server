using System;

namespace UpsMo.Entities
{
    /// <summary>
    /// POST form body values for monitor
    /// </summary>
    public class PostForm : BaseEntity<Guid>
    {
        public Guid MonitorID { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }

        public Monitor Monitor { get; set; }
    }
}