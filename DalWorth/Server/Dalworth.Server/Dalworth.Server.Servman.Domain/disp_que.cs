using System;
using System.Collections.Generic;
using System.Data;
using Dalworth.Server.Data;

namespace Dalworth.Server.Servman.Domain
{
    public partial class disp_que
    {
        public disp_que(){ }

        #region FindByTechAndDate

        private const string SqlFindByTechAndDate =
            @"select *  from [disp_que] 
                where tech_id = ?
                  and d_dispatch = ?";

        public static List<disp_que> FindByTechAndDate(string techId, DateTime date)
        {
            List<disp_que> dispques = new List<disp_que>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByTechAndDate, ConnectionKeyEnum.Servman))
            {
                Database.PutParameter(dbCommand, "@tech_id", techId);
                Database.PutParameter(dbCommand, "@d_dispatch", date.Date);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        dispques.Add(Load(dataReader));
                    }
                }
            }

            return dispques;
        }

        #endregion
    }
}
