using Math.Core.Literals;
using Math.SetTheory.Abstractions;

namespace Math.SetTheory.Literals
{
    internal class IntegerElement : Integer, IElement
    {
        public IntegerElement(long value) : base(value)
        {
        }
    }
}
