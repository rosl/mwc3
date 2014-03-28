namespace MWC3.Helpers
{
    using System;
    using System.Collections.ObjectModel;
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    using MWC3.DAL;
    using MWC3.Models;

    public static class UserHelper
    {
        public static readonly ApplicationDbContext Db = new ApplicationDbContext();
        public static UserManager<ApplicationUser> UserManager { get; private set; }

        public static ICollection<IdentityUserRole> GetUserRoles(string userName)
        {
            ICollection<IdentityUserRole> roles = new Collection<IdentityUserRole>();

            var user = Db.Users.FirstOrDefault(x => userName != null && x.UserName == userName);

            if (user != null)
            {
                roles = user.Roles;
                // roles = Roles.GetRolesForUser(userName);
            }

            return roles;
        }

        public static IdentityUserRole GetFirstRole(string userName)
        {
            var user = Db.Users.FirstOrDefault(x => userName != null && x.UserName == userName);

            if (user != null)
            {
                var role = user.Roles.First();

                if (role != null)
                {
                    return role;
                }
            }

            return null;
        }

        public static string GetUserId(string userName)
        {
            var userId = String.Empty;

            var user = Db.Users.FirstOrDefault(x => userName != null && x.UserName == userName);
            
            if (user != null)
            {
                userId = user.Id;
            }
            
            return userId;
        }
    }
}