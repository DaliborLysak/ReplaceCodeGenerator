using ReplaceCodeGenerator.Argument;
using ReplaceCodeGenerator.Commands;
using ReplaceCodeGenerator.Definitions;
using ReplaceCodeGenerator.Replacer;
using System.Runtime.CompilerServices;

namespace ReplaceCodeGenerator
{
    public class Generator
    {
        private readonly ITextReplacer textReplacer;
        private readonly IDefinitionLoader definitionLoader;
        private readonly IArgumentParser argumentParser;

        public Generator(ITextReplacer textReplacer, IDefinitionLoader definitionLoader, IArgumentParser argumentParser)
        {
            this.textReplacer = textReplacer;
            this.definitionLoader = definitionLoader;
            this.argumentParser = argumentParser;
        }

        public string Generate(string[] arguments)
        {
            ICommand? command = null;
            var data = new Data();

            command = this.argumentParser.ParseCommand(arguments);

            if (command is null)
            {
                command = new GenerateCode(this.textReplacer);
                (data.Replacement.Replacements, data.Replacement.Message) = this.argumentParser.ParseReplacements(arguments);
            }

            data = command.Run(this.definitionLoader, data);
            Console.WriteLine($"Generating finished, press any key to close.");

            return data.Message;
        }
    }
}
