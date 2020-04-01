using Math.Core.Enumerations;
using System.Collections.Generic;

namespace Math.Core.Abstractions
{
    public interface IBuilder
    {
        public List<INode> Nodes { get; }

        public void UseDefaultCommander();
        public void ExecuteCommand(BuilderCommand command, char? character = null);
    }
}
