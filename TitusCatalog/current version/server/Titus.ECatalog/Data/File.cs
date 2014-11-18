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

    public class File
    {

        private static File c_File = new File();

        public static File GetInstance()
        {
            return c_File;
        }

        private File()
        {
        }

        private const string SQL_SELECT_BY_FILE_ID = @"
            select  f.[ID],
                    f.[Name],
                    f.[Description],
                    fb.[Binary]
            from    [tblFile] f
                    inner join [tblFileBinary] fb
                            on f.[ID] = fb.[File_Id]
            where   f.[ID] = @FileId";

        public FileDataObject FindByFileId(SqlTransaction tran, int fileId)
        {
            FileDataObject fileInfo = null;

            DbParameter[] parms = new DbParameter[1] { new SqlParameter("@FileId", fileId) };

            using (IDataReader dataReader = SqlHelper.ExecuteReader(tran, CommandType.Text, SQL_SELECT_BY_FILE_ID, parms))
            {
                while (dataReader.Read())
                {
                    fileInfo = new FileDataObject();

                    //fileInfo.FileId = dataReader.GetInt32(0);
                    fileInfo.FileId = fileId;
                    fileInfo.FileName = dataReader.GetString(1);
                    fileInfo.Description = dataReader.GetString(2);

                    long dataLength = dataReader.GetBytes(3, 0, null, 0, 0);
                    byte[] buffer = new byte[dataLength];
                    dataReader.GetBytes(3, 0, buffer, 0, (int)dataLength);
                    fileInfo.Data = buffer;
                }
            }

            return fileInfo;
        }

    }

}
