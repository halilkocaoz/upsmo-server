using System.Text.Json.Serialization;

namespace UpMo.Common.DTO.Request
{
    public class OrganizationUpdateRequest
    {
        public string Name { get; set; }

        [JsonIgnore]
        public int AuthenticatedUserID { get; set; }
    }
}