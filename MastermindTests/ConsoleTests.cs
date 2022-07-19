using System;
using System.IO;
using Mastermind.Presentation.InputOutput;
using Xunit;

namespace MastermindTests
{
    public class ConsoleTests
    {
        [Fact]
        public void
            GivenAConsoleInputOutput_WhenDisplayOutputIsCalled_ThenTheOutputShouldBePrintedCorrectlyToTheConsole()
        {
            // Arrange
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            const string expectedOutput = "Hello World!";
            var console = new ConsoleInputOutput();
            
            // Act
            console.DisplayOutput(expectedOutput);
            
            // Assert
            Assert.Contains(expectedOutput, stringWriter.ToString());
        }
    }
}