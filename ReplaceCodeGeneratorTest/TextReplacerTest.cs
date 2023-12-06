using Xunit;
using ReplaceCodeGenerator.Replacer;
using System.Xml.Linq;

namespace ReplaceCodeGenerator.Tests
{
    public class TextReplacerTest
    {
        private readonly ITextReplacer textReplacer = new TextReplacer();

        [Theory]
        [InlineData("Hi my name is <author>.", "Hi my name is Aardvark.")]
        //[InlineData("Today is .", "Hi my name is Aardvark.")]
        [InlineData("I must say you: [message].", "I must say you: Hello from Aardvark.")]
        [InlineData("Test[0]ForReplace", "TestArgumentForReplace")]
        [InlineData("Test[0L]ForReplace", "TestargumentForReplace")]
        [InlineData("Test[0U]ForReplace", "TestARGUMENTForReplace")]
        [InlineData("Test[X_NOT_WORKING_X]ForReplace", "Test[X_NOT_WORKING_X]ForReplace")]
        public void Replace(string input, string expected)
        {
            // arrange

            // act
            var actual = textReplacer.Replace(
                input, 
                new ReplaceCodeGenerator.Commands.Replacement() 
                { 
                    Author = "Aardvark",
                    Message = "Hello from Aardvark",
                    Replacements = new[] { "Argument" }
                });

            // assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("Test[1]ForReplace", "TestYyForReplace")]
        [InlineData("Test[0L]For[1U]Replace", "TestxxForYYReplace")]
        [InlineData("Test[1L]For[0U]Replace[2]", "TestyyForXXReplaceZz")]
        public void MultiArgumentReplace(string input, string expected)
        {
            // arrange

            // act
            var actual = textReplacer.Replace(
                input,
                new ReplaceCodeGenerator.Commands.Replacement()
                {
                    Replacements = new[] { "Xx", "Yy", "Zz" }
                });

            // assert
            Assert.Equal(expected, actual);
        }
    }
}
