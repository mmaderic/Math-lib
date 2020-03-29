using Math.Core.Abstractions;
using Math.Core.Enumerations;
using Math.Core.Literals;
using System;
using System.Collections.Generic;

namespace Math.Core.Builders.ExpressionBuilders
{
    internal class IntegerNode : Node, IBuilder, INumberFactory
    {
        void IBuilder.UseDefaultCommander()
            => Commander = ExecuteCommand;

        Number INumberFactory.Construct()
            => new Integer(long.Parse(new string(_characters.ToArray())));

        private readonly List<char> _characters;

        public Action<BuilderCommand, char?> Commander { get; set; }

        public IntegerNode(IBuilder builder) : base(builder)        
            => _characters = new List<char>();        

        public override string ToString()
            => $"{new string(_characters.ToArray())}";

        public void ExecuteCommand(BuilderCommand command, char? character = null)
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
                    InsertOperator(command);
                    break;
                default:
                    throw new InvalidOperationException("Invalid builder state.");
            }
        }

        private void OpenBrackets()
        {
            Builder.UseDefaultCommander();
            Builder.ExecuteCommand(BuilderCommand.OpenBrackets);
        }

        private void CloseBrackets()
        {
            Builder.UseDefaultCommander();
            Builder.ExecuteCommand(BuilderCommand.CloseBrackets);
        }

        private void EmptySpace()        
            => Builder.UseDefaultCommander();        

        private void InsertInteger(char character)
            => _characters.Add(character);        

        private void InsertOperator(BuilderCommand command)
        {
            Builder.UseDefaultCommander();
            Builder.ExecuteCommand(command);
        }
    }
}
