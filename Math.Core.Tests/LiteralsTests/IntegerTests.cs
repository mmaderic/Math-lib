using Math.Core.Literals;
using Xunit;

namespace Math.Core.Tests.LiteralsTests
{
    public class IntegerTests
    {
        [Fact]
        public void IntegerAddsInteger()
        {
            Number a = 7;
            Number b = 13;

            var result = a + b;

            Assert.Equal("20", result.ToString());
        }

        [Fact]
        public void IntegerSubtractsInteger()
        {
            Number a = 20;
            Number b = 7;

            var result = a - b;

            Assert.Equal("13", result.ToString());
        }

        [Fact]
        public void IntegerMultipliesInteger()
        {
            Number a = 5;
            Number b = 7;

            var result = a * b;

            Assert.Equal("35", result.ToString());
        }

        [Fact]
        public void IntegerDividesIntegerAsInteger()
        {
            Number a = 8;
            Number b = 4;

            var result = a / b;

            Assert.Equal("2", result.ToString());
        }

        [Fact]
        public void IntegerDividesIntegerAsFraction()
        {
            Number a = 8;
            Number b = 3;

            var result = a / b;

            Assert.Equal("8/3", result.ToString());
        }
    }
}
