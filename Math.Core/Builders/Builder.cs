using Math.Core.Abstractions;
using Math.Core.Enumerations;
using System;
using System.Collections.Generic;

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

        public abstract void DefaultCommander(BuilderCommand command, char? character);              
    }
}
