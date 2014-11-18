using System;
using System.Collections.Generic;
using System.Text;

namespace Dalworth.Codegen
{
    class Program
    {
        static void Main(string[] args)
        {
            CodeGenerator generator = new CodeGenerator();
            SchemaExtractor schemaExtractor = new SchemaExtractor();

            generator.Progress += new CodeGenerator.ProgressEventHandler(AddMessage);

            generator.OutputRootDirectory = @"C:\work\Dalworth\main\Mobile\Dalworth.Codegen\!output";

            schemaExtractor.Extract("user id=sa;password=gfhjkm;initial catalog=dalworth;data source=WSSERGEYK;connect timeout=10", 
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
