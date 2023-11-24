using ReplaceCodeGenerator.Commands;

namespace ReplaceCodeGenerator.Replacer
{
    internal class TextReplacer : ITextReplacer
    {
        public string Replace(string line, Replacement replacement)
        {
            line = line.Replace("<author>", replacement.Author);
            line = line.Replace("{date.}", DateTime.Now.ToString("dd.MM.yyyy"));
            line = line.Replace("{date-}", DateTime.Now.ToString("dd-MM-yyyy"));
            line = line.Replace("[message]", replacement.Message);

            // TODO - louzy because of time pressure
            line = line.Replace("[0]", replacement.Replacements.Length > 0 ? replacement.Replacements[0] : string.Empty);
            line = line.Replace("[0U]", replacement.Replacements.Length > 0 ? replacement.Replacements[0].ToUpper() : string.Empty);
            line = line.Replace("[0L]", replacement.Replacements.Length > 0 ? replacement.Replacements[0].ToLower() : string.Empty);

            return line;
        }
    }
}
