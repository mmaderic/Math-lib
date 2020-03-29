using Math.Core.Abstractions;

namespace Math.Core.Builders.ExpressionBuilders
{
    internal abstract class Node
    {
        protected readonly IBuilder Builder;

        public abstract override string ToString();

        public Node(IBuilder builder)
            => Builder = builder;
    }
}
