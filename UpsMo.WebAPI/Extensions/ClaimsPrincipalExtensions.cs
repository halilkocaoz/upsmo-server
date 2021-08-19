using System;
using System.Linq;
using System.Security.Claims;

namespace UpsMo.WebAPI.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        /// <summary>
        /// Get Id from claims
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        public static int GetID(this ClaimsPrincipal claimsPrincipal)
        {
            Claim userIdClaim = claimsPrincipal?.Claims?.FirstOrDefault(i => i.Type == "UserId");
            return userIdClaim == default || userIdClaim == null ? throw new ArgumentNullException(nameof(userIdClaim)) : int.Parse(userIdClaim.Value);
        }
    }
}