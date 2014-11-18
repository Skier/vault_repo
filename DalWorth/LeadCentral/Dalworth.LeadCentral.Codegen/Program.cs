using System;

namespace Servman.Codegen
{
    class Program
    {
        static void Main(string[] args)
        {
            var generator = new CodeGenerator();
            var schemaExtractor = new SchemaExtractor();

            var databaseName = args[0] ?? "servman_main";
            var outputDir = args[1] ?? "!output";

            generator.Progress += new CodeGenerator.ProgressEventHandler(AddMessage);

            generator.OutputRootDirectory = @"D:\_work\Dalworth\LeadCentral\Dalworth.LeadCentral.Codegen\" + outputDir;
            schemaExtractor.Extract(string.Format("user id=sa;password=gfhjkm;initial catalog={0};data source=VALERY-8730W;connect timeout=10", databaseName),
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
