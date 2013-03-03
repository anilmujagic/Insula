using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Insula.Common
{
    public static class TypeExtensions
    {
        public static object GetDefault(this Type type)
        {
            if (type == null)
                throw new ArgumentNullException("type");

            if (type.IsValueType)
                return Activator.CreateInstance(type);

            return null;
        }
    }
}
