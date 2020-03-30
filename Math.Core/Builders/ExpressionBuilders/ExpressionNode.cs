using Math.Core.Abstractions;
using Math.Core.Enumerations;
using Math.Core.Literals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Math.Core.Builders.ExpressionBuilders
{
    internal class ExpressionNode : Node, IBuilder, INumberFactory
    {
        void IBuilder.UseDefaultCommander()
            => Commander = DefaultCommander;

        Number INumberFactory.Construct()
        {
            var calculator = new NodeCalculator(_nodes);

            return calculator.Calculate();
        }

        private readonly List<Node> _nodes;

        public Action<BuilderCommand, char?> Commander { get; set; }

        public ExpressionNode(IBuilder builder) : base(builder)
        {
            Commander = DefaultCommander;
            _nodes = new List<Node>();
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.Append('(');

            foreach (var node in _nodes)
                builder.Append(node.ToString());
            
            builder.Append(')');

            return builder.ToString();
        }

        public void ExecuteCommand(BuilderCommand command, char? character = null)
            => Commander.Invoke(command, character);

        private void DefaultCommander(BuilderCommand command, char? character)
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
                case BuilderCommand.InsertInteger:
                    InsertInteger(node, character.Value);
                    break;
                case BuilderCommand.InsertVariable:
                    InsertVariable(node, character.Value);
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
                default:
                    throw new NotImplementedException();
            }
        }

        private void OpenBrackets(Node node)
        {
            if (!(node is OperatorNode) && !(node is null))
                _nodes.Add(new OperatorNode(this, Operator.Multiplication));

            node = new ExpressionNode(this);
            _nodes.Add(node);

            var builder = (IBuilder)node;
            Commander = builder.ExecuteCommand;
        }

        private void CloseBrackets(Node node)
        {
            if (node is OperatorNode || node is null)
                throw new InvalidOperationException("Invalid expression statement.");

            Builder.UseDefaultCommander();
        }       
        
        private void InsertInteger(Node node, char character)
        {
            if (node is null || node is OperatorNode)
                node = new IntegerNode(this);

            else if (!(node is OperatorNode))
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