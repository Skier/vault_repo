using System;
using System.Collections.Generic;
using System.Text;

namespace Dalworth.Server.Codegen
{
    class Program
    {
        static void Main(string[] args)
        {
            CodeGenerator generator = new CodeGenerator();
            SchemaExtractor schemaExtractor = new SchemaExtractor();

            generator.Progress += new CodeGenerator.ProgressEventHandler(AddMessage);

            generator.OutputRootDirectory = @"C:\temp\!output";

            //schemaExtractor.Extract("user id=sa;password=++Winston;initial catalog=ppctester;data source=boris;connect timeout=10", 
            //    generator.DbSchemaFilePath);

            schemaExtractor.Extract("user id=sa;password=++Winston;initial catalog=dalworth_server;data source=boris;connect timeout=10", 
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
