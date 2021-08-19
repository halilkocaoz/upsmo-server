using System;
using System.ComponentModel.DataAnnotations;
using UpMo.Common.Monitor;

namespace UpMo.Common.DTO.Request.Monitor
{
    public abstract class BaseRequestMonitor : BaseRequestDTO<Guid, int>
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [RegularExpression(@"https?:\/\/(www\.)?[-a-zA-Z0-9@:%._\+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}\b([-a-zA-Z0-9()@:%_\+.~#?&//=]*)")]
        public string Host { get; set; }
        
        [Required]
        [Range((int)MonitorMethodType.GET, (int)MonitorMethodType.POST)]
        public MonitorMethodType Method { get; set; }

        [Required]
        [Range((int)MonitorRegion.TR_Istanbul, (int)MonitorRegion.DE_Frankfurt)]
        public MonitorRegion Region { get; set; }
    }
}