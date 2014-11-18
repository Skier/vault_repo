using System;

namespace Servman.Codegen
{
    class Program
    {
        static void Main(string[] args)
        {
            var generator = new CodeGenerator();
            var schemaExtractor = new SchemaExtractor();

            var databaseName = "leadcentral";
            if (args.Length > 0)
                databaseName = args[0];

            var outputDir = "!output1";
            if (args.Length > 1)
                outputDir = args[1];

            generator.Progress += AddMessage;

            generator.OutputRootDirectory = @"c:\temp\" + outputDir;
            schemaExtractor.Extract(string.Format("user id=sa;password=++Winston;initial catalog={0};data source=boris;connect timeout=10", databaseName),
                generator.DbSchemaFilePath);

            generator.GenerateEmptyClass = true;
            generator.GenerateActionScriptClass = true;

            generator.Generate();
        }


        public static void AddMessage(String message)
        {
            Console.WriteLine(message);
        }
    }
}
