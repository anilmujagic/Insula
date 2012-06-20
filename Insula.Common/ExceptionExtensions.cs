using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

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

        public static IEnumerable<string> GetExceptionTreeMessages(this Exception target)
        {
            var messages = new List<string>();
            messages.AddRange(target.GetExceptionTree().Select(c => c.Message));
            return messages;
        }

        public static string GetExceptionTreeAsSingleMessage(this Exception target)
        {
            var message = string.Empty;
            var separator = string.Format("{0}{0}Inner exception message:{0}", Environment.NewLine);
            message += string.Join(separator, target.GetExceptionTreeMessages().ToArray());
            return message;
        }
    }
}
