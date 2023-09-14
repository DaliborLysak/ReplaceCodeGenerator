using ReplaceCodeGenerator.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReplaceCodeGenerator.Commands
{
    public class Data
    {
        public string Message { get; set; } = string.Empty;

        public Definition Definition { get; set; } = new Definition();

        public Replacement Replacement { get; set; } = new Replacement();
    }
}
