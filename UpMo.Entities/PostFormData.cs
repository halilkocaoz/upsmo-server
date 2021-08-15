using System;

namespace UpMo.Entities
{
    /// <summary>
    /// POST form body values for monitor
    /// </summary>
    public class PostFormData : BaseEntity<Guid>
    {
        public Guid MonitorID { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }

        public Monitor Monitor { get; set; }
    }
}