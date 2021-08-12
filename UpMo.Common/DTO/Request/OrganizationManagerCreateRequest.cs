using System;
using System.Text.Json.Serialization;

namespace UpMo.Common.DTO.Request
{
    public class OrganizationManagerCreateRequest
    {
        [JsonIgnore]
        public Guid ID { get; set; } = System.Guid.NewGuid();

        [JsonIgnore]
        public Guid OrganizationID { get; set; }


        /// <summary>
        /// will be manager user email or username
        /// </summary>
        public string Identifier { get; set; }
        public bool Admin { get; set; }
        public bool Viewer { get; set; }

        [JsonIgnore]
        public int AuthenticatedUserID { get; set; }
    }
}