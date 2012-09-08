using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Insula.Common;

namespace Insula.Common.Tests
{
    public class StringExtensionsTests
    {
        [Fact]
        public void CleanSpacesTest()
        {
            Assert.Equal<string>("My dirty string", " My dirty  string  ".CleanSpaces());
        }
    }
}
