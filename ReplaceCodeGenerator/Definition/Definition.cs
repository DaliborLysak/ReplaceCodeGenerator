namespace ReplaceCodeGenerator.Definitions
{
    public class Definition
    {
        public string ReplaceFrom { get; set; } = string.Empty;
        public string ReplaceTo { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;

        public override string ToString()
        {
            //return base.ToString();
            return $"ReplaceFrom: {ReplaceFrom}{System.Environment.NewLine}ReplaceTo: {ReplaceTo}{System.Environment.NewLine}Author: {Author}";
        }
    }
}