using System;
using System.Linq;
using UpsMo.Entities;

namespace UpsMo.Data.Extensions
{
    public static class OrganizationExtensions
    {
        /// <summary>
        /// Checks given value equals founder user id.
        /// </summary>
        /// <param name="org">Instance</param>
        /// <param name="authenticatedUserID">An value to compare <see cref="Organization.FounderUserID"/></param>
        /// <returns>true if given value has the same value as given <see cref="Organization.FounderUserID"/>; otherwise, false.</returns>
        public static bool CheckFounder(this Organization org, int authenticatedUserID)
        {
            return org?.FounderUserID == authenticatedUserID;
        }

        /// <summary>
        /// Checks given value equals any admin or founder user id.
        /// </summary>
        /// <param name="org">Instance</param>
        /// <param name="authenticatedUserID">An value to compare <see cref="Organization.FounderUserID"/></param>
        /// <returns>true if given value has the same value as given <see cref="Organization.FounderUserID"/> or same <see cref="Manager.UserID"/> as Admin; otherwise, false</returns>
        /// <exception cref="ArgumentNullException"><see cref="Organization.Managers"/></exception>
        public static bool CheckFounderOrAdmin(this Organization org, int authenticatedUserID)
        {
            if (org?.FounderUserID == authenticatedUserID)
            {
                return true;
            }
            if (org?.Managers is null)
            {
                throw new ArgumentNullException(nameof(org.Managers));
            }

            return org.Managers.Any(x => x.Admin && x.UserID == authenticatedUserID);
        }
    }
}