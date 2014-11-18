using System;
using System.Collections.Generic;
using System.Text;

namespace Dalworth.Server.Servman.Codegen
{
    class Program
    {
        static void Main(string[] args)
        {
            CodeGenerator generator = new CodeGenerator();
            SchemaExtractor schemaExtractor = new SchemaExtractor();

            generator.Progress += new CodeGenerator.ProgressEventHandler(AddMessage);

            generator.OutputRootDirectory = @"C:\Work\main\Server\Dalworth.Server.Servman.Codegen\!output";

            schemaExtractor.Extract("initial catalog=servman;data source=PSCFURMAND208;Integrated Security=SSPI;", 
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
