namespace ReplaceCodeGenerator
{
    public class Program
    {
        static void Main(string[] args)
        {
            var definitionLoader = new DefinitionLoader();

            if (args.Length == 1 && args[0].Equals("generate-definition"))
            {
                var definitionFolder = definitionLoader.GetDefinitionFolder();
                Console.WriteLine($"Generating definition at:{definitionFolder}");
                var definition = new Definition();
                definitionLoader.Save(definition, definitionFolder);
                Console.WriteLine($"Generating finished, press any key to close.");
            }

            if (args.Length > 1)
            {
                var message = args[args.Length - 1];
                var replaceArgs = new string[];
            }

            Console.ReadKey();
        }
    }
}
