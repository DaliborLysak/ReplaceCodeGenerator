using ReplaceCodeGenerator.Definitions;

namespace ReplaceCodeGenerator.Commands
{
    public interface ICommand
    {
        Data Run(IDefinitionLoader definitionLoader, Data data);
    }
}
