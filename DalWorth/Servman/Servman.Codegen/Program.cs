using System;
using System.Collections.Generic;
using System.Text;

namespace Servman.Codegen
{
    class Program
    {
        static void Main(string[] args)
        {
            CodeGenerator generator = new CodeGenerator();
            SchemaExtractor schemaExtractor = new SchemaExtractor();

            generator.Progress += new CodeGenerator.ProgressEventHandler(AddMessage);

            generator.OutputRootDirectory = @"D:\_work\Dalworth\Servman\Servman.Codegen\!output_template";

            schemaExtractor.Extract("user id=sa;password=gfhjkm;initial catalog=servman;data source=VALERY-8730W;connect timeout=10", 
                generator.DbSchemaFilePath);

            generator.GenerateEmptyClass = false;
            generator.GenerateActionScriptClass = true;

            generator.Generate();
        }


        public static void AddMessage(String message)
        {
            Console.WriteLine(message);
        }
    }
}
