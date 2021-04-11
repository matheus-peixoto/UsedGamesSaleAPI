using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UsedGamesAPI.Libraries.ExthensionMethods.Object
{
    public static class ObejectExthensionMethods
    {
        public static bool IsNull(this object obj)
        {
            return obj == null;
        }
    }
}
