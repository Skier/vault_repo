using System;

namespace TractInc.Server.Codegen
{
    class Program
    {
        static void Main()
        {
            CodeGenerator generator = new CodeGenerator();
            SchemaExtractor schemaExtractor = new SchemaExtractor();

            generator.Progress += new CodeGenerator.ProgressEventHandler(AddMessage);

            generator.OutputRootDirectory = @"D:\work\affilia\tractinc\server\!output";

            schemaExtractor.Extract("server=(local);user id=sa;password=gfhjkm;database=TractInc4;connect timeout=10;", 
                generator.DbSchemaFilePath);

            generator.GenerateEmptyClass = true;

            generator.Generate();
        }

        public static void AddMessage(String message)
        {
            Console.WriteLine(message);
        }
    }
}
