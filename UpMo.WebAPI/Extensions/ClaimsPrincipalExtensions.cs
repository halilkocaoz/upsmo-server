using System;
using System.Linq;
using System.Security.Claims;

namespace UpMo.WebAPI.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        /// <summary>
        /// Get Id from claims
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        public static int GetId(this ClaimsPrincipal claimsPrincipal)
        {
            Claim userIdClaim = claimsPrincipal?.Claims?.FirstOrDefault(i => i.Type == "UserId");
            return userIdClaim == default || userIdClaim == null ? throw new ArgumentNullException(nameof(userIdClaim)) : int.Parse(userIdClaim.Value);
        }
    }
}