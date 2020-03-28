using Math.Core.Enumerations;
using Math.Core.Literals;
using System;
using Xunit;

namespace Math.Core.Tests.LiteralsTests
{
    public class ExpressionTests
    {
        [Fact]
        public void IntegerAddsIntegerExpression()
        {
            Number e = new Expression(3, Operator.Addition, 7);

            Assert.Equal("10", e.ToString());
        }

        [Fact]
        public void IntegerAddsFractionExpression()
        {
            Number e = new Expression(3, Operator.Addition, "2/3");

            Assert.Equal("11/3", e.ToString());
        }

        [Fact]
        public void IntegerAddsVariableExpression()
        {
            Number e = new Expression(3, Operator.Addition, 'a');

            Assert.Equal("(3 + a)", e.ToString());
        }

        [Fact]
        public void FractionAddsFractionExpression()
        {
            Number e = new Expression("3/2", Operator.Addition, "7/3");

            Assert.Equal("23/6", e.ToString());
        }

        [Fact]
        public void FractionAddsIntegerExpression()
        {
            Number e = new Expression("3/2", Operator.Addition, 11);

            Assert.Equal("25/2", e.ToString());
        }

        [Fact]
        public void FractionAddsVariableExpression()
        {
            Number e = new Expression("5/7", Operator.Addition, 'a');

            Assert.Equal("(5/7 + a)", e.ToString());
        }

        [Fact]
        public void VariableAddsVariableExpression()
        {
            Number e = new Expression('c', Operator.Addition, 'a');

            Assert.Equal("(a + c)", e.ToString());
        }

        [Fact]
        public void VariableAddsIntegerExpression()
        {
            Number e = new Expression('a', Operator.Addition, 11);

            Assert.Equal("(11 + a)", e.ToString());
        }

        [Fact]
        public void VariableAddsFractionExpression()
        {
            Number e = new Expression('a', Operator.Addition, "8/11");

            Assert.Equal("(8/11 + a)", e.ToString());
        }

        [Fact]
        public void ExpressionAddsInteger()
        {
            Number e1 = new Expression('a', Operator.Addition, 21);
            Number e2 = e1 + 9;

            Assert.Equal("(30 + a)", e2.ToString());
        }

        [Fact]
        public void ExpressionAddsFraction()
        {
            Number e1 = new Expression('a', Operator.Addition, "7/13");
            Number e2 = e1 + "8/17";

            Assert.Equal("(223/221 + a)", e2.ToString());
        }

        [Fact]
        public void ExpressionAddsVariable()
        {
            Number e1 = new Expression('a', Operator.Addition, 'b');
            Number e2 = e1 + 'c';

            Assert.Equal("((a + b) + c)", e2.ToString());
        }

        [Fact]
        public void ExpressionIntegerAdditionSequence()
        {
            Number e1 = (Number)"356" + "128" + "544" + "122" + "150";
            Number e2 = (Number)"37" + "186" + "310" + "214" + "190" + "563";
            Number e3 = (Number)"129" + "352" + "471" + "253" + "548" + "21" + "747";
            Number e4 = (Number)"379" + "3464" + "343" + "121" + "536" + "1657";

            Assert.Equal("1300", e1.ToString());
            Assert.Equal("1500", e2.ToString());
            Assert.Equal("2521", e3.ToString());
            Assert.Equal("6500", e4.ToString());
        }

        [Fact]
        public void IntegerSubtractsIntegerExpression()
        {
            Number e = new Expression(3, Operator.Subtraction, 7);

            Assert.Equal("-4", e.ToString());
        }

        [Fact]
        public void IntegerSubtractsFractionExpression()
        {
            Number e = new Expression(3, Operator.Subtraction, "2/3");

            Assert.Equal("7/3", e.ToString());
        }

        [Fact]
        public void IntegerSubtractsVariableExpression()
        {
            Number e = new Expression(3, Operator.Subtraction, 'a');

            Assert.Equal("(3 - a)", e.ToString());
        }

        [Fact]
        public void FractionSubtractsFractionExpression()
        {
            Number e = new Expression("3/2", Operator.Subtraction, "7/3");

            Assert.Equal("-5/6", e.ToString());
        }

        [Fact]
        public void FractionSubtractsIntegerExpression()
        {
            Number e = new Expression("3/2", Operator.Subtraction, 11);

            Assert.Equal("-19/2", e.ToString());
        }

        [Fact]
        public void FractionSubtractsVariableExpression()
        {
            Number e = new Expression("5/7", Operator.Subtraction, 'a');

            Assert.Equal("(5/7 - a)", e.ToString());
        }

        [Fact]
        public void VariableSubtractsVariableExpression()
        {
            Number e = new Expression('c', Operator.Subtraction, 'a');

            Assert.Equal("(a - c)", e.ToString());
        }

        [Fact]
        public void VariableSubtractsIntegerExpression()
        {
            Number e = new Expression('a', Operator.Subtraction, 11);

            Assert.Equal("(11 - a)", e.ToString());
        }

        [Fact]
        public void VariableSubtractsFractionExpression()
        {
            Number e = new Expression('a', Operator.Subtraction, "8/11");

            Assert.Equal("(8/11 - a)", e.ToString());
        }

        [Fact]
        public void ExpressionSubtractsInteger()
        {
            Number e1 = new Expression('a', Operator.Addition, 21);
            Number e2 = e1 - 9;

            Assert.Equal("(12 + a)", e2.ToString());
        }

        [Fact]
        public void ExpressionSubtractsFraction()
        {
            Number e1 = new Expression('a', Operator.Addition, "7/13");
            Number e2 = e1 - "8/17";

            Assert.Equal("(15/221 + a)", e2.ToString());
        }

        [Fact]
        public void ExpressionSubtractsVariable()
        {
            Number e1 = new Expression('a', Operator.Addition, 'b');
            Number e2 = e1 - 'c';

            Assert.Equal("((a + b) - c)", e2.ToString());
        }

        [Fact]
        public void ExpressionIntegerSubtractionSequence()
        {
            Number e1 = (Number)"356" - "128" - "544" - "122" - "150";
            Number e2 = (Number)"37" - "186" - "310" - "214" - "190" - "563";
            Number e3 = (Number)"129" - "352" - "471" - "253" - "548" - "21" - "747";
            Number e4 = (Number)"379" - "3464" - "343" - "121" - "536" - "1657";

            Assert.Equal("-588", e1.ToString());
            Assert.Equal("-1426", e2.ToString());
            Assert.Equal("-2263", e3.ToString());
            Assert.Equal("-5742", e4.ToString());
        }

        [Fact]
        public void IntegerMultipliesIntegerExpression()
        {
            Number e = new Expression(3, Operator.Multiplication, 7);

            Assert.Equal("21", e.ToString());
        }

        [Fact]
        public void IntegerMultipliesFractionExpression()
        {
            Number e = new Expression(3, Operator.Multiplication, "2/3");

            Assert.Equal("2/1", e.ToString());
        }

        [Fact]
        public void IntegerMultipliesVariableExpression()
        {
            Number e = new Expression(3, Operator.Multiplication, 'a');

            Assert.Equal("3a", e.ToString());
        }

        [Fact]
        public void FractionMultipliesFractionExpression()
        {
            Number e = new Expression("3/2", Operator.Multiplication, "7/3");

            Assert.Equal("7/2", e.ToString());
        }

        [Fact]
        public void FractionMultipliesIntegerExpression()
        {
            Number e = new Expression("3/2", Operator.Multiplication, 11);

            Assert.Equal("33/2", e.ToString());
        }

        [Fact]
        public void FractionMultipliesVariableExpression()
        {
            Number e = new Expression("5/7", Operator.Multiplication, 'a');

            Assert.Equal("(5/7 x a)", e.ToString());
        }

        [Fact]
        public void VariableMultipliesVariableExpression()
        {
            Number e = new Expression('c', Operator.Multiplication, 'a');

            Assert.Equal("ac", e.ToString());
        }

        [Fact]
        public void VariableMultipliesIntegerExpression()
        {
            Number e = new Expression('a', Operator.Multiplication, 11);

            Assert.Equal("11a", e.ToString());
        }

        [Fact]
        public void VariableMultipliesFractionExpression()
        {
            Number e = new Expression('a', Operator.Multiplication, "8/11");

            Assert.Equal("(8/11 x a)", e.ToString());
        }

        [Fact]
        public void ExpressionMultipliesInteger()
        {
            Number e1 = new Expression('a', Operator.Addition, 21);
            Number e2 = e1 * 9;

            Assert.Equal("((21 + a) x 9)", e2.ToString());
        }

        [Fact]
        public void ExpressionMultipliesFraction()
        {
            Number e1 = new Expression('a', Operator.Addition, "7/13");
            Number e2 = e1 * "8/17";

            Assert.Equal("((7/13 + a) x 8/17)", e2.ToString());
        }

        [Fact]
        public void ExpressionMultipliesVariable()
        {
            Number e1 = new Expression('a', Operator.Addition, 'b');
            Number e2 = e1 * 'c';

            Assert.Equal("((a + b) x c)", e2.ToString());
        }

        [Fact]
        public void ExpressionIntegerMultiplicationSequence()
        {
            Number e1 = (Number)"356" * "128" * "544" * "122" * "150";
            Number e2 = (Number)"37" * "186" * "310" * "214" * "190" * "563";
            Number e3 = (Number)"129" * "352" * "471" * "253" * "548" * "21";
            Number e4 = (Number)"379" * "3464" * "343" * "121" * "536" * "1657";            

            var res1 = e1.ToString().Equals("453638553600", StringComparison.CurrentCulture);
            var res2 = e2.ToString().Equals("48837354603600‬", StringComparison.CurrentCulture);
            var res3 = e3.ToString().Equals("62269252924032‬", StringComparison.CurrentCulture);
            var res4 = e4.ToString().Equals("48393148854694336‬", StringComparison.CurrentCulture);

            Assert.True(res1);
            Assert.True(res2);
            Assert.True(res3);
            Assert.True(res4);
        }

        [Fact]
        public void IntegerDividesIntegerExpression()
        {
            Number e = new Expression(3, Operator.Division, 7);

            Assert.Equal("3/7", e.ToString());
        }

        [Fact]
        public void IntegerDividesFractionExpression()
        {
            Number e = new Expression(3, Operator.Division, "2/3");

            Assert.Equal("9/2", e.ToString());
        }

        [Fact]
        public void IntegerDividesVariableExpression()
        {
            Number e = new Expression(3, Operator.Division, 'a');

            Assert.Equal("3/a", e.ToString());
        }

        [Fact]
        public void FractionDividesFractionExpression()
        {
            Number e = new Expression("3/2", Operator.Division, "7/3");

            Assert.Equal("9/14", e.ToString());
        }

        [Fact]
        public void FractionDividesIntegerExpression()
        {
            Number e = new Expression("3/2", Operator.Division, 11);

            Assert.Equal("3/22", e.ToString());
        }

        [Fact]
        public void FractionDividesVariableExpression()
        {
            Number e = new Expression("5/7", Operator.Division, 'a');

            Assert.Equal("(5/7)/a", e.ToString());
        }

        [Fact]
        public void VariableDividesVariableExpression()
        {
            Number e = new Expression('c', Operator.Division, 'a');

            Assert.Equal("c/a", e.ToString());
        }

        [Fact]
        public void VariableDividesIntegerExpression()
        {
            Number e = new Expression('a', Operator.Division, 11);

            Assert.Equal("a/11", e.ToString());
        }

        [Fact]
        public void VariableDividesFractionExpression()
        {
            Number e = new Expression('a', Operator.Division, "8/11");

            Assert.Equal("a/(8/11)", e.ToString());
        }

        [Fact]
        public void ExpressionDividesInteger()
        {
            Number e1 = new Expression('a', Operator.Addition, 21);
            Number e2 = e1 / 9;

            Assert.Equal("(21 + a)/9", e2.ToString());
        }

        [Fact]
        public void ExpressionDividesFraction()
        {
            Number e1 = new Expression('a', Operator.Addition, "7/13");
            Number e2 = e1 / "8/17";

            Assert.Equal("(7/13 + a)/(8/17)", e2.ToString());
        }

        [Fact]
        public void ExpressionDividesVariable()
        {
            Number e1 = new Expression('a', Operator.Addition, 'b');
            Number e2 = e1 / 'c';

            Assert.Equal("(a + b)/c", e2.ToString());
        }

        [Fact]
        public void ExpressionIntegerDivisionSequence()
        {
            Number e1 = (Number)"356" / "128" / "544" / "122" / "150";
            Number e2 = (Number)"37" / "186" / "310" / "214" / "190" / "563";
            Number e3 = (Number)"129" / "352" / "471" / "253" / "548" / "21";
            Number e4 = (Number)"379" / "3464" / "343" / "121" / "536" / "1657";

            Assert.Equal("89/318566400", e1.ToString());
            Assert.Equal("37/1319928502800", e2.ToString());
            Assert.Equal("43/160902462336", e3.ToString());
            Assert.Equal("379/127686408587584", e4.ToString());
        }
    }
}
