using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MWC3.Helpers
{
    using MWC3.DAL;

    public static class Static
    {
        public static readonly ApplicationDbContext Db = new ApplicationDbContext();

        public static Dictionary<string, TEnum> ToDictionary<TEnum>()
    where TEnum : struct
        {
            if (!typeof(TEnum).IsEnum)
                throw new ArgumentException("Type must be an enumeration");
            return Enum.GetValues(typeof(TEnum)).Cast<TEnum>().
                    ToDictionary(e => Enum.GetName(typeof(TEnum), e));
        }

        public static string GetUserId(string userName)
        {
            var userId = string.Empty;
            var user = Db.Users.FirstOrDefault(x => userName != null && x.UserName == userName);
            if (user != null)
            {
                userId = user.Id;
            }
            return userId;
        }
    }
}