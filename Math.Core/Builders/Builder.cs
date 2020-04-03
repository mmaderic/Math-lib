using Math.Core.Abstractions;
using Math.Core.Builders.Nodes;
using Math.Core.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Math.Core.Builders
{
    public abstract class Builder : IBuilder
    {
        public List<INode> Nodes { get; }
        protected Action<BuilderCommand, char?> Commander { get; set; }

        public Builder()
        {
            Commander = DefaultCommander;
            Nodes = new List<INode>();
        }

        public void ExecuteCommand(BuilderCommand command, char? character = null)        
            => Commander.Invoke(command, character);               

        public virtual void UseDefaultCommander()
            => Commander = DefaultCommander;

        public virtual void DefaultCommander(BuilderCommand command, char? character)
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
