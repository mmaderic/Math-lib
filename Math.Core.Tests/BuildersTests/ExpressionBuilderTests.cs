﻿using Math.Core.Literals;
using Xunit;

namespace Math.Core.Tests.BuildersTests
{
    public class ExpressionBuilderTests
    {
        [Fact]
        public void ImplicitStringToInteger()
        {
            Number integerA = "2357";
            Number integerB = "(2357)";

            Assert.True(integerA is Integer && integerA.ToString() == "2357");
            Assert.True(integerB is Integer && integerB.ToString() == "2357");
        }

        [Fact]
        public void ImplicitStringToFraction()
        {
            Number fractionA = "257/432";
            Number fractionB = "(257/432)";

            Assert.True(fractionA is Fraction && fractionA.ToString() == "257/432");
            Assert.True(fractionB is Fraction && fractionB.ToString() == "257/432");
        }

        [Fact]
        public void ImplicitStringToVariable()
        {
            Number variableA = "a";
            Number variableB = "(a)";

            Assert.True(variableA is Variable && variableA.ToString() == "a");
            Assert.True(variableB is Variable && variableB.ToString() == "a");
        }

        [Fact]
        public void ImplicitStringToExpressionResult_A()
        {
            Number expression = "(13 + 5) x 8";

            Assert.Equal("144", expression.ToString());
        }

        [Fact]
        public void ImplicitStringToExpressionResult_B()
        {
            Number expression = "ab";

            Assert.True(expression is Expression && expression.ToString() == "ab");
        }

        [Fact]
        public void ImplicitStringToExpressionResult_C()
        {
            Number expression = "(13 + 5 x 8)(17 + 4 / 2)";

            Assert.Equal("1007", expression.ToString());
        }


        [Fact]
        public void ImplicitStringToExpressionResult_D()
        {
            Number expressionA = "(237 - 18) x 4"; 
            Number expressionB = "(2 x 17 + 5) x (24 x 3 - 70)";
            Number expressionC = "(3 x 81 - 23) x 4 - 2 x (34 x 5 - 8 x 15)";
            Number expressionD = "2(7 + 4 x (13 x 6 - 19 x 2))";
            Number expressionE = "7((42 x 7 - 14) x 2 - 5 x (105 - 3 x 25))";
            Number expressionF = "(41 - 3 x 7) x (6(184 - 32 x 2) - 2(501 - 17 x 23))";
            Number expressionG = "5(5(5(5 x 19 - 2 x 47) - 4(8 x 32 - 3 x 17 x 5)))";
            Number expressionH = "3 + 2(3(8(19 x 21 - 36 x 11) - 23))";
            Number expressionI = "14 + 9(23(15(14 x 19 - 16 x 13) + 24) - 129 x 118)";

            Assert.Equal("876", expressionA.ToString());
            Assert.Equal("78", expressionB.ToString());
            Assert.Equal("780", expressionC.ToString());
            Assert.Equal("334", expressionD.ToString());
            Assert.Equal("2870", expressionE.ToString());
            Assert.Equal("10000", expressionF.ToString());
            Assert.Equal("25", expressionG.ToString());
            Assert.Equal("9", expressionH.ToString());
            Assert.Equal("48074", expressionI.ToString());
        }

        [Fact]
        public void ImplicitStringToExpressionResult_E()
        {
            Number expressionA = "-15 + 27 - 3 - 11 + 2";
            Number expressionB = "7 - 9 + 6 - 3 - 1";
            Number expressionC = "(-5) + (+4) + (-4) + (+4) + (-8)";
            Number expressionD = "(-15) + (+16) + (-9) + (+5) + (+7) + (-10)";
            Number expressionE = "(-12) + (+15) - (-4) - (-8) + (+11)";
            Number expressionF = "(-21) + (-11) - (+6) - (-8) - (-4)";

            Assert.Equal("0", expressionA.ToString());
            Assert.Equal("0", expressionB.ToString());
            Assert.Equal("-9", expressionC.ToString());
            Assert.Equal("-6", expressionD.ToString());
            Assert.Equal("26", expressionE.ToString());
            Assert.Equal("-26", expressionF.ToString());
        }

        [Fact]
        public void ImplicitStringToExpressionResult_F()
        {
            Number expressionA = "3 x (-8) + 2 x (-1) + 5 x (-4)";
            Number expressionB = "(-3) x (-8) + 2 x (-10) - 5 x (-2)";
            Number expressionC = "7 x (-2) -5 x (-3) - (-1)";
            Number expressionD = "(-2)x(-5)x(-3)-(+4)x(-2)x(+5)+(+6)x(-10)";
            Number expressionE = "((+3) x (-6) - (-2)) x (24(-1) - (-1) x (-4))";
            Number expressionF = "((8 - 5) x (2 - 7) - (6 - 7) x (4 - 3)) x (5 - 9)";
            Number expressionG = "-2-(-5-(8-(5-3)))";
            Number expressionH = "4-(4-(-10 -(8-11)-(1-12)))";
            Number expressionI = "-2-2(-2-2(-2x(-2)-2))";


            Assert.Equal("-46", expressionA.ToString());
            Assert.Equal("14", expressionB.ToString());
            Assert.Equal("2", expressionC.ToString());
            Assert.Equal("-50", expressionD.ToString());
            Assert.Equal("448", expressionE.ToString());
            Assert.Equal("56", expressionF.ToString());
            Assert.Equal("9", expressionG.ToString());
            Assert.Equal("4", expressionH.ToString());
            Assert.Equal("10", expressionI.ToString());
        }
    }
}
