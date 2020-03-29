using Math.Core.Enumerations;
using System;

namespace Math.Core.Abstractions
{
    public interface IBuilder
    {
        public Action<BuilderCommand, char?> Commander { get; set; }

        public void UseDefaultCommander();
        public void ExecuteCommand(BuilderCommand command, char? character = null);
    }
}
