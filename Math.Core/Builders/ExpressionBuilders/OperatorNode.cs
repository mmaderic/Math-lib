using Math.Core.Abstractions;
using Math.Core.Enumerations;
using Math.Core.Extensions;
using System;

namespace Math.Core.Builders.ExpressionBuilders
{
    internal class OperatorNode : Node
    {
        public readonly Operator Operator;

        public OperatorNode(IBuilder builder, Operator @operator) : base(builder)
        {
            if (@operator == Operator.None)
                throw new InvalidOperationException("Invalid builder state.");

            Operator = @operator;
        }           

        public override string ToString()
            => $" {Operator.Sign()} ";        
    }
}
