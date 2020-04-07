using Math.SetTheory.Literals;
using Xunit;

namespace Math.SetTheory.Tests.BuildersTests
{
    public class SetBuilderTests
    {
        [Fact]
        public void ImplicitStringToSet()
        {
            Set setA = "{13,2,4}";
            Set setB = "B = {15, 17, 1}";

            Assert.Equal("{13, 2, 4}", setA.ToString());
            Assert.Equal("B = {15, 17, 1}", setB.ToString());
        }
    }
}
