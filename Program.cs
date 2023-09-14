namespace ReplaceCodeGenerator
{
    public class Program
    {
        static void Main(string[] args)
        {
            var generator = new Generator();
            Console.WriteLine(generator.Generate(args));
            //Console.WriteLine("Press any key to continue.");
            //Console.ReadKey();
        }
    }
}
