namespace MWC3.Helpers
{
    using System;
    using System.Collections.ObjectModel;
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.AspNet.Identity.EntityFramework;

    using MWC3.DAL;

    public static class UserHelper
    {
        public static readonly ApplicationDbContext Db = new ApplicationDbContext();

        public static ICollection<IdentityUserRole> GetUserRoles(string userName)
        {
            ICollection<IdentityUserRole> roles = new Collection<IdentityUserRole>();

            var user = Db.Users.FirstOrDefault(x => userName != null && x.UserName == userName);

            if (user != null)
            {
                roles = user.Roles;
            }

            return roles;
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