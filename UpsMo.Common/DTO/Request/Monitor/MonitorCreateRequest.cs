using System.Collections.Generic;

namespace UpsMo.Common.DTO.Request.Monitor
{
    public class MonitorCreateRequest : BaseRequestMonitor
    {
        public List<PostFormRequest> PostForms { get; set; }
        public List<HeaderRequest> Headers { get; set; }
    }
}