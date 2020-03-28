
using System.Linq;
using System.Text.RegularExpressions;

namespace Math.Core.Literals
{
    public abstract class Number
    {
        public abstract bool IsNegative { get; }

        public static Number operator +(Number a, Number b)
            => a.Add(b);

        public static Number operator -(Number a, Number b)
            => a.Subtract(b);

        public static Number operator *(Number a, Number b)
            => a.Multiply(b);

        public static Number operator /(Number a, Number b)
            => a.Divide(b);

        public static bool operator ==(Number a, Number b)
            => a.Equals(b);

        public static bool operator !=(Number a, Number b)
            => !a.Equals(b);

        public static implicit operator Number(long value)
            => new Integer(value);

        public static implicit operator Number(char value)
            => new Variable(value);

        public static implicit operator Number(string value)
            => ConstructFromString(value);

        public abstract override string ToString();
        public abstract override bool Equals(object obj);
        public abstract override int GetHashCode();

        internal abstract Number Add(Number number);
        internal abstract Number Subtract(Number number);
        internal abstract Number Multiply(Number number);
        internal abstract Number Divide(Number number);

        internal abstract Number ReverseSubtract(Number number);
        internal abstract Number ReverseDivide(Number number);

        internal abstract bool Equals(Number number);

        private static Number ConstructFromString(string input)
        {
            var integer = IntegerMatch(input);
            if (!(integer is null))
                return integer;

            var singleFraction = SingleFractionMatch(input);
            if (!(singleFraction is null))
                return singleFraction;

            return default;
        }

        private static Integer IntegerMatch(string input)
        {
            var integerRegex = new Regex("^[[0-9]*$");
            var integerMatch = integerRegex.Matches(input).SingleOrDefault();

            if (integerMatch is null)
                return null;

            var result = integerMatch.ToString();

            return new Integer(result);
        }

        private static Fraction SingleFractionMatch(string input)
        {
            var singleFractionRegex = new Regex("^[0-9]*[/][0-9]*$|^[(][0-9]*[/][0-9]*[)]$");
            var singleFractionMatch = singleFractionRegex.Matches(input).SingleOrDefault();

            if (singleFractionMatch is null)
                return null;
            
            var result = singleFractionMatch.ToString().Replace("(", "").Replace(")", "");

            return new Fraction(result); 
        }        
    }
}
