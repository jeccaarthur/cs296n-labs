using System;
using Xunit;
using System.Collections.Generic;
using System.Threading.Tasks;
using Winterfell.Models;

namespace Tests
{
    public class UnitTest1 : FactAttribute
    {
        [Fact]
        public void TestCorrect()
        {
            // arrange
            var quiz = new QuizVM()
            {
                answer1 = "Direwolf",
                answer2 = "Winter is coming",
                answer3 = "false"
            };

            // act
            for (int i = 0; i < 5; i++)
            {
                quiz.CheckAnswers();
            }

            // assert
            Assert.True("Right" == quiz.result1);
            Assert.True("Right" == quiz.result2);
            Assert.True("Right" == quiz.result3);
        }

        [Fact]
        public void TestIncorrect()
        {
            // arrange
            var quiz = new QuizVM()
            {
                answer1 = "Dragon",
                answer2 = "To the North",
                answer3 = "true"
            };

            // act
            quiz.CheckAnswers();

            // assert
            Assert.True("Wrong" == quiz.result1);
            Assert.True("Wrong" == quiz.result2);
            Assert.True("Wrong" == quiz.result3);
        }
    }
}
