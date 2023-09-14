using System.Reflection;
using System.Text.Json;

namespace ReplaceCodeGenerator.Definitions
{
    internal class DefinitionLoader : IDefinitionLoader
    {
        private const string DEFINITION_FILE_NAME = "definition.json";
        private const string REPLACE_CODE_GENERATOR = "ReplaceCodeGenerator";

        public Definition Load()
        {
            var definition = new Definition();

            var definitionFile = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                REPLACE_CODE_GENERATOR,
                DEFINITION_FILE_NAME);

            if (File.Exists(definitionFile))
            {

                string jsonString = File.ReadAllText(definitionFile);
                definition = JsonSerializer.Deserialize<Definition>(jsonString)!;
            }

            return definition;
        }

        public async void Save(Definition definition, string folder = "")
        {
            var definitionFolder = folder;

            if (string.IsNullOrEmpty(definitionFolder))
                definitionFolder = GetDefinitionFolder();

            if (string.IsNullOrEmpty(definitionFolder))
                return;

            if (!Directory.Exists(definitionFolder))
                Directory.CreateDirectory(definitionFolder);

            using FileStream stream = File.Create(Path.Combine(definitionFolder, DEFINITION_FILE_NAME));
            await JsonSerializer.SerializeAsync(stream, definition);
            await stream.DisposeAsync();
        }

        public string GetDefinitionFolder()
        {
            var definitionFolder = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                REPLACE_CODE_GENERATOR);

            return definitionFolder;
        }
    }
}