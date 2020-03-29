using Math.Core.Abstractions;
using Math.Core.Enumerations;
using Math.Core.Literals;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Math.Core.Builders.ExpressionBuilders
{
    internal class ExpressionBuilder : IBuilder
    {
        void IBuilder.UseDefaultCommander()
            => Commander = ExecuteCommand;

        private readonly List<Node> _nodes;

        public Action<BuilderCommand, char?> Commander { get; set; }

        public ExpressionBuilder(string input)
        {
            _nodes = new List<Node>();
            Commander = ExecuteCommand;

            for (var i = 0; i < input.Length; i++)
            {
                var character = input[i];

                if (character == '(')
                    Commander.Invoke(BuilderCommand.OpenBrackets, null);

                else if (character == ')')
                    Commander.Invoke(BuilderCommand.CloseBrackets, null);

                else if (character == ' ')
                    Commander.Invoke(BuilderCommand.EmptySpace, null);

                else if (character == '+')
                    Commander.Invoke(BuilderCommand.Add, null);

                else if (character == '-')
                    Commander.Invoke(BuilderCommand.Subtract, null);

                else if (character == 'x')
                    Commander.Invoke(BuilderCommand.Multiply, null);

                else if (character == '/')
                    Commander.Invoke(BuilderCommand.Divide, null);

                else if (char.IsNumber(character))
                    Commander.Invoke(BuilderCommand.InsertInteger, character);

                else if (char.IsLower(character))
                    Commander.Invoke(BuilderCommand.InsertVariable, character);

                else
                    throw new InvalidOperationException("Invalid expression statement.");
            }
        }

        public Number SolveExpression()
        {
            var calculator = new NodeCalculator(_nodes);

            return calculator.Calculate();
        }

        public void ExecuteCommand(BuilderCommand command, char? character = null)
        {
            var node = _nodes.Count == 0 ? null : _nodes.Last();

            switch (command)
            {
                case BuilderCommand.OpenBrackets:
                    OpenBrackets(node);
                    break;
                case BuilderCommand.CloseBrackets:
                    CloseBrackets(node);
                    break;
                case BuilderCommand.EmptySpace:
                    break;
                case BuilderCommand.Add:
                    InsertOperator(node, Operator.Addition);
                    break;
                case BuilderCommand.Subtract:
                    InsertOperator(node, Operator.Subtraction);
                    break;
                case BuilderCommand.Multiply:
                    InsertOperator(node, Operator.Multiplication);
                    break;
                case BuilderCommand.Divide:
                    InsertOperator(node, Operator.Division);
                    break;
                case BuilderCommand.InsertInteger:
                    InsertInteger(node, character.Value);
                    break;
                case BuilderCommand.InsertVariable:
                    InsertVariable(node, character.Value);
                    break;
                default:
                    throw new InvalidOperationException("Invalid builder state.");
            }
        }

        private void OpenBrackets(Node node)
        {
            if (node is null || node is OperatorNode)
                node = new ExpressionNode(this);

            else if(!(node is OperatorNode))
            {
                _nodes.Add(new OperatorNode(this, Operator.Multiplication));

                node = new ExpressionNode(this);
            }

            var builder = (IBuilder)node;
            Commander = builder.ExecuteCommand;
            _nodes.Add(node);
        }

        private void CloseBrackets(Node _)
        {
            throw new InvalidOperationException("Invalid expression statement.");
        }

        private void InsertInteger(Node node, char character)
        {
            if (node is null || node is OperatorNode)            
                node = new IntegerNode(this);

            else if(!(node is OperatorNode))
            {
                node = new OperatorNode(this, Operator.Multiplication);
                _nodes.Add(node);

                node = new IntegerNode(this);
            }

            var builder = (IBuilder)node;
            Commander = builder.ExecuteCommand;
            _nodes.Add(node);

            Commander.Invoke(BuilderCommand.InsertInteger, character);
        }

        private void InsertVariable(Node node, char character)
        {
            if (node is null || node is OperatorNode)
                node = new VariableNode(this, character);

            else if (!(node is OperatorNode))
            {
                node = new OperatorNode(this, Operator.Multiplication);
                _nodes.Add(node);

                node = new VariableNode(this, character);
            }

            _nodes.Add(node);
        }

        private void InsertOperator(Node node, Operator @operator)
        {
            if (node is null || node is OperatorNode)
                throw new InvalidOperationException("Invalid expression statement.");

            node = new OperatorNode(this, @operator);
            _nodes.Add(node);
        }        
    }
}
