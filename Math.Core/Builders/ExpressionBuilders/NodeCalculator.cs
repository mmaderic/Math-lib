using Math.Core.Abstractions;
using Math.Core.Builders.Nodes;
using Math.Core.Enumerations;
using Math.Core.Extensions;
using Math.Core.Literals;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Math.Core.Builders.ExpressionBuilders
{
    internal class NodeCalculator
    {
        private readonly List<Number> _numbers;
        private readonly IEnumerable<Operator> _operators;

        public NodeCalculator(IEnumerable<INode> nodes)
        {
            if (nodes.Count() == 0 || (nodes.Count() == 1 && nodes.First() is OperatorNode))
                throw new InvalidOperationException("Invalid calculation. Expression is empty.");

            if (nodes.First() is OperatorNode operatorNode && operatorNode.Operator == Operator.Subtraction)
            {
                nodes = nodes.Skip(1);
                if (!(nodes.First() is INumberFactory firstNumberFactory))
                    throw new InvalidOperationException("Invalid calculation.");

                firstNumberFactory.Negate();
            }

            _numbers = new List<Number>();
            foreach (var node in nodes)
                if (node is INumberFactory numberFactory)
                    _numbers.Add(numberFactory.Construct());

            _operators = nodes.Where(x => x is OperatorNode)
               .Select(x => ((OperatorNode)x).Operator).ToArray();
        }
    

        public Number Calculate()
        {
            if (_numbers.Count() == 1 && _operators.Count() == 0)
                return _numbers.First();

            var calculations = new List<Calculation>();
            for (var i = 0; i < _operators.Count(); i++)
            {
                var @operator = _operators.ElementAt(i);
                var left = _numbers.ElementAt(i);
                var right = _numbers.ElementAt(i + 1);

                calculations.Add(new Calculation(left, @operator, right));
            }

            calculations.Sort();
            calculations.Link();

            var result = calculations.Calculate();

            return result;
        }
    }
}
