using Math.Core.Enumerations;
using Math.Core.Extensions;
using Math.Core.Literals;
using System;

namespace Math.Core.Builders
{
    internal class Calculator
    {
        public Number A { get; }
        public Number B { get; }
        public Number C { get; }

        public Operator OA { get; }
        public Operator OB { get; }

        public Calculator(Number a, Operator oa, Number b, Operator ob, Number c)
        {
            A = a;
            OA = oa;
            B = b;
            OB = ob;
            C = c;
        }

        public Calculation SubCalculation()
        {
            if (OA.IsHigherPrecedence(OB) || OA.IsComplementary(OB))
            {
                var AB = new Calculation(A, OA, B).Calculate();
                return new Calculation(AB, OB, C);
            }

            if (OB.IsHigherPrecedence(OA))
            {
                var BC = new Calculation(B, OB, C).Calculate();
                return new Calculation(A, OA, BC);
            }

            throw new InvalidOperationException("Invalid builder state.");
        }
    }
}
