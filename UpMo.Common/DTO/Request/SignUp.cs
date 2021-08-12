using System.ComponentModel.DataAnnotations;

namespace UpMo.Common.DTO.Request
{
    public class SignUpRequest
    {
        [Required]
        [MaxLength(256)]
        public string Email { get; set; }

        [Required]
        [RegularExpression("^[a-z0-9]*$")]
        [MaxLength(39)]
        public string UserName { get; set; }

        [Required]
        [MinLength(4)]
        public string Password { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        [MaxLength(30)]
        public string Surname { get; set; }
    }
}