using ReplaceCodeGenerator.Definitions;
using ReplaceCodeGenerator.Replacer;
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
        private readonly ITextReplacer textReplacer;

        public GenerateCode(ITextReplacer textReplacer)
        {
            this.textReplacer = textReplacer;
        }

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
            var lines = File.ReadAllLines(path);
            var lineNumber = 0;

            while (lineNumber < lines.Length)
            {
                var line = lines[lineNumber];
                lines[lineNumber] = this.textReplacer.Replace(line, replacement);

                lineNumber++;
            }

            var fileName = Path.GetFileName(this.textReplacer.Replace(path, replacement));
            var newFile = Path.Combine(destination, fileName);
            File.WriteAllLines(newFile, lines);
        }
    }
}
