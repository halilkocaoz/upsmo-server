using System;
using System.Text.Json.Serialization;

namespace UpMo.Common.DTO.Request.Monitor
{
    public class PostFormDataCreateRequest
    {
        [JsonIgnore]
        public Guid ID { get; private set; } = Guid.NewGuid();

        public string Key { get; set; }
        public string Value { get; set; }
    }
}