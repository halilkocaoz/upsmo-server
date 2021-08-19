using System.Text.Json.Serialization;

namespace UpsMo.Common.DTO.Request
{
    public abstract class BaseRequestDTO<T, TT> where T : struct where TT : struct
    {
        [JsonIgnore]
        public T ID { get; set; }
        [JsonIgnore]
        public T OrganizationID { get; set; }

        [JsonIgnore]
        public TT AuthenticatedUserID { get; set; }
    }
}