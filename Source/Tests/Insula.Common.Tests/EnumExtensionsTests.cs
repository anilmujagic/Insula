using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Insula.Common.Tests
{
    public enum TestEnum
    {
        A,
        B,
        C
    }

    public class EnumExtensionsTests
    {
        public class GetNameTests
        {
            [Fact]
            public void GetName_OnValidValue_ReturnsNameOfValue()
            {
                TestEnum testEnum = TestEnum.B;
                TestEnum? nullableTestEnum = TestEnum.C;

                var expectedName = Enum.GetName(testEnum.GetType(), testEnum);
                var name = testEnum.GetName();
                var expectedNullableName = Enum.GetName(nullableTestEnum.GetType(), nullableTestEnum);
                var nullableName = nullableTestEnum.GetName();

                Assert.Equal("B", name);
                Assert.Equal(expectedName, name);
                Assert.Equal("C", nullableName);
                Assert.Equal(expectedNullableName, nullableName);
            }

            [Fact]
            public void GetName_OnInvalidValue_ReturnsNull()
            {
                TestEnum testEnum = (TestEnum)5;
                TestEnum? nullableTestEnum = (TestEnum?)6;

                var expectedName = Enum.GetName(testEnum.GetType(), testEnum);
                var name = testEnum.GetName();
                var expectedNullableName = Enum.GetName(nullableTestEnum.GetType(), nullableTestEnum);
                var nullableName = nullableTestEnum.GetName();

                Assert.Equal(null, name);
                Assert.Equal(expectedName, name);
                Assert.Equal(null, nullableName);
                Assert.Equal(expectedNullableName, nullableName);
            }

            [Fact]
            public void GetName_OnNullValue_ReturnsNull()
            {
                TestEnum? nullableTestEnum = null;

                var nullableName = nullableTestEnum.GetName();

                Assert.Equal(null, nullableName);
            }
        }
    }
}
