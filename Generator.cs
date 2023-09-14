using ReplaceCodeGenerator.Commands;
using ReplaceCodeGenerator.Definitions;
using System.Runtime.CompilerServices;

namespace ReplaceCodeGenerator
{
    public class Generator
    {
        private static IDefinitionLoader definitionLoader = new DefinitionLoader();

        public string Generate(string[] arguments)
        {
            ICommand? command = null;
            var data = new Data();

            if (arguments.Length == 1 && arguments[0].Equals("generate-definition"))
                command = new GenerateDefinition();

            if (arguments.Length == 1 && arguments[0].Equals("load-definition"))
                command = new LoadDefinition();

            if (arguments.Length > 1)
            {
                command = new GenerateCode();
                data.Replacement.Message = arguments.Last();
                data.Replacement.Arguments = new string[arguments.Length - 1];
                Array.Copy(arguments, data.Replacement.Arguments, arguments.Length - 1);
            }

            if (command == null)
                return "Unsupported command";

            data = command.Run(definitionLoader, data);
            Console.WriteLine($"Generating finished, press any key to close.");

            return data.Message;
        }
    }
}
