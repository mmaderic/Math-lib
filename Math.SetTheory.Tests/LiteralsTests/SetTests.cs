using Math.SetTheory.Literals;
using Xunit;

namespace Math.SetTheory.Tests.LiteralsTests
{
    public class SetTests
    {
        [Fact]
        public void InvalidIdentifierException()
        {
            /*var ex = Assert.Throws<InvalidOperationException>(() => new Set('a'));

            Assert.Equal("Set identifier should be upper case letter.", ex.Message);*/
        }        
    }
}
