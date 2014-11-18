using System;
using System.Collections.Generic;
using System.Text;

namespace MobileTech.Codegen
{
    class Program
    {
        static void Main(string[] args)
        {
            CodeGenerator generator = new CodeGenerator();
            SchemaExtractor schemaExtractor = new SchemaExtractor();

            generator.Progress += new CodeGenerator.ProgressEventHandler(AddMessage);

            generator.OutputRootDirectory = @"D:\dev\source\MobileTech\MobileTech.Domain";

            schemaExtractor.Extract("user id=sa;password=go;initial catalog=MobileTech2;data source=RA\\MSSQL2000;connect timeout=10", 
                generator.DbSchemaFilePath);

            generator.GenerateEmptyClass = false;

            generator.Generate();
        }


        public static void AddMessage(String message)
        {
            Console.WriteLine(message);
        }
    }
}
