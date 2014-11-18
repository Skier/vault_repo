using System;
using System.Collections.Generic;
using System.Text;
using MobileTech.Data;
using System.Data;

namespace MobileTech.Windows.UI.DatabaseManager
{
    public class PopulateItemsTask:Task
    {
        public int ItemsCount = 30000;

        protected override void Main()
        {
            Random rnd = new Random();
            byte[] chars = new byte[20];


            String sql = "Insert Into Item ( LocationId, RouteNumber, ItemNumber, ItemCategoryId,ItemTypeId,Name,Description,NameSortIndex,ItemNumberSortIndex/*,IXName0*/ )" +
                " Values (1,1,@ItemNumber,@ItemCategoryId,1,@Name,'',0,0/*,0*/);";

            AddMessage("Removing old items ...");

            Database.Begin();

            using (IDbCommand dbCommand = Database.PrepareCommand("Delete from Item Where convert(int,ItemNumber) >= 10000"))
            {

                dbCommand.Prepare();

                dbCommand.ExecuteNonQuery();
            }

            Database.Commit();

            Database.Begin();

            AddMessage("Inserting new items ...");

            using (IDbCommand dbCommand = Database.PrepareCommand(sql))
            {

                double max = ItemsCount;

                try
                {
                    for (int i = 0; i < max; i++)
                    {
                        for (int j = 0; j < 20; j++)
                        {
                            chars[j] = (byte)rnd.Next(65, 90);
                        }

                        String name = System.Text.ASCIIEncoding.ASCII.GetString(chars, 0, 20);

                        Database.PutParameter(dbCommand, "@ItemNumber", (i + 10000).ToString());
                        Database.PutParameter(dbCommand, "@Name", name.ToLower());
                        Database.PutParameter(dbCommand, "@ItemCategoryId", rnd.Next(1,11));


                        dbCommand.ExecuteNonQuery();

                        dbCommand.Parameters.Clear();

                        if (i > 0)
                        {
                            double d = (double)i;
                            double pbv = (d / max) * 100;

                            if (PercentComplete < pbv)
                            {
                                SetPercent((int)pbv);
                            }
                        }

                    }

                    AddMessage("Commiting transaction ...");

                    Database.Commit();

                    AddMessage("Complete");
                }
                catch (Exception ex)
                {
                    Database.Rollback();

                    throw ex;
                }
            }

        }
    }
}
