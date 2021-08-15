using System;
using System.Text.Json.Serialization;

namespace UpMo.Common.DTO.Request.Monitor
{
    /// <summary>
    /// Create and Update DTO
    /// </summary>
    public class PostFormRequest : BaseRequestDTO<Guid, int>
    {
        [JsonIgnore]
        public Guid MonitorID { get; set; }

        public string Key { get; set; }
        public string Value { get; set; }
    }
}