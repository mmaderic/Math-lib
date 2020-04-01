
namespace Math.Core.Abstractions
{
    public interface INode
    {
        public IBuilder Builder { get; }

        public string ToString();
    }
}
