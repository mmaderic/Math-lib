using Math.SetTheory.Literals;
using Xunit;

namespace Math.SetTheory.Tests.BuildersTests
{
    public class SetBuilderTests
    {
        [Fact]
        public void ImplicitStringToSet()
        {
            Set set = "{13,2,4}";

            Assert.Equal("{13, 2, 4}", set.ToString());
        }
    }
}
