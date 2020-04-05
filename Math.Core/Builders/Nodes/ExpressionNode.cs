using Math.Core.Abstractions;
using Math.Core.Enumerations;
using Math.Core.Literals;
using System;
using System.Linq;
using System.Text;

namespace Math.Core.Builders.Nodes
{
    internal class ExpressionNode : ExpressionBuilder, INode, INumberFactory
    {
        public IBuilder Builder { get; }
        private bool _isNegative;

        Number INumberFactory.Construct()
        {
            var calculator = new NodeCalculator(Nodes);
            var result = calculator.Calculate();

            if (_isNegative)
                result = result *= -1;

            return result;
        }

        public ExpressionNode(IBuilder builder)        
            => Builder = builder;        

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.Append('(');

            foreach (var node in Nodes)
                builder.Append(node.ToString());
            
            builder.Append(')');

            return builder.ToString();
        }

        public override void DefaultCommander(BuilderCommand command, char? character)
        {
            switch (command)
            {
                case BuilderCommand.OpenBrackets:
                    OpenBrackets();
                    break;
                case BuilderCommand.CloseBrackets:
                    CloseBrackets();
                    break;
                case BuilderCommand.EmptySpace:
                    break;
                case BuilderCommand.InsertInteger:
                    InsertInteger(character.Value);
                    break;
                case BuilderCommand.InsertVariable:
                    InsertVariable(character.Value);
                    break;
                case BuilderCommand.Add:
                    InsertOperator(Operator.Addition);
                    break;
                case BuilderCommand.Subtract:
                    InsertOperator(Operator.Subtraction);
                    break;
                case BuilderCommand.Multiply:
                    InsertOperator(Operator.Multiplication);
                    break;
                case BuilderCommand.Divide:
                    InsertOperator(Operator.Division);
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        public void Negate()
            => _isNegative ^= true;

        protected override void CloseBrackets()
        {
            var lastNode = Nodes.LastOrDefault();
            if (lastNode is OperatorNode || lastNode is null)
                throw new InvalidOperationException("Invalid expression statement.");

            Builder.UseDefaultCommander();
        }
    }
}