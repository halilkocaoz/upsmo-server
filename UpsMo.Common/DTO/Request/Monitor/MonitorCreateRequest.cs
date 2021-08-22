using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UpsMo.Common.Monitor;

namespace UpsMo.Common.DTO.Request.Monitor
{
    public class MonitorCreateRequest : BaseRequestMonitor
    {
        [Required]
        [Range((int)MonitorRegion.TR_Istanbul, (int)MonitorRegion.DE_Frankfurt)]
        public MonitorRegion Region { get; set; }
        
        public List<PostFormRequest> PostForms { get; set; }
        public List<HeaderRequest> Headers { get; set; }
    }
}