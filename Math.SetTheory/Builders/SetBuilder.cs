using Math.Core.Abstractions;
using Math.Core.Builders;
using Math.Core.Enumerations;
using Math.SetTheory.Builders.Nodes;
using Math.SetTheory.Literals;
using System;
using System.Linq;

namespace Math.SetTheory.Builders
{
    internal class SetBuilder : Builder
    {
        public SetBuilder(string input)
        {
            for (var i = 0; i < input.Length; i++)
            {
                var character = input[i];

                if (char.IsUpper(character) && Nodes.Count == 0)
                    ExecuteCommand(BuilderCommand.DefineSet, character);

                else if (character == '=')
                    ExecuteCommand(BuilderCommand.Assignment);

                else if (character == ' ')
                    ExecuteCommand(BuilderCommand.EmptySpace);

                else if (character == '{')
                    ExecuteCommand(BuilderCommand.OpenBrackets);

                else if (character == '}')
                    ExecuteCommand(BuilderCommand.CloseBrackets);

                else if (character == ',')
                    ExecuteCommand(BuilderCommand.Comma);

                else if (char.IsDigit(character))
                    ExecuteCommand(BuilderCommand.InsertInteger, character);

                else if (char.IsLetter(character))
                    ExecuteCommand(BuilderCommand.InsertVariable, character);

                else
                    throw new InvalidOperationException("Invalid expression statement.");
            }
        }

        protected SetBuilder()
        {
        }

        public virtual Set ToSet()
        {
            if (Nodes.Count == 0)
                throw new InvalidOperationException("Builder is empty.");

            var node = Nodes.First();
            if(!(node is SetNode setNode))
                throw new InvalidOperationException("Invalid builder state.");

            return setNode.ToSet();
        }

        public override void DefaultCommander(BuilderCommand command, char? character)
        {
            switch (command)
            {
                case BuilderCommand.DefineSet:
                    DefineSet(character.Value);
                    break;
                case BuilderCommand.Assignment:
                    Assignment();
                    break;
                case BuilderCommand.OpenBrackets:
                    OpenBrackets();
                    break;
                case BuilderCommand.CloseBrackets:
                    CloseBrackets();
                    break;
                case BuilderCommand.Comma:
                    Comma();
                    break;
                case BuilderCommand.EmptySpace:
                    EmptySpace();
                    break;               
                case BuilderCommand.InsertInteger:
                    InsertInteger(character.Value);
                    break;
                case BuilderCommand.InsertVariable:
                    InsertVariable(character.Value);
                    break;
                default:
                    throw new NotImplementedException("Builder command not implemented.");
            }
        }

        protected virtual void DefineSet(char character)
        {
            if (Nodes.Count != 0)
                throw new InvalidOperationException("Invalid expression statement.");

            var newNode = new SetNode(this, character);
            Nodes.Add(newNode);

            if (newNode is IBuilder builderNode)
                Commander = builderNode.ExecuteCommand;
        }

        protected virtual void Assignment()
        {
            throw new InvalidOperationException("Invalid expression statement.");
        }

        protected virtual void OpenBrackets()
        {
            if (Nodes.Count != 0)
                throw new InvalidOperationException("Invalid expression statement.");

            var newNode = new SetNode(this, true);
            Nodes.Add(newNode);

            if (newNode is IBuilder builderNode)
                Commander = builderNode.ExecuteCommand;
        }

        protected virtual void CloseBrackets()
        {
            throw new InvalidOperationException("Invalid expression statement.");
        }

        protected virtual void Comma()
        {
            throw new InvalidOperationException("Invalid expression statement.");
        }

        protected virtual void EmptySpace()
        {
        }

        protected virtual void InsertInteger(char character)
        {
            throw new InvalidOperationException("Invalid expression statement.");
        }

        protected virtual void InsertVariable(char character)
        {
            throw new InvalidOperationException("Invalid expression statement.");
        }
    }
}
