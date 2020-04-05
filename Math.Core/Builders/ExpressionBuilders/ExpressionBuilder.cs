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
                    ExecuteCommand(BuilderCommand.OpenBrackets);

                else if (character == ')')
                    ExecuteCommand(BuilderCommand.CloseBrackets);

                else if (character == ' ')
                    ExecuteCommand(BuilderCommand.EmptySpace);

                else if (character == '+')
                    ExecuteCommand(BuilderCommand.Add);

                else if (character == '-')
                    ExecuteCommand(BuilderCommand.Subtract);

                else if (character == 'x')
                    ExecuteCommand(BuilderCommand.Multiply);

                else if (character == '/')
                    ExecuteCommand(BuilderCommand.Divide);

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
