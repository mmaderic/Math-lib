using Math.Core.Builders;
using Math.Core.Literals;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Math.Core.Extensions
{
    internal static class CalculationExtensions
    {   
        public static Number Calculate(this IEnumerable<Calculation> calculations)
        {
            var count = calculations.Count();
            if (count == 0)
                throw new InvalidOperationException("Empty calculations collection.");

            else if (count == 1 || (count == 2 && calculations.ElementAt(1).Right is null))
                return calculations.First().Calculate();

            var subCalculations = calculations.ToList();
            for (var i = 0; i < subCalculations.Count - 1; i++)
            {
                var calculation = subCalculations[i];
                var nextCalculation = subCalculations[i + 1];

                var a = calculation.Left;
                var b = calculation.Right;
                var c = nextCalculation.Right;

                var oa = calculation.Operator;
                var ob = nextCalculation.Operator;

                var subCalculation = new Calculator(a, oa, b, ob, c).SubCalculation();

                subCalculations[i--] = subCalculation;
                subCalculations.Remove(nextCalculation);
            }

            return subCalculations.First().Calculate();
        } 
    }
}
