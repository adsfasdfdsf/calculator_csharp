using NUnit.Framework;
using System.Collections.Generic;

namespace CalculatorApp.Tests
{
    public class CalculatorTests
    {
        // --------------------------
        // ТЕСТЫ Tokenize
        // --------------------------

        [Test]
        public void TestTokenizeBasic()
        {
            var tokens = Calculator.Tokenize("2 + 3");
            CollectionAssert.AreEqual(new List<string> { "2", "+", "3" }, tokens);
        }

        [Test]
        public void TestTokenizeWithParentheses()
        {
            var tokens = Calculator.Tokenize("( 10 - 2 ) * ( 3 + 1 )");
            CollectionAssert.AreEqual(new List<string> { "(", "10", "-", "2", ")", "*", "(", "3", "+", "1", ")" }, tokens);
        }

        [Test]
        public void TestTokenizeMultipleSpaces()
        {
            var tokens = Calculator.Tokenize("  4   *   (  2 + 1 )  ");
            CollectionAssert.AreEqual(new List<string> { "4", "*", "(", "2", "+", "1", ")" }, tokens);
        }

        // --------------------------
        // ТЕСТЫ EvalTokens
        // --------------------------

        [Test]
        public void TestEvalPostfixAddition()
        {
            Assert.AreEqual(5, Calculator.EvalTokens(new List<string> { "2", "3", "+" }));
        }
        
        [Test]
        public void TestEvalPostfixDivision()
        {
            Assert.AreEqual(4, Calculator.EvalTokens(new List<string> { "8", "2", "/" }));
            Assert.AreEqual(3.5, Calculator.EvalTokens(new List<string> { "7", "2", "/" }));
        }
        

        [Test]
        public void TestEvalPostfixErrorNotEnoughOperands()
        {
            Assert.Throws<System.Exception>(() =>
                Calculator.EvalTokens(new List<string> { "2", "+" })
            );
        }

        [Test]
        public void TestEvalPostfixErrorUnknownToken()
        {
            Assert.Throws<System.Exception>(() =>
                Calculator.EvalTokens(new List<string> { "2", "X", "+" })
            );
        }

        // --------------------------
        // ТЕСТЫ Calculate (интеграционные)
        // --------------------------

        [Test]
        public void TestCalculateSimple()
        {
            Assert.AreEqual(5, Calculator.Calculate("2 + 3"));
        }

        [Test]
        public void TestCalculateWithParentheses()
        {
            Assert.AreEqual(-18, Calculator.Calculate("2 + 5 * ( 3 - 7 )"));
        }

        [Test]
        public void TestCalculateMultiple()
        {
            Assert.AreEqual(32, Calculator.Calculate("( 10 - 2 ) * ( 3 + 1 )"));
        }

        [Test]
        public void TestCalculateDivision()
        {
            Assert.AreEqual(4, Calculator.Calculate("8 / 2"));
            Assert.AreEqual(2, Calculator.Calculate("7 - 10 / 2"));
            Assert.AreEqual(2, Calculator.Calculate("10 / 2 - 3"));
        }

        [Test]
        public void TestCalculateNestedParentheses()
        {
            Assert.AreEqual(38, Calculator.Calculate("2 * ( 3 + ( 4 * ( 5 - 1 ) ) )"));
        }

        [Test]
        public void TestCalculateNegativeResult()
        {
            Assert.AreEqual(-7, Calculator.Calculate("3 - 10"));
        }

        [Test]
        public void TestCalculateMultipleOperations()
        {
            Assert.AreEqual(14, Calculator.Calculate("10 - 2 + 3 * 2"));
        }

        [Test]
        public void TestCalculateLargeExpression()
        {
            var expr = "1 + 2 + 3 + 4 * 5 + ( 6 - 2 ) * 3";
            Assert.AreEqual(38, Calculator.Calculate(expr));
        }

        [Test]
        public void TestCalculateLargeExpression2()
        {
            var expr = "1 + 2 * 3 + 4 * 5 + ( 6 - 2 ) * 3";
            Assert.AreEqual(39, Calculator.Calculate(expr));
        }

        // --------------------------
        // ТЕСТЫ на исключения
        // --------------------------

        [Test]
        public void TestErrorUnbalancedParentheses()
        {
            Assert.Throws<System.Exception>(() =>
                Calculator.Calculate("( 2 + 3")
            );
        }

        [Test]
        public void TestErrorUnknownToken()
        {
            Assert.Throws<System.Exception>(() =>
                Calculator.Tokenize("2 ? 3")
            );
        }

        [Test]
        public void TestErrorNotEnoughOperands()
        {
            Assert.Throws<System.Exception>(() =>
                Calculator.EvalTokens(new List<string> { "2", "+" })
            );
        }
    }
}
