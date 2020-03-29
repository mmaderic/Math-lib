using Math.Core.Enumerations;
using Math.Core.Extensions;
using Math.Core.Literals;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Math.Core.Builders.ExpressionBuilders
{
    internal class Calculation : IComparable<Calculation>
    {
        public Func<Number> Left;
        public Func<Number> Right;

        public Number LeftNumber { get; set; }
        public Number RightNumber { get; set; }     
        
        public Calculation LeftReference { get; set; }
        public Calculation RightReference { get; set; }

        public Operator Operator { get; }

        public Calculation(Number left, Operator @operator, Number right)
        {
            Operator = @operator;
            LeftNumber = left;
            RightNumber = right;

            Left = () => LeftNumber;
            Right = () => RightNumber;
        }        

        public int CompareTo([AllowNull] Calculation other)
        {
            if (Operator.IsComplementary(other.Operator))
                return 0;

            if ((Operator == Operator.Addition || Operator == Operator.Subtraction) &&
                other.Operator == Operator.Multiplication || other.Operator == Operator.Division)
                return 1;

            return -1;
        }

        public Number Calculate()
        {
            return Operator switch
            {
                Operator.Addition => Left.Invoke() + Right.Invoke(),
                Operator.Subtraction => Left.Invoke() - Right.Invoke(),
                Operator.Multiplication => Left.Invoke() * Right.Invoke(),
                Operator.Division => Left.Invoke() / Right.Invoke(),
                Operator.None when Right is null => Left.Invoke(),

                _ => throw new NotImplementedException("Operator not implemented."),
            };
        }
    }
}
