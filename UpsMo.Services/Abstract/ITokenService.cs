using UpsMo.Common.DTO.Response;
using UpsMo.Entities;

namespace UpsMo.Services.Abstract
{
    public interface ITokenService
    {
        Token CreateToken(AppUser user);
    }
}