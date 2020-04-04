using Math.Core.Enumerations;
using Math.Core.Extensions;
using Math.Core.Literals;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Math.Core.Builders
{
    internal class Calculation : IComparable<Calculation>
    {
        public Number Left { get; }
        public Number Right { get; }
        public Operator Operator { get; }

        public Calculation(Number left, Operator @operator, Number right)
        {
            Operator = @operator;
            Left = left;
            Right = right;
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
                Operator.Addition => Left + Right,
                Operator.Subtraction => Left - Right,
                Operator.Multiplication => Left * Right,
                Operator.Division => Left / Right,
                Operator.None when Right is null => Left,

                _ => throw new NotImplementedException("Operator not implemented."),
            };
        }  

        public override string ToString()
            => $"{Left} {Operator.Sign()} {Right}";
    }
}
