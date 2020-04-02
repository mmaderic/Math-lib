using Math.Core.Abstractions;
using Math.Core.Literals;

namespace Math.Core.Builders.Nodes
{
    internal class VariableNode : INode, INumberFactory
    {
        Number INumberFactory.Construct()
        {
            Number result = new Variable(_variable);
            if (_isNegative)
                result *= -1;

            return result;
        }

        public IBuilder Builder { get; }
        private bool _isNegative;

        private readonly char _variable;

        public VariableNode(IBuilder builder, char variable)
        {
            Builder = builder;
            _variable = variable;
        }

        public override string ToString()
            => _variable.ToString();

        public void Negate()
            => _isNegative ^= true;
    }
}
