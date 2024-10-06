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
            string firstInput = "�����";
            string secondInput = "�����";
            var expectedOutput = "ͳ���!";

            string actualOutput = CalculateResult(firstInput, secondInput);

            Assert.Equal(expectedOutput, actualOutput);
        }

        [Theory]
        [InlineData("�����", "������", "�� ���������!")]
        [InlineData("������", "����", "�� ���������!")]
        [InlineData("����", "�����", "�� ���������!")]
        [InlineData("�����", "����", "�� ��������!")]
        [InlineData("������", "�����", "�� ��������!")]
        [InlineData("����", "������", "�� ��������!")]
        public void TestResultCalculation_MultipleCases(string userInput, string opponentInput, string expectedOutcome)
        {
            string actualOutcome = CalculateResult(userInput, opponentInput);

            Assert.Equal(expectedOutcome, actualOutcome);
        }

        private string CalculateResult(string userChoice, string opponentChoice)
        {
            if (userChoice == opponentChoice)
            {
                return "ͳ���!";
            }
            else if ((userChoice == "�����" && opponentChoice == "������") ||
                     (userChoice == "������" && opponentChoice == "����") ||
                     (userChoice == "����" && opponentChoice == "�����"))
            {
                return "�� ���������!";
            }
            else
            {
                return "�� ��������!";
            }
        }
    }
}
