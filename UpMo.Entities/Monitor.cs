using System;
using System.Collections.Generic;
using UpMo.Common.Monitor;

namespace UpMo.Entities
{
    public class Monitor : BaseEntity<Guid>
    {
        public Monitor()
        {
            PostBodyValues = new HashSet<MonitorPostBody>();
        }
        
        public Guid OrganizationID { get; set; }
        public string Name { get; set; }

        /// <summary>
        /// Domain or IP Address
        /// </summary>
        public string Host { get; set; }
        
        public MonitorMethodType Method { get; set; }
        
        public MonitorCheckIntervalMs IntervalMs { get; set; }


        /// <summary>
        /// POST form body values
        /// </summary>
        public virtual ICollection<MonitorPostBody> PostBodyValues { get; set; }
    }
}