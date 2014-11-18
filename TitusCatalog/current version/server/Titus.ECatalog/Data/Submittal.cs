using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Titus.ECatalog.Entity;
using Titus.ECatalog.Util;

namespace Titus.ECatalog.Data
{

    public class Submittal
    {

        private static Submittal c_Submittal = new Submittal();

        public static Submittal GetInstance()
        {
            return c_Submittal;
        }

        private Submittal()
        {
        }

        private const string SQL_SELECT_BY_MODEL_ID = @"
            exec EC_GetSubmittals @ModelId";

        public List<SubmittalDataObject> FindByModelId(SqlTransaction tran, int modelId)
        {
            List<SubmittalDataObject> result = new List<SubmittalDataObject>();

            DbParameter[] parms = new DbParameter[1] { new SqlParameter("@ModelId", modelId) };

            using (IDataReader dataReader = SqlHelper.ExecuteReader(tran, CommandType.Text, SQL_SELECT_BY_MODEL_ID, parms))
            {
                while (dataReader.Read())
                {
                    SubmittalDataObject submittalInfo = new SubmittalDataObject();

                    submittalInfo.FileCategory = (string)dataReader.GetValue(0);
                    submittalInfo.FileCategoryId = (int)dataReader.GetValue(1);
                    submittalInfo.Secure = (int)dataReader.GetValue(2);
                    submittalInfo.FileType = (string)dataReader.GetValue(3);
                    submittalInfo.FileTypeSort = (int)dataReader.GetValue(4);
                    submittalInfo.FileId = (int)dataReader.GetInt64(5);
                    submittalInfo.FileName = (string)dataReader.GetValue(6);

                    result.Add(submittalInfo);
                }
            }

            return result;
        }

    }

}
