using ReplaceCodeGenerator.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReplaceCodeGenerator.Commands
{
    internal class LoadDefinition : ICommand
    {
        public Data Run(IDefinitionLoader definitionLoader, Data data)
        {
            var definitionFolder = definitionLoader.GetDefinitionFolder();
            var message = $"Loading definition at:{definitionFolder}";
            data.Message = message;
            Console.WriteLine(message);
            data.Definition = definitionLoader.Load();
            Console.WriteLine(data.Definition.ToString());

            return data;
        }
    }
}
