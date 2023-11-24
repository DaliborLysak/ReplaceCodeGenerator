using ReplaceCodeGenerator.Commands;

namespace ReplaceCodeGenerator.Argument
{
    public interface IArgumentParser
    {
        ICommand? ParseCommand(string[] arguments);

        (string[] replacements, string message) ParseReplacements(string[] arguments);
    }
}
