using System;
using System.Collections.Generic;
using UpsMo.Common.Monitor;

namespace UpsMo.Entities
{
    public class Monitor : BaseEntity<Guid>
    {
        public Monitor()
        {
            PostForms = new HashSet<PostForm>();
            Headers = new HashSet<Header>();
        }
        public Guid OrganizationID { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// Domain or IP Address
        /// </summary>
        public string Host { get; set; }
        public string Region { get; set; }
        /// <summary>
        /// request method
        /// </summary>
        public string Method { get; set; }
        public int IntervalMs { get; set; } = 1000 * 60;
        public int TimeoutMs { get; set; } = 1000 * 10;

        /// <summary>
        /// POST form body values
        /// </summary>
        public virtual ICollection<PostForm> PostForms { get; set; }
        /// <summary>
        /// Header values
        /// </summary>
        public virtual ICollection<Header> Headers { get; set; }
        public virtual Organization Organization { get; set; }
    }
}