using Math.Core.Abstractions;
using Math.Core.Enumerations;
using Math.SetTheory.Abstractions;
using Math.SetTheory.Literals;
using System;
using System.Linq;
using System.Text;

namespace Math.SetTheory.Builders.Nodes
{
    internal class SetNode : SetBuilder, INode
    {
        public IBuilder Builder { get; }

        private readonly char? _identifier;
        private bool _assignmentIsSpecified;
        private bool _bracketsOpened;
        private bool _bracketsClosed;

        public SetNode(IBuilder builder, bool bracketsOpened = false)
        {
            Builder = builder;
            _bracketsOpened = bracketsOpened;
        }        

        public SetNode(IBuilder builder, char identifier) : this(builder)        
            => _identifier = identifier;        

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append($"{_identifier} = {{");
            builder.Append(string.Join(", ", Nodes.Select(x => x.ToString())));
            builder.Append('}');

            return builder.ToString();
        }

        public override Set ToSet()
        {
            if (!(_bracketsOpened && _bracketsClosed))
                throw new InvalidOperationException("Invalid set expression.");

            var elementFactories = Nodes.Where(x => x is IElementFactory);
            var elements = elementFactories.Select(x => ((IElementFactory)x).Construct());

            return new Set(_identifier, elements);
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

        protected override void Assignment()
        {
            if (_assignmentIsSpecified)
                throw new InvalidOperationException("Invalid expression statement.");

            _assignmentIsSpecified = true;
        }

        protected override void OpenBrackets()
        {
            if (_bracketsOpened)
                throw new InvalidOperationException("Invalid expression statement.");

            _bracketsOpened = true;
        }

        protected override void CloseBrackets()
        {
            if (_bracketsClosed)
                throw new InvalidOperationException("Invalid builder state.");

            _bracketsClosed = true;
            Builder.UseDefaultCommander();
        }

        protected override void Comma()
        {
            if (Nodes.Count == 0)
                throw new InvalidOperationException("Invalid expression statement.");
            else throw new InvalidOperationException("Invalid builder state.");
        }

        protected override void InsertInteger(char character)
        {
            var newNode = new SetIntegerNode(this, character);
            Nodes.Add(newNode);

            if (newNode is IBuilder builderNode)
                Commander = builderNode.ExecuteCommand;
        }
    }
}
