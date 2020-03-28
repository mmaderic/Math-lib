using Math.Core.Attributes;
using Math.Core.Enumerations;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Math.Core.Literals
{
    public class Variable : Number, IComparable<Variable>
    {
        private readonly char _sign;
        private readonly bool _isNegative;

        public override bool IsNegative 
            => _isNegative;       

        public Variable(char sign)
        {
            if (sign < 97 || sign > 122)
                throw new InvalidOperationException("Variable sign should be lower case letter.");

            _sign = sign;
        }

        public Variable(char sign, bool isNegative) : this(sign)
            => _isNegative = isNegative;            

        public override string ToString()
            => $"{(IsNegative ? "-" : "")}{_sign}";

        public int CompareTo([AllowNull] Variable other)
            => _sign.CompareTo(other._sign);

        public override bool Equals(object obj)
        {
            if (obj is Variable variable)
                return _sign == variable._sign && IsNegative == variable.IsNegative;

            if (obj is Expression expression)
                return expression.Result == this;

            return false;
        }

        public override int GetHashCode()
            => HashCode.Combine(IsNegative, _sign);

        [ExpressionResult]
        internal override Number Add(Number number)
            => new Expression(this, Operator.Addition, number);

        [ExpressionResult]
        internal override Number Subtract(Number number)
            => new Expression(this, Operator.Subtraction, number);

        [ExpressionResult]
        internal override Number Multiply(Number number)
        {
            if (number == 1)
                return this;

            if (number == -1)
                return new Variable(_sign, true);

            return new Expression(this, Operator.Multiplication, number);
        }

        [ExpressionResult(reverseCalculation: true)]
        internal override Number ReverseSubtract(Number number)
            => new Expression(number, Operator.Subtraction, this);

        internal override Number Divide(Number number)
            => new Fraction(this, number);

        internal override Number ReverseDivide(Number number)
            => new Fraction(number, this);

        internal override bool Equals(Number number)
        {
            if (number is Variable variable)
                return _sign == variable._sign && IsNegative == variable.IsNegative;

            if (number is Expression expression)
                return expression.Result == this;

            return false;
        }       
    }
}
