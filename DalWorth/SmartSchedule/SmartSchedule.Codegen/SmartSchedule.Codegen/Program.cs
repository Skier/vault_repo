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

            generator.OutputRootDirectory = @"C:\work\SmartSchedule\SmartSchedule.Codegen\!output";

            schemaExtractor.Extract("user id=sa;password=gfhjkm;initial catalog=smartschedule;data source=wssergeik;connect timeout=10", 
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
