using System;
using System.Collections.Generic;
using System.Text;

namespace QuickBooksAgent.Codegen
{
    class Program
    {
        static void Main(string[] args)
        {
            CodeGenerator generator = new CodeGenerator();
            SchemaExtractor schemaExtractor = new SchemaExtractor();

            generator.Progress += new CodeGenerator.ProgressEventHandler(AddMessage);

            generator.OutputRootDirectory = @"D:\dev\source\QuickBooksAgent\QuickBooksAgent.Domain";

            schemaExtractor.Extract("user id=sa;password=go;initial catalog=QuickBooksAgent;data source=Jupiter;connect timeout=10", 
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
