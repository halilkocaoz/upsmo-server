using System;
using System.Collections.Generic;
using UpMo.Common.Monitor;

namespace UpMo.Entities
{
    public class Monitor : BaseEntity<Guid>
    {
        public Guid OrganizationID { get; set; }
        public string Name { get; set; }

        /// <summary>
        /// URL or IP Address
        /// </summary>
        public string URL { get; set; }
        
        public MonitorMethodType Method { get; set; }
        
        public MonitorCheckIntervalMs IntervalMs { get; set; }


        /// <summary>
        /// POST form body values
        /// </summary>
        public virtual ICollection<MonitorBody> BodyValues { get; set; }
    }
}