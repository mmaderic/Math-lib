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
        public void ImplicitStringToVariable()
        {
            Number variable = "d";

            Assert.True(variable is Variable && variable.ToString() == "d");
        }
    }
}
