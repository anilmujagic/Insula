using System;

namespace Insula.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class IdentityAttribute : Attribute
    {
    }
}
