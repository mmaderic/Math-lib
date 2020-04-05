using Math.Core.Builders;

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

        public static implicit operator Number(string input)
            => new ExpressionBuilder(input).SolveExpression();

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
    }
}
