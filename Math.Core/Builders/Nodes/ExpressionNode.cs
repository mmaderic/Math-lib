﻿using Math.Core.Abstractions;
using Math.Core.Builders.ExpressionBuilders;
using Math.Core.Enumerations;
using Math.Core.Literals;
using System;
using System.Linq;
using System.Text;

namespace Math.Core.Builders.Nodes
{
    internal class ExpressionNode : Builder, INode, INumberFactory
    {
        public IBuilder Builder { get; }

        Number INumberFactory.Construct()
        {
            var calculator = new NodeCalculator(Nodes);

            return calculator.Calculate();
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

        public override void UseDefaultCommander()
            => Commander = DefaultCommander;

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

        protected override void CloseBrackets()
        {
            var lastNode = Nodes.LastOrDefault();
            if (lastNode is OperatorNode || lastNode is null)
                throw new InvalidOperationException("Invalid expression statement.");

            Builder.UseDefaultCommander();
        }   
    }
}