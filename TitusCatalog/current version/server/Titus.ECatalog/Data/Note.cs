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

    public class Note
    {

        private static Note c_Note = new Note();

        public static Note GetInstance()
        {
            return c_Note;
        }

        private Note()
        {
        }

        private const string SQL_SELECT_BY_PAGE_ID = @"
            select  [NoteId],
                    [UserId],
                    [DocumentPageId],
                    [Top],
                    [Left],
                    [Width],
                    [Height],
                    [NoteText]
            from    [Note]
            where   [DocumentPageId] = @DocumentPageId";

        public List<NoteDataObject> FindByPageId(SqlTransaction tran, int pageId)
        {
            List<NoteDataObject> result = new List<NoteDataObject>();

            DbParameter[] parms = new DbParameter[1] { new SqlParameter("@DocumentPageId", pageId) };

            using (IDataReader dataReader = SqlHelper.ExecuteReader(tran, CommandType.Text, SQL_SELECT_BY_PAGE_ID, parms))
            {
                while (dataReader.Read())
                {
                    NoteDataObject noteInfo = new NoteDataObject();

                    noteInfo.NoteId = (int)dataReader.GetValue(0);
                    noteInfo.UserId = (int)dataReader.GetValue(1);
                    noteInfo.DocumentPageId = (int)dataReader.GetValue(2);
                    noteInfo.Top = (int)dataReader.GetValue(3);
                    noteInfo.Left = (int)dataReader.GetValue(4);
                    noteInfo.Width = (int)dataReader.GetValue(5);
                    noteInfo.Height = (int)dataReader.GetValue(6);
                    noteInfo.NoteText = (string)dataReader.GetValue(7);

                    result.Add(noteInfo);
                }
            }

            return result;
        }

        private const string SQL_INSERT = @"
        insert  into [Note]
              ( [UserId],
                [DocumentPageId],
                [Top],
                [Left],
                [Width],
                [Height],
                [NoteText])
        values( @UserId,
                @DocumentPageId,
                @Top,
                @Left,
                @Width,
                @Height,
                @NoteText);
        select  cast(scope_identity() as int)";

        public void Insert(SqlTransaction tran, NoteDataObject noteInfo)
        {
            DbParameter[] parms = new DbParameter[7] {
                new SqlParameter("@UserId", noteInfo.UserId),
                new SqlParameter("@DocumentPageId", noteInfo.DocumentPageId),
                new SqlParameter("@Top", noteInfo.Top),
                new SqlParameter("@Left", noteInfo.Left),
                new SqlParameter("@Width", noteInfo.Width),
                new SqlParameter("@Height", noteInfo.Height),
                new SqlParameter("@NoteText", noteInfo.NoteText)
            };

            noteInfo.NoteId = (int)SqlHelper.ExecuteScalar(tran, CommandType.Text, SQL_INSERT, parms);
        }

        private const string SQL_UPDATE = @"
        update  [Note]
        set     [DocumentPageId] = @DocumentPageId,
                [Top] = @Top,
                [Left] = @Left,
                [Width] = @Width,
                [Height] = @Height,
                [NoteText] = @NoteText
        where   [NoteId] = @NoteId";

        public void Update(SqlTransaction tran, NoteDataObject noteInfo)
        {
            DbParameter[] parms = new DbParameter[7] {
                new SqlParameter("@NoteId", noteInfo.NoteId),
                new SqlParameter("@DocumentPageId", noteInfo.DocumentPageId),
                new SqlParameter("@Top", noteInfo.Top),
                new SqlParameter("@Left", noteInfo.Left),
                new SqlParameter("@Width", noteInfo.Width),
                new SqlParameter("@Height", noteInfo.Height),
                new SqlParameter("@NoteText", noteInfo.NoteText)
            };

            SqlHelper.ExecuteNonQuery(tran, CommandType.Text, SQL_UPDATE, parms);
        }

        private const string SQL_REMOVE = @"
        delete  from [Note]
        where   [NoteId] = @NoteId";

        public void Remove(SqlTransaction tran, int noteId)
        {
            DbParameter[] parms = new DbParameter[1] { new SqlParameter("@NoteId", noteId) };

            SqlHelper.ExecuteNonQuery(tran, CommandType.Text, SQL_REMOVE, parms);
        }

    }

}
