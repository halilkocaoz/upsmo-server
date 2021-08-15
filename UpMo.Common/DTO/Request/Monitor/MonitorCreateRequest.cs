using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UpMo.Common.Monitor;

namespace UpMo.Common.DTO.Request.Monitor
{
    public class MonitorCreateRequest : BaseMonitor
    {
        public List<PostFormRequest> PostForms { get; set; }
    }
}