using System;
using System.Linq;

namespace Math.Core.Literals
{
    public class Fraction : Number
    {
        private readonly Number _numerator;
        private readonly Number _denominator;

        public override bool IsNegative 
            => _numerator.IsNegative;        

        public Fraction(Number numerator, Number denominator)
        {
            if (denominator == 0)
                throw new InvalidOperationException("Denominator is not allowed to have a value of zero.");

            _numerator = numerator;
            _denominator = denominator;

            static Number GCD(Number a, Number b)
            {
                if (!(a is Integer integerA) || !(b is Integer integerB))
                    return null;

                if (integerB != 0)
                    return GCD(integerB, integerA % integerB);
                else
                    return integerA;
            }

            var gcd = GCD(numerator, denominator);
            if (!(gcd is null))
            {
                _numerator /= gcd;
                _denominator /= gcd;
            }

            if (_denominator.IsNegative)
            {
                _denominator *= -1;
                _numerator *= -1;
            }
        }

        public Fraction(string input) : this(ReadStringInput(input)) { }
        public Fraction((Number Numerator, Number Denominator) value) : this(value.Numerator, value.Denominator) { }        

        public static Number Divide(Integer a, Integer b)
        {
            if (a % b == 0)
                return new Integer(a / b);

            return new Fraction(a, b);
        }

        public override string ToString()
        {
            var numerator = _numerator is Fraction ? $"({_numerator})" : $"{_numerator}";
            var denominator = _denominator is Fraction ? $"({_denominator})" : $"{_denominator}";

            return $"{numerator}/{denominator}";
        }

        public override bool Equals(object obj)
        {
            if (obj is Integer integer)
                return _numerator == integer && _denominator == 1;

            if (obj is Fraction fraction)
                return _numerator == fraction._numerator && _denominator == fraction._denominator;

            return obj.Equals(this);
        }

        public override int GetHashCode()        
            => HashCode.Combine(_numerator, _denominator);        

        public static Fraction operator +(Fraction a, Fraction b)
        {
            var numerator = a._numerator * b._denominator + b._numerator * a._denominator;
            var denominator = a._denominator * b._denominator;

            return new Fraction(numerator, denominator);
        }

        public static Fraction operator -(Fraction a, Fraction b)
        {
            var numerator = a._numerator * b._denominator - b._numerator * a._denominator;
            var denominator = a._denominator * b._denominator;

            return new Fraction(numerator, denominator);
        }

        public static Fraction operator *(Fraction a, Fraction b)
        {
            var numerator = a._numerator * b._numerator;
            var denominator = a._denominator * b._denominator;

            return new Fraction(numerator, denominator);
        }

        public static Fraction operator /(Fraction a, Fraction b)
        {
            var numerator = a._numerator * b._denominator;
            var denominator = a._denominator * b._numerator;

            return new Fraction(numerator, denominator);
        }

        internal override Number Add(Number number)
        {
            if (number is Integer integer)
                return this + new Fraction(integer, 1);

            if (number is Fraction fraction)
                return this + fraction;

            return number.Add(this);
        }

        internal override Number Subtract(Number number)
        {
            if (number is Integer integer)
                return this - new Fraction(integer, 1);

            if (number is Fraction fraction)
                return this - fraction;

            return number.ReverseSubtract(this);
        }

        internal override Number Multiply(Number number)
        {
            if (number is Integer integer)
                return this * new Fraction(integer, 1);

            if (number is Fraction fraction)
                return this * fraction;

            return number.Multiply(this);
        }

        internal override Number Divide(Number number)
        {
            if (number is Integer integer)
                return this / new Fraction(integer, 1);

            if (number is Fraction fraction)
                return this / fraction;

            return number.ReverseDivide(this);
        }

        internal override Number ReverseSubtract(Number number)
        {
            if (number is Integer integer)
                return new Fraction(integer, 1) - this;

            if (number is Fraction fraction)
                return fraction - this;

            return number.Subtract(this);
        }

        internal override Number ReverseDivide(Number number)
        {
            if (number is Integer integer)
                return new Fraction(integer, 1) / this;

            if (number is Fraction fraction)
                return fraction / this;

            return number.Divide(this);
        }

        internal override bool Equals(Number number)
        {
            if (number is Integer integer)
                return _denominator == 1 && _numerator == integer;

            if (number is Fraction fraction)
                return _numerator == fraction._numerator && _denominator == fraction._denominator;

            return number == this;
        }

        private static (Number, Number) ReadStringInput(string input)
        {
            var splits = input.Split("/");

            return (long.Parse(splits.First()), long.Parse(splits.Last()));
        }       
    }
}
