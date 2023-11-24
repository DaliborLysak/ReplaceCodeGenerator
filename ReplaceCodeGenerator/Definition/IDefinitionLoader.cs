namespace ReplaceCodeGenerator.Definitions
{
    public interface IDefinitionLoader
    {
        string GetDefinitionFolder();

        Definition Load();

        void Save(Definition definition, string folder = "");
    }
}
