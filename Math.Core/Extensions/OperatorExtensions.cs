using Math.Core.Enumerations;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Math.Core.Extensions
{
    public static class OperatorExtensions
    {
        public static string Sign(this Operator @operator)
        {
            var type = @operator.GetType();
            var member = type.GetMember(@operator.ToString()).First();
            var attribute = member.GetCustomAttributes().Select(x => (DescriptionAttribute)x).First();

            return attribute.Description;
        }

        public static bool IsComplementary(this Operator @operator, Operator otherOperator)
        {
            switch (@operator)
            {
                case Operator.Addition:
                case Operator.Subtraction:
                    return otherOperator == Operator.Addition || otherOperator == Operator.Subtraction;
                case Operator.Multiplication:
                case Operator.Division:
                    return otherOperator == Operator.Multiplication || otherOperator == Operator.Division;
                default:
                    return false;
            }
        }
    }
}
