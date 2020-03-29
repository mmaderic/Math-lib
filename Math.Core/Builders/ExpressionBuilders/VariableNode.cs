using Math.Core.Abstractions;
using Math.Core.Literals;

namespace Math.Core.Builders.ExpressionBuilders
{
    internal class VariableNode : Node, INumberFactory
    {
        Number INumberFactory.Construct()
            => new Variable(_variable);

        private readonly char _variable;

        public VariableNode(IBuilder builder, char variable) : base(builder)        
            => _variable = variable;

        public override string ToString()
            => _variable.ToString();        
    }
}
