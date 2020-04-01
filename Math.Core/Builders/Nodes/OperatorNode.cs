using Math.Core.Abstractions;
using Math.Core.Enumerations;
using Math.Core.Extensions;
using System;

namespace Math.Core.Builders.Nodes
{
    internal class OperatorNode : INode
    {
        public IBuilder Builder { get; }
        public Operator Operator { get; }

        public OperatorNode(IBuilder builder, Operator @operator)
        {
            if (@operator == Operator.None)
                throw new InvalidOperationException("Invalid builder state.");

            Builder = builder;
            Operator = @operator;
        }

        public override string ToString()
            => $" {Operator.Sign()} ";        
    }
}
