using System.Data.SqlClient;
using TractInc.TrueTract.Data;
using FileInfo=TractInc.TrueTract.Entity.FileInfo;

namespace TractInc.TrueTract
{
public class File
{
    
    public FileInfo AddFile(FileInfo file, string uploadId)
    {
        file.FilePath = Weborb.Util.Paths.GetUploadPath() + uploadId;
        file.FilePath = "";

        using (SqlConnection conn = SQLHelper.CreateConnection())
        {
            conn.Open();

            SqlTransaction tran = conn.BeginTransaction();

            (new FileDataMapper()).Create(tran, file);

            tran.Commit();
        }
        
        return file;
    }
    
}
}
