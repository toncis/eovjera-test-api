using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eOvjera.Test.WebAPI.Constants
{
    /// <summary>
    /// Web API - Constants - Custom claim types.
    /// </summary>
    public static class CustomClaimTypes
    {
        /// <summary>
        /// The claim string for user id.
        /// </summary>
        public static readonly string eOvjeraUserId = "User.UserId";
        /// <summary>
        /// The claim string for workflow user guid.
        /// </summary>
        public static readonly string eOvjeraUserGuid = "User.UserGuid";
        /// <summary>
        /// The claim string for user name.
        /// </summary>
        public static readonly string eOvjeraUserName = "User.Username";

        /// <summary>
        /// The claim string for user application.
        /// </summary>
        public static readonly string eOvjeraUserApplication = "User.Application";

        /// <summary>
        /// The claim string for user owner id.
        /// </summary>
        public static readonly string eOvjeraOwnerId = "User.OwnerId";
        /// <summary>
        /// The claim string for user owner name.
        /// </summary>
        public static readonly string eOvjeraOwnerName = "User.OwnerName";
        /// <summary>
        /// The claim string for user organization unit id.
        /// </summary>
        public static readonly string eOvjeraUserOrgJedId = "User.OrgJedId";
        /// <summary>
        /// The claim string for user organization unit name.
        /// </summary>
        public static readonly string eOvjeraUserOrgJedName = "User.OrgJedName";
        // /// <summary>
        // /// The claim string for user organization unit name.
        // /// </summary>
        // public static readonly string WfMUserOrgJed = "User.WfM.Role";


    }

    /// <summary>
    /// Web API - Constants - Custom claim values.
    /// </summary>
    public static class CustomClaimValues
    {
        /// <summary>
        /// The claim string for the super user.
        /// </summary>
        public static readonly string SuperUser = "SuperUser";
        /// <summary>
        /// The claim string for admin user.
        /// </summary>
        public static readonly string AdminUser = "AdminUser";
        /// <summary>
        /// The claim string for regular user.
        /// </summary>
        public static readonly string User = "User";
    }
}
