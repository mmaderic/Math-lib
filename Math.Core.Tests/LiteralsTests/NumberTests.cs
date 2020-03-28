using Math.Core.Literals;
using Xunit;

namespace Math.Core.Tests.LiteralsTests
{
    public class NumberTests
    {
        [Fact]
        public void ImplicitLongToIntegerOperator()
        {
            Number integer = 91239768715135;

            Assert.True(integer is Integer);
        }

        [Fact]
        public void ImplicitCharToVariableOperator()
        {
            Number variable = 'a';

            Assert.True(variable is Variable);
        }

        [Fact]
        public void ImplicitStringToInteger()
        {
            Number integer = "2357";

            Assert.True(integer is Integer && integer.ToString() == "2357");
        }

        [Fact]
        public void ImplicitStringToSingleFraction()
        {
            Number fractionA = "257/432";
            Number fractionB = "(257/432)";

            Assert.True(fractionA is Fraction && fractionA.ToString() == "257/432");
            Assert.True(fractionB is Fraction && fractionB.ToString() == "257/432");
        }
    }
}
