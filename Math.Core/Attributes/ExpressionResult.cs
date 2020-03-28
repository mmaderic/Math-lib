using System;

namespace Math.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    internal class ExpressionResultAttribute : Attribute
    {
        public bool ReverseCalculation { get; }

        public ExpressionResultAttribute() { }

        public ExpressionResultAttribute(bool reverseCalculation)
            => ReverseCalculation = reverseCalculation;
    }
}
