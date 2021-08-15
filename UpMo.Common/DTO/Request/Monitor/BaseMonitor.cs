using System;
using System.ComponentModel.DataAnnotations;
using UpMo.Common.Monitor;

namespace UpMo.Common.DTO.Request.Monitor
{
    public abstract class BaseMonitor : BaseRequestDTO<Guid, int>
    {
        [Required]
        public string Name { get; set; }

        //todo regex
        [Required]
        public string Host { get; set; }

        [Range((int)MonitorMethodType.GET, (int)MonitorMethodType.POST)]
        public MonitorMethodType Method { get; set; }

        [Range((int)MonitorCheckIntervalMs.OneMin, (int)MonitorCheckIntervalMs.OneDay)]
        public MonitorCheckIntervalMs IntervalMs { get; set; }
    }
}