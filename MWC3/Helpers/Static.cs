namespace MWC3.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class Static
    {
        public static Dictionary<string, TEnum> ToDictionary<TEnum>() where TEnum : struct
        {
            if (!typeof(TEnum).IsEnum)
            {
                throw new ArgumentException("Type must be an enumeration");
            }
            
            return Enum.GetValues(typeof(TEnum)).Cast<TEnum>().ToDictionary(e => Enum.GetName(typeof(TEnum), e));
        }
    }
}