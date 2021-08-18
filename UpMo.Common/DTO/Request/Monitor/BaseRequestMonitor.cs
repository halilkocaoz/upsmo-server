using System;
using System.ComponentModel.DataAnnotations;
using UpMo.Common.Monitor;

namespace UpMo.Common.DTO.Request.Monitor
{
    public abstract class BaseRequestMonitor : BaseRequestDTO<Guid, int>
    {
        [Required]
        public string Name { get; set; }

        //todo regex
        [Required]
        public string Host { get; set; }
        
        [Required]
        [Range((int)MonitorMethodType.GET, (int)MonitorMethodType.POST)]
        public MonitorMethodType Method { get; set; }

        [Required]
        [Range((int)MonitorRegion.TR_Istanbul, (int)MonitorRegion.EU_Berlin)]
        public MonitorRegion Region { get; set; }
    }
}