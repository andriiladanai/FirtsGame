using System;
using Xunit;
using Moq;
using RockPaperScissorsServer;

namespace ServerSide.Tests
{
    public class ServerSideTestSuite
    {
        [Fact]
        public void TestResultCalculation_TieCase()
        {
            string firstInput = "камінь";
            string secondInput = "камінь";
            var expectedOutput = "Нічия!";

            string actualOutput = CalculateResult(firstInput, secondInput);

            Assert.Equal(expectedOutput, actualOutput);
        }

        [Theory]
        [InlineData("камінь", "ножиці", "Ви перемогли!")]
        [InlineData("ножиці", "папір", "Ви перемогли!")]
        [InlineData("папір", "камінь", "Ви перемогли!")]
        [InlineData("камінь", "папір", "Ви програли!")]
        [InlineData("ножиці", "камінь", "Ви програли!")]
        [InlineData("папір", "ножиці", "Ви програли!")]
        public void TestResultCalculation_MultipleCases(string userInput, string opponentInput, string expectedOutcome)
        {
            string actualOutcome = CalculateResult(userInput, opponentInput);

            Assert.Equal(expectedOutcome, actualOutcome);
        }

        private string CalculateResult(string userChoice, string opponentChoice)
        {
            if (userChoice == opponentChoice)
            {
                return "Нічия!";
            }
            else if ((userChoice == "камінь" && opponentChoice == "ножиці") ||
                     (userChoice == "ножиці" && opponentChoice == "папір") ||
                     (userChoice == "папір" && opponentChoice == "камінь"))
            {
                return "Ви перемогли!";
            }
            else
            {
                return "Ви програли!";
            }
        }
    }
}
