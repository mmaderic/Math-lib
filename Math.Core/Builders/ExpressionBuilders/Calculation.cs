using Math.Core.Enumerations;
using Math.Core.Extensions;
using Math.Core.Literals;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Math.Core.Builders.ExpressionBuilders
{
    internal class Calculation : IComparable<Calculation>
    {
        private readonly Number _left;
        private readonly Number _right;
        private readonly Operator _operator;

        public Func<Number> Left { get; set; }
        public Func<Number> Right { get; set; }
        
        public Calculation LeftReference { get; set; }
        public Calculation RightReference { get; set; }

        public Calculation(Number left, Operator @operator, Number right)
        {
            _operator = @operator;
            _left = left;
            _right = right;

            Left = () => _left;
            Right = () => _right;
        }        

        public int CompareTo([AllowNull] Calculation other)
        {
            if (_operator.IsComplementary(other._operator))
                return 0;

            if ((_operator == Operator.Addition || _operator == Operator.Subtraction) &&
                other._operator == Operator.Multiplication || other._operator == Operator.Division)
                return 1;

            return -1;
        }

        public Number Calculate()
        {
            return _operator switch
            {
                Operator.Addition => Left.Invoke() + Right.Invoke(),
                Operator.Subtraction => Left.Invoke() - Right.Invoke(),
                Operator.Multiplication => Left.Invoke() * Right.Invoke(),
                Operator.Division => Left.Invoke() / Right.Invoke(),
                Operator.None when Right is null => Left.Invoke(),

                _ => throw new NotImplementedException("Operator not implemented."),
            };
        }

        public bool IsLinkedOnLeft(Calculation leftCalculation)
            => ReferenceEquals(_left, leftCalculation._right);

        public bool IsLinkedOnRight(Calculation rightCalculation)
            => ReferenceEquals(_right, rightCalculation._left);

        public void UseDefaultLeftValue()
            => Left = () => _left;

        public void UseDefaultRightValue()
            => Right = () => _right;
    }
}
