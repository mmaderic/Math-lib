using Math.Core.Literals;

namespace Math.Core.Abstractions
{
    public interface INumberFactory
    {
        public Number Construct();
        public void Negate();
    }
}
