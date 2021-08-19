using System.ComponentModel.DataAnnotations;

namespace UpsMo.Common.DTO.Request.Auth
{
    public class SignInRequest
    {
        [Required]
        public string Identifier { get; set; }
        [Required]
        public string Password { get; set; }
    }
}