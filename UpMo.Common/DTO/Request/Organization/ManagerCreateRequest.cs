using System;
using System.ComponentModel.DataAnnotations;

namespace UpMo.Common.DTO.Request.Organization
{
    public class ManagerCreateRequest : BaseManager
    {
        /// <summary>
        /// will be manager user email or username
        /// </summary>
        [Required]
        public string Identifier { get; set; }
    }
}