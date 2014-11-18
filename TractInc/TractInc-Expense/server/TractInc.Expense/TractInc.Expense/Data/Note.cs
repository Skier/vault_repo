using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using TractInc.Expense.Entity;

namespace TractInc.Expense.Data
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

        private const string SQL_INSERT = @"
        insert  into [Note]
              ( [RelatedItemId],
                [ItemType],
                [SenderId],
                [Posted],
                [NoteText])
        values( @RelatedItemId,
                @ItemType,
                @SenderId,
                @Posted,
                @NoteText );
        select  cast(scope_identity() as int)";

        public void Insert(SqlTransaction tran, NoteDataObject noteInfo)
        {
            DbParameter[] parms = new DbParameter[5] {
                new SqlParameter("@RelatedItemId",  noteInfo.RelatedItemId),
                new SqlParameter("@ItemType",       noteInfo.ItemType),
                new SqlParameter("@SenderId",       noteInfo.SenderId),
                new SqlParameter("@Posted",         noteInfo.Posted),
                new SqlParameter("@NoteText",       noteInfo.NoteText),
            };

            noteInfo.NoteId = (int)SqlHelper.ExecuteScalar(tran, CommandType.Text, SQL_INSERT, parms);
        }

        private const string SQL_SELECT_BY_ITEM_TYPE_AND_ID = @"
        select  n.[NoteId],
                n.[RelatedItemId],
                n.[ItemType],
                n.[SenderId],
                n.[Posted],
                n.[NoteText],
                u.[Login] as [SenderName]
        from    [Note] n
                inner join [User] u
                        on n.[SenderId] = u.[UserId]
        where   [RelatedItemId] = @RelatedItemId
        and     [ItemType] = @ItemType";

        public List<NoteDataObject> GetNotes(SqlTransaction tran, int relatedItemId, string itemType)
        {
            List<NoteDataObject> result = new List<NoteDataObject>();

            DbParameter relatedItemIdParam = new SqlParameter("@RelatedItemId", relatedItemId);
            DbParameter itemTypeParam = new SqlParameter("@ItemType", itemType);
            DbParameter[] parms = new DbParameter[2] { relatedItemIdParam, itemTypeParam };

            using (IDataReader dataReader = SqlHelper.ExecuteReader(tran, CommandType.Text, SQL_SELECT_BY_ITEM_TYPE_AND_ID, parms))
            {
                while (dataReader.Read())
                {
                    NoteDataObject noteInfo = new NoteDataObject();

                    noteInfo.NoteId = (int)dataReader.GetValue(0);
                    noteInfo.RelatedItemId = (int)dataReader.GetValue(1);
                    noteInfo.ItemType = (string)dataReader.GetValue(2);
                    noteInfo.SenderId = (int)dataReader.GetValue(3);
                    noteInfo.Posted = dataReader.GetDateTime(4);
                    noteInfo.NoteText = (string)dataReader.GetValue(5);
                    noteInfo.SenderName = (string)dataReader.GetValue(6);

                    result.Add(noteInfo);
                }
            }

            return result;
        }

    }

}
