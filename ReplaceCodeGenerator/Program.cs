using ReplaceCodeGenerator.Argument;
using ReplaceCodeGenerator.Definitions;
using ReplaceCodeGenerator.Replacer;

namespace ReplaceCodeGenerator
{
    public class Program
    {
        static void Main(string[] args)
        {
            var definitionLoader = new DefinitionLoader();
            var textReplacer = new TextReplacer();
            var argumentParser = new ArgumentParser();

            var generator = new Generator(textReplacer, definitionLoader, argumentParser);
            Console.WriteLine(generator.Generate(args));
            //Console.WriteLine("Press any key to continue.");
            //Console.ReadKey();
        }
    }
}
