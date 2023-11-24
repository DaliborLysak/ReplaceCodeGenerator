using ReplaceCodeGenerator.Argument;
using ReplaceCodeGenerator.Commands;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using Xunit;

namespace ReplaceCodeGenerator.Tests
{
    public class ArgumentsProcesingTest
    {
        [Fact]
        public void ParseGenerateDefinitionCommand()
        {
            // arrange
            var argumentParser = new ArgumentParser();

            // act
            var command = argumentParser.ParseCommand(new[] { "generate-definition" });

            // assert
            Assert.IsType<GenerateDefinition>(command);
        }

        [Fact]
        public void ParseLoadDefinitionCommand()
        {
            // arrange
            var argumentParser = new ArgumentParser();

            // act
            var command = argumentParser.ParseCommand(new[] { "load-definition" });

            // assert
            Assert.IsType<LoadDefinition>(command);
        }

        [Theory]
        [InlineData("generate-definition", "load-definition")]
        [InlineData("")]
        [InlineData("BLA", "BLA", "BLA")]
        [InlineData("BLA", "BLA", "BLA", "BLA")]
        public void ParseCommandFail(params string[] argument)
        {
            // arrange
            var argumentParser = new ArgumentParser();

            // act
            var command = argumentParser.ParseCommand(argument);

            // assert
            Assert.Null(command);
        }

        [Theory, MemberData(nameof(Replacements))]
        public void ParseReplacements(string[] input, string[] replacementsExpected, string messageExpected)
        {
            // arrange
            var argumentParser = new ArgumentParser();

            // act
            (var replacementsActual, var messageActual) = argumentParser.ParseReplacements(input);

            // assert
            Assert.Equal(messageExpected, messageActual);
            Assert.Equal(replacementsExpected, replacementsActual);
        }

        public static IEnumerable<object[]> Replacements =>
            new List<object[]>
            {
                new object[] { new string[] { "-message", "Hello how are you." }, Array.Empty<string>(), "Hello how are you."},
                new object[] { new string[] { "-replacements", "replacement" }, new string[] { "replacement" }, string.Empty },
                new object[] { 
                    new string[] { "-replacements", "replacement0 replacement1 replacement2" }, 
                    new string[] { "replacement0", "replacement1", "replacement2" },
                    string.Empty },
                new object[] { 
                    new string[] { "-message", "Hello how are you.", "-replacements", "replacement" },
                    new string[] { "replacement" },
                    "Hello how are you." },
                new object[] {
                    new string[] { "-message", "Hello how are you.", "-nonsense", "-replacements", "replacement" },
                    new string[] { "replacement" },
                    "Hello how are you." },
                new object[] {
                    new string[] { "-replacements", "replacement", "-nonsense", "-message", "Hello how are you." },
                    new string[] { "replacement" },
                    "Hello how are you." },
            };
    }
}