using Math.Core.Builders.ExpressionBuilders;
using Math.Core.Literals;
using System.Collections.Generic;
using System.Linq;

namespace Math.Core.Extensions
{
    internal static class CalculationExtensions
    {
        public static void Link(this IEnumerable<Calculation> calculations)
        {
            if (calculations.Count() < 2)
                return;

            Calculation switcher;
            var linked = new List<Calculation>();            

            for (var i = calculations.Count() - 1; i >= 0; i--)
            {
                var calculation = calculations.ElementAt(i);

                var referencedLeft = calculations.SingleOrDefault(x => !linked.Contains(x) &&
                    !ReferenceEquals(x, calculation) &&
                    ReferenceEquals(calculation.LeftNumber, x.RightNumber));

                var referencedRight = calculations.SingleOrDefault(x => !linked.Contains(x) &&
                    !ReferenceEquals(x, calculation) 
                    && ReferenceEquals(calculation.RightNumber, x.LeftNumber));

                if (!(referencedLeft is null))
                {                    
                    calculation.Left = referencedLeft.Calculate;
                    calculation.LeftReference = referencedLeft;                    
                }

                if (!(referencedRight is null))
                {
                    calculation.Right = referencedRight.Calculate;
                    calculation.RightReference = referencedRight;
                    switcher = calculation;
                }

                linked.Add(calculation);
            }

            void SwitchReferences(Calculation switcher)
            {
                switcher = switcher.RightReference;                
                var right = calculations.SingleOrDefault(
                    x => ReferenceEquals(x.LeftReference, switcher));

                if (right is null)
                    return;

                right.LeftReference = null;
                right.Left = () => right.LeftNumber;

                switcher.RightReference = right;
                switcher.Right = right.Calculate;

                SwitchReferences(switcher);
            }

            var sideSwitcher = linked.SingleOrDefault(x => !(x.LeftReference is null) && !(x.RightReference is null));
            if (!(sideSwitcher is null))
                SwitchReferences(sideSwitcher);
        }

        public static Number Calculate(this IEnumerable<Calculation> calculations)        
            => calculations.Last().Calculate();
    }
}
