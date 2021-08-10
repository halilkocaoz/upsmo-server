using Microsoft.AspNetCore.Identity;

namespace UpMo.Entities
{
    public class AppUser : IdentityUser<uint>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}