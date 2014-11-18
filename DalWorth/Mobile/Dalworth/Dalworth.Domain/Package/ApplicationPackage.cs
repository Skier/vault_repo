using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dalworth.Data;

namespace Dalworth.Domain.Package
{
    public class ApplicationPackage
    {
        #region Application

        private Application m_application;
        public Application Application
        {
            get { return m_application; }
            set { m_application = value; }
        }

        #endregion

        #region Work

        private Work m_work;
        public Work Work
        {
            get { return m_work; }
            set { m_work = value; }
        }

        #endregion

        #region GetApplicationPackage

        private const string SqlGetApplicationPackage =
            @"SELECT *
            FROM Application a
                Left join Work w on a.WorkId = w.ID";

        public static ApplicationPackage GetApplicationPackage()
        {
            ApplicationPackage package = new ApplicationPackage();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlGetApplicationPackage))
            {
                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        package.Application = Domain.Application.Load(dataReader);
                        if (package.Application.WorkId != null)
                        {
                            package.Work = new Work();
                            package.Work.ID = (int) dataReader.GetValue(3);
                            package.Work.DispatchEmployeeId = (int) dataReader.GetValue(4);
                            package.Work.TechnicianEmployeeId = (int) dataReader.GetValue(5);
                            package.Work.VanId = (int) dataReader.GetValue(6);
                            package.Work.StartDate = (DateTime?) dataReader.GetValue(7);
                            package.Work.WorkStatusId = (int?) dataReader.GetValue(8);
                            package.Work.StartMessage = (string) dataReader.GetValue(9);
                            package.Work.EndMessage = (string) dataReader.GetValue(10);
                            package.Work.EquipmentNotes = (string) dataReader.GetValue(11);
                        } else
                        {
                            package.Work = null;
                        }
                    }
                }
            }
            return package;
        }

        #endregion        
    }
}
