using Math.Core.Abstractions;
using Math.Core.Literals;

namespace Math.Core.Builders.Nodes
{
    internal class VariableNode : INode, INumberFactory
    {
        Number INumberFactory.Construct()
            => new Variable(_variable);

        public IBuilder Builder { get; }

        private readonly char _variable;

        public VariableNode(IBuilder builder, char variable)
        {
            Builder = builder;
            _variable = variable;
        }

        public override string ToString()
            => _variable.ToString();        
    }
}
