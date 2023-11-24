using ReplaceCodeGenerator.Definitions;

namespace ReplaceCodeGenerator.Commands
{
    internal class GenerateDefinition : ICommand
    {
        public Data Run(IDefinitionLoader definitionLoader, Data data)
        {
            var definitionFolder = definitionLoader.GetDefinitionFolder();
            var message = $"Generating definition at:{definitionFolder}";
            data.Message = message;
            Console.WriteLine(message);
            definitionLoader.Save(new Definition(), definitionFolder);

            return data;
        }
    }
}
