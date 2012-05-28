using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Insula.Common
{
    public static class EnumExtensions
    {
        public static string GetName(this Enum target)
        {
            return Enum.GetName(target.GetType(), target);
        }
    }
}
