using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Insula.Common
{
    public static class EnumExtensions
    {
        public static string GetName(this Enum e)
        {
            return Enum.GetName(e.GetType(), e);
        }
    }
}
