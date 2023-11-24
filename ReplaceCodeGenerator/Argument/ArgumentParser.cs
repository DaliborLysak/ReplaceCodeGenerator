using ReplaceCodeGenerator.Commands;
using System.Data.Common;

namespace ReplaceCodeGenerator.Argument
{
    internal class ArgumentParser : IArgumentParser
    {
        private const string GENERATE_DEFINITION = "generate-definition";
        private const string LOAD_DEFINITION = "load-definition";

        private static readonly Dictionary<string, Func<ICommand>> commandCatalog = new Dictionary<string, Func<ICommand>>()
        {
            [GENERATE_DEFINITION] = () => new GenerateDefinition(),
            [LOAD_DEFINITION] = () => new LoadDefinition(),
        };

        public ICommand? ParseCommand(string[] arguments)
        {
            if (arguments.Length == 1 && commandCatalog.ContainsKey(arguments[0]))
            {
                return commandCatalog[arguments[0]]?.Invoke();
            }

            return null;
        }

        private const string MESSAGE = "-message";
        private const string REPLACEMENTS = "-replacements";

        public (string[] replacements, string message) ParseReplacements(string[] arguments)
        {
            var parsedReplacements = Array.Empty<string>();
            var parsedMessage = string.Empty;

            var indexer = 0;
            while (indexer < arguments.Length)
            {
                if (indexer + 1 > arguments.Length)
                    break;

                if ((arguments[indexer]).Equals(REPLACEMENTS))
                {
                    parsedReplacements = arguments[indexer + 1].Split(" ");
                }

                if ((arguments[indexer]).Equals(MESSAGE))
                {
                    parsedMessage = arguments[indexer + 1];
                }

                indexer++;
            }

            return (replacements: parsedReplacements, message: parsedMessage);
        }
    }
}
