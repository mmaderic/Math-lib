using Math.Core.Enumerations;
using Math.Core.Literals;
using System;

namespace Math.Core.Builders.ExpressionBuilders
{
    internal class ExpressionBuilder : Builder
    {
        public ExpressionBuilder(string input)
        {      
            for (var i = 0; i < input.Length; i++)
            {
                var character = input[i];

                if (character == '(')
                    ExecuteCommand(BuilderCommand.OpenBrackets, null);

                else if (character == ')')
                    ExecuteCommand(BuilderCommand.CloseBrackets, null);

                else if (character == ' ')
                    ExecuteCommand(BuilderCommand.EmptySpace, null);

                else if (character == '+')
                    ExecuteCommand(BuilderCommand.Add, null);

                else if (character == '-')
                    ExecuteCommand(BuilderCommand.Subtract, null);

                else if (character == 'x')
                    ExecuteCommand(BuilderCommand.Multiply, null);

                else if (character == '/')
                    ExecuteCommand(BuilderCommand.Divide, null);

                else if (char.IsNumber(character))
                    ExecuteCommand(BuilderCommand.InsertInteger, character);

                else if (char.IsLower(character))
                    ExecuteCommand(BuilderCommand.InsertVariable, character);

                else
                    throw new InvalidOperationException("Invalid expression statement.");
            }
        } 

        public Number SolveExpression()
        {
            var calculator = new NodeCalculator(Nodes);

            return calculator.Calculate();
        }        
    }
}
