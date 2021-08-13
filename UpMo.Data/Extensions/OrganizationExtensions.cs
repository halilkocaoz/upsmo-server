using System;
using System.Linq;
using UpMo.Entities;

namespace UpMo.Data.Extensions
{
    public static class OrganizationExtensions
    {
        /// <summary>
        /// Checks given value equals creator user id.
        /// </summary>
        /// <param name="org">Instance</param>
        /// <param name="authenticatedUserID">An value to compare <see cref="Organization.CreatorUserID"/></param>
        /// <returns>true if given value has the same value as given <see cref="Organization.CreatorUserID"/>; otherwise, false.</returns>
        public static bool CheckCreator(this Organization org, int authenticatedUserID)
        {
            return org?.CreatorUserID == authenticatedUserID;
        }

        /// <summary>
        /// Checks given value equals any admin or creator user id.
        /// </summary>
        /// <param name="org">Instance</param>
        /// <param name="authenticatedUserID">An value to compare <see cref="Organization.CreatorUserID"/></param>
        /// <returns>true if given value has the same value as given <see cref="Organization.CreatorUserID"/> or same <see cref="OrganizationManager.UserID"/> as Admin; otherwise, false</returns>
        /// <exception cref="ArgumentNullException"><see cref="Organization.Managers"/></exception>
        public static bool CheckCreatorOrAdmin(this Organization org, int authenticatedUserID)
        {
            if (org?.CreatorUserID == authenticatedUserID)
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