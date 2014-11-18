
          namespace TractInc.Expense.Domain
          {
              using System;
              using System.Text;
              using System.Data;
              using System.Data.SqlClient;
              using System.Globalization;
              using TractInc.Expense.Entity;

              public class NoteDataMapper : _NoteDataMapper
            {
              public NoteDataMapper()
              {}
              public NoteDataMapper(TractIncRAIDDb database):base(database)
              {}

              private const string SQL_INSERT = @"
            INSERT INTO [Note] (RelatedItemId, ItemType, SenderId, Posted, NoteText) VALUES
                ({0}, '{1}', {2}, '{3}', '{4}')";

              public void Insert(SqlTransaction tran, NoteDataObject noteInfo)
              {
                  IFormatProvider culture = new CultureInfo("en-US", true);
                  string sql = String.Format(
                      culture,
                      SQL_INSERT,
                      noteInfo.RelatedItemId,
                      noteInfo.ItemType,
                      noteInfo.SenderId,
                      noteInfo.Posted,
                      noteInfo.NoteText);

                  SqlHelper.ExecuteNonQuery(tran, CommandType.Text, sql, null);
              }

          }
        }
      