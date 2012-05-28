using System;

namespace Insula.Data.Orm
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class IdentityAttribute : Attribute
    {
    }
}
