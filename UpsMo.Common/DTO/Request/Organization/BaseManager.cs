using System;

namespace UpsMo.Common.DTO.Request.Organization
{
    public abstract class BaseManager : BaseRequestDTO<Guid, int>
    {
        public bool Admin { get; set; }
        public bool Viewer { get; set; }
    }
}