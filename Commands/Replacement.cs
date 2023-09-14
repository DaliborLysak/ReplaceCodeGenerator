﻿namespace ReplaceCodeGenerator.Commands
{
    public class Replacement
    {
        public string Author { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string[] Arguments { get; set; } = new string[0];
    }
}
