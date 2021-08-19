using System;

namespace UpsMo.Common.DTO.Request.Organization
{
    /// <summary>
    /// Create and Update DTO
    /// </summary>
    public class OrganizationRequest : BaseRequestDTO<Guid, int>
    {
        public string Name { get; set; }
    }
}