using System.ComponentModel.DataAnnotations;

namespace UpMo.Common.DTO.Request
{
    public class SignInRequest
    {
        [Required]
        public string Identifier { get; set; }
        [Required]
        public string Password { get; set; }
    }
}