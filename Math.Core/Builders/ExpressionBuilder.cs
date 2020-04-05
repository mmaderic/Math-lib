using Math.Core.Abstractions;
using Math.Core.Builders.Nodes;
using Math.Core.Enumerations;
using Math.Core.Literals;
using System;
using System.Linq;

namespace Math.Core.Builders
{
    internal class ExpressionBuilder : Builder
    {
        public ExpressionBuilder(string input)
        {      
            for (var i = 0; i < input.Length; i++)
            {
                var character = input[i];

                if (character == '(')
                    ExecuteCommand(BuilderCommand.OpenBrackets);

                else if (character == ')')
                    ExecuteCommand(BuilderCommand.CloseBrackets);

                else if (character == ' ')
                    ExecuteCommand(BuilderCommand.EmptySpace);

                else if (character == '+')
                    ExecuteCommand(BuilderCommand.Add);

                else if (character == '-')
                    ExecuteCommand(BuilderCommand.Subtract);

                else if (character == 'x')
                    ExecuteCommand(BuilderCommand.Multiply);

                else if (character == '/')
                    ExecuteCommand(BuilderCommand.Divide);

                else if (char.IsNumber(character))
                    ExecuteCommand(BuilderCommand.InsertInteger, character);

                else if (char.IsLower(character))
                    ExecuteCommand(BuilderCommand.InsertVariable, character);

                else
                    throw new InvalidOperationException("Invalid expression statement.");
            }
        }

        protected ExpressionBuilder()
        {
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
                    EmptySpace();
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
                case BuilderCommand.InsertInteger:
                    InsertInteger(character.Value);
                    break;
                case BuilderCommand.InsertVariable:
                    InsertVariable(character.Value);
                    break;
                default:
                    throw new NotImplementedException("Builder command not implemented.");
            }
        }

        public Number SolveExpression()
        {
            var calculator = new NodeCalculator(Nodes);

            return calculator.Calculate();
        }

        protected virtual void OpenBrackets()
        {
            var lastNode = Nodes.LastOrDefault();
            if (!(lastNode is OperatorNode) && !(lastNode is null))
                Nodes.Add(new OperatorNode(this, Operator.Multiplication));

            var newNode = new ExpressionNode(this);
            Nodes.Add(newNode);

            if (newNode is IBuilder builderNode)
                Commander = builderNode.ExecuteCommand;
        }

        protected virtual void CloseBrackets()
        {
            var lastNode = Nodes.LastOrDefault();
            if (lastNode is null || !(lastNode is IBuilder builderNode))
                throw new InvalidOperationException("Invalid expression statement.");

            builderNode.ExecuteCommand(BuilderCommand.CloseBrackets);
        }

        protected virtual void EmptySpace()
        {
        }

        protected virtual void InsertOperator(Operator @operator)
        {
            var lastNode = Nodes.LastOrDefault();
            if (lastNode is null && @operator != Operator.Addition && @operator != Operator.Subtraction)
                throw new InvalidOperationException("Invalid expression statement.");

            else if (lastNode is OperatorNode)
                throw new InvalidOperationException("Invalid expression statement.");

            if (lastNode is null && @operator == Operator.Addition)
                return;

            var newNode = new OperatorNode(this, @operator);
            Nodes.Add(newNode);

            if (newNode is IBuilder builderNode)
                Commander = builderNode.ExecuteCommand;
        }

        protected virtual void InsertInteger(char character)
        {
            var lastNode = Nodes.LastOrDefault();
            if (!(lastNode is OperatorNode) && !(lastNode is null))
                Nodes.Add(new OperatorNode(this, Operator.Multiplication));

            var newNode = new IntegerNode(this, character);
            Nodes.Add(newNode);

            if (newNode is IBuilder builderNode)
                Commander = builderNode.ExecuteCommand;
        }

        protected virtual void InsertVariable(char character)
        {
            var lastNode = Nodes.LastOrDefault();
            if (!(lastNode is OperatorNode) && !(lastNode is null))
                Nodes.Add(new OperatorNode(this, Operator.Multiplication));

            var newNode = new VariableNode(this, character);
            Nodes.Add(newNode);

            if (newNode is IBuilder builderNode)
                Commander = builderNode.ExecuteCommand;
        }        
    }
}
