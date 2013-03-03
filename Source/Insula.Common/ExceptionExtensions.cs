using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Globalization;

namespace Insula.Common
{
    public static class ExceptionExtensions
    {
        public static IEnumerable<Exception> GetExceptionTree(this Exception target)
        {
            var exceptions = new List<Exception>();

            var currentException = target;
            while (currentException != null)
            {
                exceptions.Add(currentException);
                currentException = currentException.InnerException;
            }

            return exceptions;
        }
    }
}
