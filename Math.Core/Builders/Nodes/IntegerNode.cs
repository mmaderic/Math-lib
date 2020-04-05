using Math.Core.Abstractions;
using Math.Core.Enumerations;
using Math.Core.Literals;
using System;
using System.Collections.Generic;

namespace Math.Core.Builders.Nodes
{
    internal class IntegerNode : ExpressionBuilder, INode, INumberFactory
    { 
        Number INumberFactory.Construct()
        {
            var result = new Integer(long.Parse(new string(_characters.ToArray())));
            if (_isNegative)
                result *= -1;

            return result;
        }

        public IBuilder Builder { get; }
        private bool _isNegative;

        private readonly List<char> _characters;

        public IntegerNode(IBuilder builder, char character)
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
                case BuilderCommand.OpenBrackets:
                    OpenBrackets();
                    break;
                case BuilderCommand.CloseBrackets:
                    CloseBrackets();
                    break;
                case BuilderCommand.EmptySpace:
                    EmptySpace();
                    break;
                case BuilderCommand.InsertInteger:
                    InsertInteger(character.Value);
                    break;
                case BuilderCommand.Add:
                case BuilderCommand.Subtract:
                case BuilderCommand.Multiply:
                case BuilderCommand.Divide:
                    Builder.UseDefaultCommander();
                    Builder.ExecuteCommand(command);
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

        protected override void InsertInteger(char character)
            => _characters.Add(character);

        public void Negate()
            => _isNegative ^= true;
    }
}
