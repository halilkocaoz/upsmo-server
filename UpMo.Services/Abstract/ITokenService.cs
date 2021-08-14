using UpMo.Common.DTO.Response;
using UpMo.Entities;

namespace UpMo.Services.Abstract
{
    public interface ITokenService
    {
        Token CreateToken(AppUser user);
    }
}