using ReplaceCodeGenerator.Definitions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReplaceCodeGenerator.Commands
{
    internal class GenerateCode : ICommand
    {
        public Data Run(IDefinitionLoader definitionLoader, Data data)
        {
            if (data is null)
                return new Data() { Message = "Missing data!!!"};

            data.Definition = definitionLoader.Load();

            if (!Directory.Exists(data.Definition.ReplaceFrom))
                return new Data() { Message = $"Missing source data folder: {data.Definition.ReplaceFrom}" };

            if (!Directory.Exists(data.Definition.ReplaceTo))
                Directory.CreateDirectory(data.Definition.ReplaceTo);

            data.Replacement.Author = data.Definition.Author;
            var files = Directory.GetFiles(data.Definition.ReplaceFrom);
            foreach (var file in files) 
            {
                this.GenerateFile(file, data.Definition.ReplaceTo, data.Replacement);
            }

            data.Message = "Code generating finished.";
            return data;
        }

        private void GenerateFile(string path, string destination, Replacement replacement)
        {
            var lines = File.ReadAllLines(path, Encoding.UTF8);
            var lineNumber = 0;

            while (lineNumber < lines.Length)
            {
                var line = lines[lineNumber];
                lines[lineNumber] = DoReplace(line, replacement);

                lineNumber++;
            }

            var fileName = Path.GetFileName(this.DoReplace(path, replacement).ToLowerInvariant());
            var newFile = Path.Combine(destination, fileName);
            File.WriteAllLines(newFile, lines, Encoding.UTF8);
        }

        private string DoReplace(string line, Replacement replacement)
        {
            line = line.Replace("<author>", replacement.Author);
            line = line.Replace("{date.}", DateTime.Now.ToString("dd.MM.yyyy"));
            line = line.Replace("{date-}", DateTime.Now.ToString("dd-MM-yyyy"));
            line = line.Replace("[message]", replacement.Message);

            // TODO - louzy because of time pressure
            line = line.Replace("[0]", replacement.Arguments.Length > 0 ? replacement.Arguments[0] : string.Empty);
            line = line.Replace("[0U]", replacement.Arguments.Length > 0 ? replacement.Arguments[0].ToUpper() : string.Empty);
            line = line.Replace("[0L]", replacement.Arguments.Length > 0 ? replacement.Arguments[0].ToLower() : string.Empty);

            return line;
        }
    }
}
