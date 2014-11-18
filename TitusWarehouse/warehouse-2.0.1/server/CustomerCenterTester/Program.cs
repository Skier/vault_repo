using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using ShoppingCart;

namespace CustomerCenterTester
{
class Program
{
    static void Main(string[] args) {
        SQL tts = new SQL("titus");
/*
        DataSet ds = tts.Get_Customer_Account("rhua@airsysco.com");
        PrintRows(ds);
        Console.WriteLine("----------------------------------");
        ds = tts.Get_Customer_Account_Detail("001-001-A24101");
        PrintRows(ds);
        Console.WriteLine("");
        Console.WriteLine("----------------------------------");
        ds = tts.Get_User_Name("rhua@airsysco.com");

*/
        Console.WriteLine("===   TTS   ===");
        DataSet ds = tts.getProductCategory("TTS");
        PrintRows(ds);
/*
        SQL kru = new SQL("");
        Console.WriteLine("===   KRU   ===");
        ds = tts.getProductCategory("KRU");
        PrintRows(ds);

        Console.WriteLine("===   TNB   ===");
        ds = tts.getProductCategory("TNB");
        PrintRows(ds);
*/
    }

    private static void PrintRows(DataSet dataSet)
    {
        // For each table in the DataSet, print the row values.
        foreach(DataTable table in dataSet.Tables)
        {
            Console.WriteLine("Table: "+ table.TableName.ToString());
            foreach (DataColumn column in table.Columns) {
                Console.Write(column.ColumnName+"\t" );
            }
            Console.WriteLine("");
            Console.WriteLine("----------------------------------------------");

            foreach(DataRow row in table.Rows)
            {
                foreach (DataColumn column in table.Columns)
                {
                    Console.Write(row[column]);
                    Console.Write("\t");
                }
                Console.WriteLine("");
            }
        }
    }

}
}
