using Math.Core.Abstractions;
using Math.Core.Enumerations;
using Math.SetTheory.Abstractions;
using Math.SetTheory.Literals;
using System;
using System.Collections.Generic;

namespace Math.SetTheory.Builders.Nodes
{
    internal class SetIntegerNode : SetBuilder, INode, IElementFactory
    {
        IElement IElementFactory.Construct()
        {
            if (_characters.Count == 0)
                throw new InvalidOperationException("Empty node.");

            return new IntegerElement(long.Parse(new string(_characters.ToArray())));
        }

        public IBuilder Builder { get; }

        private readonly List<char> _characters;

        public SetIntegerNode(IBuilder builder, char character)
        {
            Builder = builder;
            _characters = new List<char>() { character };
        }

        public override string ToString()
            => $"{new string(_characters.ToArray())}";

        public override void DefaultCommander(BuilderCommand command, char? character)
        {
            switch (command)
            {                
                case BuilderCommand.CloseBrackets:
                    CloseBrackets();
                    break;
                case BuilderCommand.EmptySpace:
                    EmptySpace();
                    break;
                case BuilderCommand.Comma:
                    Comma();
                    break;
                case BuilderCommand.InsertInteger:
                    InsertInteger(character.Value);
                    break;                
                default:
                    throw new InvalidOperationException("Invalid builder state.");
            }
        }

        protected override void OpenBrackets()
        {
            Builder.UseDefaultCommander();
            Builder.ExecuteCommand(BuilderCommand.OpenBrackets);
        }

        protected override void CloseBrackets()
        {
            Builder.UseDefaultCommander();
            Builder.ExecuteCommand(BuilderCommand.CloseBrackets);
        }

        protected override void EmptySpace()
            => Builder.UseDefaultCommander();

        protected override void Comma()        
            => Builder.UseDefaultCommander();
        
        protected override void InsertInteger(char character)
            => _characters.Add(character);        
    }
}
