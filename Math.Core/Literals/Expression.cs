using Math.Core.Attributes;
using Math.Core.Enumerations;
using Math.Core.Extensions;
using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Math.Core.Literals
{   
    public class Expression : Number
    {
        private readonly Operator _operator = Operator.None;
        private readonly Number _left;
        private readonly Number _right;

        public override bool IsNegative => _left.IsNegative;
        public bool IsSolved => _right is null;
        public Number Result => IsSolved ? _left : null;

        public Expression(Number left, Operator @operator, Number right)
        {
            _left = left;

            var stackTrace = new StackTrace();
            var method = stackTrace.GetFrame(1).GetMethod();
            var expressionAttribute = method.GetCustomAttributes()
                .SingleOrDefault(x => typeof(ExpressionResultAttribute).IsAssignableFrom(x.GetType()));

            if (!(expressionAttribute is null))
            {
                _right = right;
                _operator = @operator;
            }
            else
            {
                Func<Number, Number> solution = @operator switch
                {
                    Operator.Addition => _left.Add,
                    Operator.Subtraction => _left.Subtract,
                    Operator.Multiplication => _left.Multiply,
                    Operator.Division => _left.Divide,

                    _ => throw new NotImplementedException("Operator not implemented."),
                };

                expressionAttribute = solution.Method.GetCustomAttributes()
                    .SingleOrDefault(x => typeof(ExpressionResultAttribute).IsAssignableFrom(x.GetType()));

                if (expressionAttribute is null)
                {
                    _left = solution.Invoke(right);
                    return;
                }
                else
                {
                    _right = right;
                    _operator = @operator;
                }
            }

            if (_left is Variable && !(_right is Variable))
            {
                var tmp = _left;
                _left = _right;
                _right = tmp;
            }
            else if (_left is Variable varA && _right is Variable varB)
            {
                var arr = new[] { varA, varB };
                _left = arr.Min();
                _right = arr.Max();
            }
        }        

        public override string ToString()
        {
            var builder = new StringBuilder();

            if (_operator == Operator.None)
                builder.Append($"{_left}");

            else if (_operator == Operator.Multiplication && !(_left is Expression) && !(_left is Fraction))
                builder.Append($"{_left}{_right}");

            else
                builder.Append($"({_left} {_operator.Sign()} {_right})");            

            return builder.ToString();            
        }

        public override bool Equals(object obj)
        {
            if (obj is Expression expression)
                return expression._left == _left && expression._right == _right && expression._operator == _operator;

            if (!IsSolved)
                return false;

            return obj.Equals(Result);
        }

        public override int GetHashCode()
            => HashCode.Combine(_left, _right, _operator);

        [ExpressionResult]
        internal override Number Add(Number number)
            => SolveExpression(Operator.Addition, number);

        [ExpressionResult]
        internal override Number Subtract(Number number)
            => SolveExpression(Operator.Subtraction, number);

        [ExpressionResult]
        internal override Number Multiply(Number number)
            => SolveExpression(Operator.Multiplication, number);

        [ExpressionResult]
        internal override Number Divide(Number number) 
            => SolveExpression(Operator.Division, number);

        internal override Number ReverseSubtract(Number number)
            => number.Subtract(this);

        internal override Number ReverseDivide(Number number)
            => number.Divide(this);

        internal override bool Equals(Number number)
        {
            if (number is Expression expression)
                return expression._left == _left && expression._right == _right && expression._operator == _operator;

            if (!IsSolved)
                return false;

            return Result == number;
        }

        private Number SolveExpression(Operator @operator, Number number)
        {
            var leftMethod = ResolveMethod(_left, @operator); 
            var rightMethod = ResolveMethod(_right, @operator);

            if (_operator == Operator.None)
            {
                var result = leftMethod.Invoke(number);
                if (result.IsSolved())
                    return result;

                return new Expression(_left, @operator, number);
            }

            if (_operator.IsComplementary(@operator))
            {
                var result = leftMethod.Invoke(number);
                if (result.IsSolved())
                    return new Expression(result, _operator, _right);

                result = rightMethod.Invoke(number);
                if (result.IsSolved())
                    return new Expression(_left, _operator, result);

                return new Expression(new Expression(_left, _operator, _right), @operator, number);
            }
            else
            {
                if (@operator == Operator.Division)                
                    return new Fraction(new Expression(_left, _operator, _right), number);                
                else                
                    return new Expression(new Expression(_left, _operator, _right), @operator, number);                 
            }
        }

        private Func<Number, Number> ResolveMethod(Number member, Operator @operator)
        {
            if (member is null)
                return null;

            return @operator switch
            {
                Operator.Addition => member.Add,
                Operator.Subtraction => member.Subtract,
                Operator.Multiplication => member.Multiply,
                Operator.Division => member.Divide,

                _ => throw new NotImplementedException("Operator not implemented."),
            };
        }        
    }    
}
