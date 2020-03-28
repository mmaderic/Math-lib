using Math.Core.Literals;

namespace Math.Core.Extensions
{
    public static class ExpressionExtensions
    {
        public static bool IsSolved(this Number number)
            => !(number is Expression expression) || expression.IsSolved;
    }
}
