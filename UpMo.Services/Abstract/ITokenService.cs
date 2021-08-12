using UpMo.Entities;

namespace UpMo.Services.Abstract
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}