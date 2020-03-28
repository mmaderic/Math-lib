
namespace Math.Core.Literals
{
    public class Integer : Number
    {
        private readonly long _value;

        public override bool IsNegative => _value < 0;       

        public Integer(long value)
            => _value = value;

        public Integer(string input) : this(long.Parse(input)) { }

        public override string ToString()
            => _value.ToString();

        public override bool Equals(object obj)
        {
            if (obj is Integer integer)
                return integer._value == _value;

            return obj.Equals(this);
        }

        public override int GetHashCode()
            => _value.GetHashCode();

        public static implicit operator Integer(long value)
            => new Integer(value);

        public static implicit operator long(Integer integer)
            => integer._value;

        public static Integer operator +(Integer a, Integer b)
            => new Integer(a._value + b._value);

        public static Integer operator -(Integer a, Integer b)
            => new Integer(a._value - b._value);

        public static Integer operator *(Integer a, Integer b)
            => new Integer(a._value * b._value);

        public static Integer operator /(Integer a, Integer b)
            => new Integer(a._value / b._value);

        public static Integer operator %(Integer a, Integer b)
            => new Integer(a._value % b._value);        

        internal override Number Add(Number number)
        {
            if (number is Integer integer)
                return new Integer(this + integer);

            return number.Add(this);
        }

        internal override Number Subtract(Number number)
        {
            if (number is Integer integer)
                return new Integer(this - integer);

            return number.ReverseSubtract(this);
        }

        internal override Number Multiply(Number number)
        {
            if (number is Integer integer)
                return new Integer(this * integer);

            return number.Multiply(this);
        }

        internal override Number Divide(Number number)
        {
            if (number is Integer integer)
                return Fraction.Divide(this, integer);

            return number.ReverseDivide(this);
        }

        internal override Number ReverseSubtract(Number number)
        {
            if (number is Integer integer)
                return new Integer(integer - this);

            return number.Subtract(this);
        }

        internal override Number ReverseDivide(Number number)
        {
            if (number is Integer integer)
                return Fraction.Divide(integer, this);

            return number.Divide(this);
        }

        internal override bool Equals(Number number)
        {
            if (number is Integer integer)
                return _value == integer._value;

            return number == this;
        }
    }
}
