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
            if (target == null)
                return Enumerable.Empty<Exception>();

            var exceptions = new List<Exception>();
            exceptions.Add(target);

            if (target.InnerException != null)
                exceptions.AddRange(target.InnerException.GetExceptionTree());

            return new ReadOnlyCollection<Exception>(exceptions);
        }

        public static IEnumerable<string> GetExceptionTreeMessages(this Exception target)
        {
            var messages = new List<string>();
            messages.AddRange(target.GetExceptionTree().Select(c => c.Message));
            return new ReadOnlyCollection<string>(messages);
        }

        public static string GetExceptionTreeAsOneMessage(this Exception target)
        {
            var message = String.Empty;
            message += String.Join(Environment.NewLine + Environment.NewLine + "Inner exception message:" + Environment.NewLine, 
                target.GetExceptionTreeMessages().ToArray());
            return message;
        }
    }
}
