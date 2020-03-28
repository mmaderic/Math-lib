using Math.Core.Literals;
using System;
using Xunit;

namespace Math.Core.Tests.LiteralsTests
{
    public class FractionTests
    {
        [Fact]
        public void DenominatorZeroException()
        {
            var ex = Assert.Throws<InvalidOperationException>(() => new Fraction(2,0));

            Assert.Equal("Denominator is not allowed to have a value of zero.", ex.Message);
        }

        [Fact]
        public void IntegerAddsFraction()
        {
            Number a = 7;
            Number b = "8/5";

            var result = a + b;

            Assert.Equal("43/5", result.ToString());
        }

        [Fact]
        public void FractionAddsInteger()
        {            
            Number a = "8/5";
            Number b = 7;

            var result = a + b;

            Assert.Equal("43/5", result.ToString());
        }

        [Fact]
        public void FractionAddsFraction()
        {
            Number a = "8/5";
            Number b = "7/2";

            var result = a + b;

            Assert.Equal("51/10", result.ToString());
        }

        [Fact]
        public void IntegerSubtractsFraction()
        {
            Number a = 7;
            Number b = "8/5";

            var result = a - b;

            Assert.Equal("27/5", result.ToString());
        }

        [Fact]
        public void FractionSubtractsInteger()
        {
            Number a = "8/5";
            Number b = 7;

            var result = a - b;

            Assert.Equal("-27/5", result.ToString());
        }

        [Fact]
        public void FractionSubtractsFraction()
        {
            Number a = "8/5";
            Number b = "7/2";

            var result = a - b;

            Assert.Equal("-19/10", result.ToString());
        }

        [Fact]
        public void IntegerMultipliesFraction()
        {
            Number a = 7;
            Number b = "8/5";

            var result = a * b;

            Assert.Equal("56/5", result.ToString());
        }

        [Fact]
        public void FractionMultipliesInteger()
        {
            Number a = "8/5";
            Number b = 7;

            var result = a * b;

            Assert.Equal("56/5", result.ToString());
        }

        [Fact]
        public void FractionMultipliesFraction()
        {
            Number a = "8/5";
            Number b = "7/2";

            var result = a * b;

            Assert.Equal("28/5", result.ToString());
        }

        [Fact]
        public void IntegerDividesFraction()
        {
            Number a = 7;
            Number b = "8/5";

            var result = a / b;

            Assert.Equal("35/8", result.ToString());
        }

        [Fact]
        public void FractionDividesInteger()
        {
            Number a = "8/5";
            Number b = 7;

            var result = a / b;

            Assert.Equal("8/35", result.ToString());
        }

        [Fact]
        public void FractionDividesFraction()
        {
            Number a = "8/5";
            Number b = "7/2";

            var result = a / b;

            Assert.Equal("16/35", result.ToString());
        }
    }
}
