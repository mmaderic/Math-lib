using Math.Core.Literals;
using System;
using Xunit;

namespace Math.Core.Tests.LiteralsTests
{
    public class VariableTests
    {
        [Fact]
        public void InvalidSignException()
        {
            var ex = Assert.Throws<InvalidOperationException>(() => new Variable('A'));

            Assert.Equal("Variable sign should be lower case letter.", ex.Message);
        }

        [Fact]
        public void IntegerAddsVariable()
        {
            Number a = 7;
            Number b = 'a';

            var result = a + b;

            Assert.Equal("(7 + a)", result.ToString());
        }

        [Fact]
        public void VariableAddsInteger()
        {
            Number a = 'a';
            Number b = 7;

            var result = a + b;

            Assert.Equal("(7 + a)", result.ToString());
        }

        [Fact]
        public void FractionAddsVariable()
        {
            Number a = "8/5";
            Number b = 'a';

            var result = a + b;

            Assert.Equal("(8/5 + a)", result.ToString());
        }

        [Fact]
        public void VariableAddsFraction()
        {
            Number a = 'a';
            Number b = "8/5";

            var result = a + b;

            Assert.Equal("(8/5 + a)", result.ToString());
        }

        [Fact]
        public void VariableAddsVariable()
        {
            Number a = 'a';
            Number b = 'b';

            var result = a + b;

            Assert.Equal("(a + b)", result.ToString());
        }

        [Fact]
        public void IntegerSubtractsVariable()
        {
            Number a = 7;
            Number b = 'a';

            var result = a - b;

            Assert.Equal("(7 - a)", result.ToString());
        }

        [Fact]
        public void VariableSubtractsInteger()
        {
            Number a = 'a';
            Number b = 7;

            var result = a - b;

            Assert.Equal("(7 - a)", result.ToString());
        }

        [Fact]
        public void FractionSubtractsVariable()
        {
            Number a = "8/5";
            Number b = 'a';

            var result = a - b;

            Assert.Equal("(8/5 - a)", result.ToString());
        }

        [Fact]
        public void VariableSubtractsFraction()
        {
            Number a = 'a';
            Number b = "8/5";

            var result = a - b;

            Assert.Equal("(8/5 - a)", result.ToString());
        }

        [Fact]
        public void VariableSubtractsVariable()
        {
            Number a = 'a';
            Number b = 'b';

            var result = a - b;

            Assert.Equal("(a - b)", result.ToString());
        }

        [Fact]
        public void IntegerMultipliesVariable()
        {
            Number a = 7;
            Number b = 'a';

            var result = a * b;

            Assert.Equal("7a", result.ToString());
        }

        [Fact]
        public void VariableMultipliesInteger()
        {
            Number a = 'a';
            Number b = 7;

            var result = a * b;

            Assert.Equal("7a", result.ToString());
        }

        [Fact]
        public void FractionMultipliesVariable()
        {
            Number a = "8/5";
            Number b = 'a';

            var result = a * b;

            Assert.Equal("(8/5 x a)", result.ToString());
        }

        [Fact]
        public void VariableMultipliesFraction()
        {
            Number a = 'a';
            Number b = "8/5";

            var result = a * b;

            Assert.Equal("(8/5 x a)", result.ToString());
        }

        [Fact]
        public void VariableMultipliesVariable()
        {
            Number a = 'a';
            Number b = 'b';

            var result = a * b;

            Assert.Equal("ab", result.ToString());
        }

        [Fact]
        public void IntegerDividesVariable()
        {
            Number a = 7;
            Number b = 'a';

            var result = a / b;

            Assert.Equal("7/a", result.ToString());
        }

        [Fact]
        public void VariableDividesInteger()
        {
            Number a = 'a';
            Number b = 7;

            var result = a / b;

            Assert.Equal("a/7", result.ToString());
        }

        [Fact]
        public void FractionDividesVariable()
        {
            Number a = "8/5";
            Number b = 'a';

            var result = a / b;

            Assert.Equal("(8/5)/a", result.ToString());
        }

        [Fact]
        public void VariableDividesFraction()
        {
            Number a = 'a';
            Number b = "8/5";

            var result = a / b;

            Assert.Equal("a/(8/5)", result.ToString());
        }

        [Fact]
        public void VariableDividesVariable()
        {
            Number a = 'a';
            Number b = 'b';

            var result = a / b;

            Assert.Equal("a/b", result.ToString());
        }

        [Fact]
        public void VariableMultipliedByOne()
        {
            Number a = 'a';
            Number b = 1;

            var result = a * b;

            Assert.Equal("a", result.ToString());
        }

        [Fact]
        public void VariableMultipliedByNegativeOne()
        {
            Number a = 'a';
            Number b = -1;

            var result = a * b;

            Assert.Equal("-a", result.ToString());
        }
    }
}
