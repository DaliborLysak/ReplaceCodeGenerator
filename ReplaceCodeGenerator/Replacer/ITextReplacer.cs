using ReplaceCodeGenerator.Commands;

namespace ReplaceCodeGenerator.Replacer
{
    public interface ITextReplacer
    {
        string Replace(string line, Replacement replacement);
    }
}
