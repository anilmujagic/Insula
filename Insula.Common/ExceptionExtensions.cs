using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Insula.Common
{
    public static class ExceptionExtensions
    {
        public static string GetFullErrorMessageTree(this Exception ex)
        {
            if (ex == null)
                return string.Empty;

            string message = string.Empty;

            if (ex.Message.IsNullOrWhiteSpace())
                message = "(none)";
            else
                message = ex.Message;

            if (ex.InnerException != null)
                message += string.Format("{0}{0}Inner exception message:{0}{1}",
                    Environment.NewLine, ex.InnerException.GetFullErrorMessageTree());

            return message;
        }
    }
}
