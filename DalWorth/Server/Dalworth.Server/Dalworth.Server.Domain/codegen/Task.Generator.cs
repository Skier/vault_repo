
    using System;
    using System.Data;
    using System.Collections.Generic;
    using Dalworth.Server.Data;
    using Dalworth.Server.SDK;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Text;
  
      namespace Dalworth.Server.Domain
      {


      public partial class Task : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into Task ( " +
      
        " ParentTaskId, " +
      
        " ServmanOrderNum, " +
      
        " ProjectId, " +
      
        " TaskTypeId, " +
      
        " TaskStatusId, " +
      
        " TaskFailTypeId, " +
      
        " IsReady, " +
      
        " Number, " +
      
        " Sequence, " +
      
        " CreateDate, " +
      
        " ServiceDate, " +
      
        " DurationMin, " +
      
        " Description, " +
      
        " Message, " +
      
        " Notes, " +
      
        " FailReason, " +
      
        " IsSentToServman, " +
      
        " ClosedAmount, " +
      
        " IsClosedAmountAutoCalculated, " +
      
        " EstimatedClosedAmount, " +
      
        " IsEstimatedClosedAmountAutoCalculated, " +
      
        " IsReincluded, " +
      
        " Modified, " +
      
        " LastSyncDate, " +
      
        " DumpedTaskId, " +
      
        " DumpWorkTransactionId, " +
      
        " FailCount, " +
      
        " LastFailDate, " +
      
        " IsRugCleaningDepartment, " +
      
        " DiscountPercentage, " +
      
        " ReadyDate " +
      
      ") Values (" +
      
        " ?ParentTaskId, " +
      
        " ?ServmanOrderNum, " +
      
        " ?ProjectId, " +
      
        " ?TaskTypeId, " +
      
        " ?TaskStatusId, " +
      
        " ?TaskFailTypeId, " +
      
        " ?IsReady, " +
      
        " ?Number, " +
      
        " ?Sequence, " +
      
        " ?CreateDate, " +
      
        " ?ServiceDate, " +
      
        " ?DurationMin, " +
      
        " ?Description, " +
      
        " ?Message, " +
      
        " ?Notes, " +
      
        " ?FailReason, " +
      
        " ?IsSentToServman, " +
      
        " ?ClosedAmount, " +
      
        " ?IsClosedAmountAutoCalculated, " +
      
        " ?EstimatedClosedAmount, " +
      
        " ?IsEstimatedClosedAmountAutoCalculated, " +
      
        " ?IsReincluded, " +
      
        " ?Modified, " +
      
        " ?LastSyncDate, " +
      
        " ?DumpedTaskId, " +
      
        " ?DumpWorkTransactionId, " +
      
        " ?FailCount, " +
      
        " ?LastFailDate, " +
      
        " ?IsRugCleaningDepartment, " +
      
        " ?DiscountPercentage, " +
      
        " ?ReadyDate " +
      
      ")";

      public static void Insert(Task task, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?ParentTaskId", task.ParentTaskId);
      
        Database.PutParameter(dbCommand,"?ServmanOrderNum", task.ServmanOrderNum);
      
        Database.PutParameter(dbCommand,"?ProjectId", task.ProjectId);
      
        Database.PutParameter(dbCommand,"?TaskTypeId", task.TaskTypeId);
      
        Database.PutParameter(dbCommand,"?TaskStatusId", task.TaskStatusId);
      
        Database.PutParameter(dbCommand,"?TaskFailTypeId", task.TaskFailTypeId);
      
        Database.PutParameter(dbCommand,"?IsReady", task.IsReady);
      
        Database.PutParameter(dbCommand,"?Number", task.Number);
      
        Database.PutParameter(dbCommand,"?Sequence", task.Sequence);
      
        Database.PutParameter(dbCommand,"?CreateDate", task.CreateDate);
      
        Database.PutParameter(dbCommand,"?ServiceDate", task.ServiceDate);
      
        Database.PutParameter(dbCommand,"?DurationMin", task.DurationMin);
      
        Database.PutParameter(dbCommand,"?Description", task.Description);
      
        Database.PutParameter(dbCommand,"?Message", task.Message);
      
        Database.PutParameter(dbCommand,"?Notes", task.Notes);
      
        Database.PutParameter(dbCommand,"?FailReason", task.FailReason);
      
        Database.PutParameter(dbCommand,"?IsSentToServman", task.IsSentToServman);
      
        Database.PutParameter(dbCommand,"?ClosedAmount", task.ClosedAmount);
      
        Database.PutParameter(dbCommand,"?IsClosedAmountAutoCalculated", task.IsClosedAmountAutoCalculated);
      
        Database.PutParameter(dbCommand,"?EstimatedClosedAmount", task.EstimatedClosedAmount);
      
        Database.PutParameter(dbCommand,"?IsEstimatedClosedAmountAutoCalculated", task.IsEstimatedClosedAmountAutoCalculated);
      
        Database.PutParameter(dbCommand,"?IsReincluded", task.IsReincluded);
      
        Database.PutParameter(dbCommand,"?Modified", task.Modified);
      
        Database.PutParameter(dbCommand,"?LastSyncDate", task.LastSyncDate);
      
        Database.PutParameter(dbCommand,"?DumpedTaskId", task.DumpedTaskId);
      
        Database.PutParameter(dbCommand,"?DumpWorkTransactionId", task.DumpWorkTransactionId);
      
        Database.PutParameter(dbCommand,"?FailCount", task.FailCount);
      
        Database.PutParameter(dbCommand,"?LastFailDate", task.LastFailDate);
      
        Database.PutParameter(dbCommand,"?IsRugCleaningDepartment", task.IsRugCleaningDepartment);
      
        Database.PutParameter(dbCommand,"?DiscountPercentage", task.DiscountPercentage);
      
        Database.PutParameter(dbCommand,"?ReadyDate", task.ReadyDate);
      

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        task.ID = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }

      public static void Insert(Task task)
      {
        Insert(task, null);
      }


      public static void Insert(List<Task>  taskList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(Task task in  taskList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?ParentTaskId", task.ParentTaskId);
      
        Database.PutParameter(dbCommand,"?ServmanOrderNum", task.ServmanOrderNum);
      
        Database.PutParameter(dbCommand,"?ProjectId", task.ProjectId);
      
        Database.PutParameter(dbCommand,"?TaskTypeId", task.TaskTypeId);
      
        Database.PutParameter(dbCommand,"?TaskStatusId", task.TaskStatusId);
      
        Database.PutParameter(dbCommand,"?TaskFailTypeId", task.TaskFailTypeId);
      
        Database.PutParameter(dbCommand,"?IsReady", task.IsReady);
      
        Database.PutParameter(dbCommand,"?Number", task.Number);
      
        Database.PutParameter(dbCommand,"?Sequence", task.Sequence);
      
        Database.PutParameter(dbCommand,"?CreateDate", task.CreateDate);
      
        Database.PutParameter(dbCommand,"?ServiceDate", task.ServiceDate);
      
        Database.PutParameter(dbCommand,"?DurationMin", task.DurationMin);
      
        Database.PutParameter(dbCommand,"?Description", task.Description);
      
        Database.PutParameter(dbCommand,"?Message", task.Message);
      
        Database.PutParameter(dbCommand,"?Notes", task.Notes);
      
        Database.PutParameter(dbCommand,"?FailReason", task.FailReason);
      
        Database.PutParameter(dbCommand,"?IsSentToServman", task.IsSentToServman);
      
        Database.PutParameter(dbCommand,"?ClosedAmount", task.ClosedAmount);
      
        Database.PutParameter(dbCommand,"?IsClosedAmountAutoCalculated", task.IsClosedAmountAutoCalculated);
      
        Database.PutParameter(dbCommand,"?EstimatedClosedAmount", task.EstimatedClosedAmount);
      
        Database.PutParameter(dbCommand,"?IsEstimatedClosedAmountAutoCalculated", task.IsEstimatedClosedAmountAutoCalculated);
      
        Database.PutParameter(dbCommand,"?IsReincluded", task.IsReincluded);
      
        Database.PutParameter(dbCommand,"?Modified", task.Modified);
      
        Database.PutParameter(dbCommand,"?LastSyncDate", task.LastSyncDate);
      
        Database.PutParameter(dbCommand,"?DumpedTaskId", task.DumpedTaskId);
      
        Database.PutParameter(dbCommand,"?DumpWorkTransactionId", task.DumpWorkTransactionId);
      
        Database.PutParameter(dbCommand,"?FailCount", task.FailCount);
      
        Database.PutParameter(dbCommand,"?LastFailDate", task.LastFailDate);
      
        Database.PutParameter(dbCommand,"?IsRugCleaningDepartment", task.IsRugCleaningDepartment);
      
        Database.PutParameter(dbCommand,"?DiscountPercentage", task.DiscountPercentage);
      
        Database.PutParameter(dbCommand,"?ReadyDate", task.ReadyDate);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?ParentTaskId",task.ParentTaskId);
      
        Database.UpdateParameter(dbCommand,"?ServmanOrderNum",task.ServmanOrderNum);
      
        Database.UpdateParameter(dbCommand,"?ProjectId",task.ProjectId);
      
        Database.UpdateParameter(dbCommand,"?TaskTypeId",task.TaskTypeId);
      
        Database.UpdateParameter(dbCommand,"?TaskStatusId",task.TaskStatusId);
      
        Database.UpdateParameter(dbCommand,"?TaskFailTypeId",task.TaskFailTypeId);
      
        Database.UpdateParameter(dbCommand,"?IsReady",task.IsReady);
      
        Database.UpdateParameter(dbCommand,"?Number",task.Number);
      
        Database.UpdateParameter(dbCommand,"?Sequence",task.Sequence);
      
        Database.UpdateParameter(dbCommand,"?CreateDate",task.CreateDate);
      
        Database.UpdateParameter(dbCommand,"?ServiceDate",task.ServiceDate);
      
        Database.UpdateParameter(dbCommand,"?DurationMin",task.DurationMin);
      
        Database.UpdateParameter(dbCommand,"?Description",task.Description);
      
        Database.UpdateParameter(dbCommand,"?Message",task.Message);
      
        Database.UpdateParameter(dbCommand,"?Notes",task.Notes);
      
        Database.UpdateParameter(dbCommand,"?FailReason",task.FailReason);
      
        Database.UpdateParameter(dbCommand,"?IsSentToServman",task.IsSentToServman);
      
        Database.UpdateParameter(dbCommand,"?ClosedAmount",task.ClosedAmount);
      
        Database.UpdateParameter(dbCommand,"?IsClosedAmountAutoCalculated",task.IsClosedAmountAutoCalculated);
      
        Database.UpdateParameter(dbCommand,"?EstimatedClosedAmount",task.EstimatedClosedAmount);
      
        Database.UpdateParameter(dbCommand,"?IsEstimatedClosedAmountAutoCalculated",task.IsEstimatedClosedAmountAutoCalculated);
      
        Database.UpdateParameter(dbCommand,"?IsReincluded",task.IsReincluded);
      
        Database.UpdateParameter(dbCommand,"?Modified",task.Modified);
      
        Database.UpdateParameter(dbCommand,"?LastSyncDate",task.LastSyncDate);
      
        Database.UpdateParameter(dbCommand,"?DumpedTaskId",task.DumpedTaskId);
      
        Database.UpdateParameter(dbCommand,"?DumpWorkTransactionId",task.DumpWorkTransactionId);
      
        Database.UpdateParameter(dbCommand,"?FailCount",task.FailCount);
      
        Database.UpdateParameter(dbCommand,"?LastFailDate",task.LastFailDate);
      
        Database.UpdateParameter(dbCommand,"?IsRugCleaningDepartment",task.IsRugCleaningDepartment);
      
        Database.UpdateParameter(dbCommand,"?DiscountPercentage",task.DiscountPercentage);
      
        Database.UpdateParameter(dbCommand,"?ReadyDate",task.ReadyDate);
      
      }

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        task.ID = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }
      }

      public static void Insert(List<Task>  taskList)
      {
        Insert(taskList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update Task Set "
      
        + " ParentTaskId = ?ParentTaskId, "
      
        + " ServmanOrderNum = ?ServmanOrderNum, "
      
        + " ProjectId = ?ProjectId, "
      
        + " TaskTypeId = ?TaskTypeId, "
      
        + " TaskStatusId = ?TaskStatusId, "
      
        + " TaskFailTypeId = ?TaskFailTypeId, "
      
        + " IsReady = ?IsReady, "
      
        + " Number = ?Number, "
      
        + " Sequence = ?Sequence, "
      
        + " CreateDate = ?CreateDate, "
      
        + " ServiceDate = ?ServiceDate, "
      
        + " DurationMin = ?DurationMin, "
      
        + " Description = ?Description, "
      
        + " Message = ?Message, "
      
        + " Notes = ?Notes, "
      
        + " FailReason = ?FailReason, "
      
        + " IsSentToServman = ?IsSentToServman, "
      
        + " ClosedAmount = ?ClosedAmount, "
      
        + " IsClosedAmountAutoCalculated = ?IsClosedAmountAutoCalculated, "
      
        + " EstimatedClosedAmount = ?EstimatedClosedAmount, "
      
        + " IsEstimatedClosedAmountAutoCalculated = ?IsEstimatedClosedAmountAutoCalculated, "
      
        + " IsReincluded = ?IsReincluded, "
      
        + " Modified = ?Modified, "
      
        + " LastSyncDate = ?LastSyncDate, "
      
        + " DumpedTaskId = ?DumpedTaskId, "
      
        + " DumpWorkTransactionId = ?DumpWorkTransactionId, "
      
        + " FailCount = ?FailCount, "
      
        + " LastFailDate = ?LastFailDate, "
      
        + " IsRugCleaningDepartment = ?IsRugCleaningDepartment, "
      
        + " DiscountPercentage = ?DiscountPercentage, "
      
        + " ReadyDate = ?ReadyDate "
      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static void Update(Task task, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", task.ID);
      
        Database.PutParameter(dbCommand,"?ParentTaskId", task.ParentTaskId);
      
        Database.PutParameter(dbCommand,"?ServmanOrderNum", task.ServmanOrderNum);
      
        Database.PutParameter(dbCommand,"?ProjectId", task.ProjectId);
      
        Database.PutParameter(dbCommand,"?TaskTypeId", task.TaskTypeId);
      
        Database.PutParameter(dbCommand,"?TaskStatusId", task.TaskStatusId);
      
        Database.PutParameter(dbCommand,"?TaskFailTypeId", task.TaskFailTypeId);
      
        Database.PutParameter(dbCommand,"?IsReady", task.IsReady);
      
        Database.PutParameter(dbCommand,"?Number", task.Number);
      
        Database.PutParameter(dbCommand,"?Sequence", task.Sequence);
      
        Database.PutParameter(dbCommand,"?CreateDate", task.CreateDate);
      
        Database.PutParameter(dbCommand,"?ServiceDate", task.ServiceDate);
      
        Database.PutParameter(dbCommand,"?DurationMin", task.DurationMin);
      
        Database.PutParameter(dbCommand,"?Description", task.Description);
      
        Database.PutParameter(dbCommand,"?Message", task.Message);
      
        Database.PutParameter(dbCommand,"?Notes", task.Notes);
      
        Database.PutParameter(dbCommand,"?FailReason", task.FailReason);
      
        Database.PutParameter(dbCommand,"?IsSentToServman", task.IsSentToServman);
      
        Database.PutParameter(dbCommand,"?ClosedAmount", task.ClosedAmount);
      
        Database.PutParameter(dbCommand,"?IsClosedAmountAutoCalculated", task.IsClosedAmountAutoCalculated);
      
        Database.PutParameter(dbCommand,"?EstimatedClosedAmount", task.EstimatedClosedAmount);
      
        Database.PutParameter(dbCommand,"?IsEstimatedClosedAmountAutoCalculated", task.IsEstimatedClosedAmountAutoCalculated);
      
        Database.PutParameter(dbCommand,"?IsReincluded", task.IsReincluded);
      
        Database.PutParameter(dbCommand,"?Modified", task.Modified);
      
        Database.PutParameter(dbCommand,"?LastSyncDate", task.LastSyncDate);
      
        Database.PutParameter(dbCommand,"?DumpedTaskId", task.DumpedTaskId);
      
        Database.PutParameter(dbCommand,"?DumpWorkTransactionId", task.DumpWorkTransactionId);
      
        Database.PutParameter(dbCommand,"?FailCount", task.FailCount);
      
        Database.PutParameter(dbCommand,"?LastFailDate", task.LastFailDate);
      
        Database.PutParameter(dbCommand,"?IsRugCleaningDepartment", task.IsRugCleaningDepartment);
      
        Database.PutParameter(dbCommand,"?DiscountPercentage", task.DiscountPercentage);
      
        Database.PutParameter(dbCommand,"?ReadyDate", task.ReadyDate);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(Task task)
      {
        Update(task, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " ParentTaskId, "
      
        + " ServmanOrderNum, "
      
        + " ProjectId, "
      
        + " TaskTypeId, "
      
        + " TaskStatusId, "
      
        + " TaskFailTypeId, "
      
        + " IsReady, "
      
        + " Number, "
      
        + " Sequence, "
      
        + " CreateDate, "
      
        + " ServiceDate, "
      
        + " DurationMin, "
      
        + " Description, "
      
        + " Message, "
      
        + " Notes, "
      
        + " FailReason, "
      
        + " IsSentToServman, "
      
        + " ClosedAmount, "
      
        + " IsClosedAmountAutoCalculated, "
      
        + " EstimatedClosedAmount, "
      
        + " IsEstimatedClosedAmountAutoCalculated, "
      
        + " IsReincluded, "
      
        + " Modified, "
      
        + " LastSyncDate, "
      
        + " DumpedTaskId, "
      
        + " DumpWorkTransactionId, "
      
        + " FailCount, "
      
        + " LastFailDate, "
      
        + " IsRugCleaningDepartment, "
      
        + " DiscountPercentage, "
      
        + " ReadyDate "
      

      + " From Task "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static Task FindByPrimaryKey(
      int iD, IDbConnection connection
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", iD);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("Task not found, search by primary key");

      }

      public static Task FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(Task task, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID",task.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(Task task)
      {
      return Exists(task, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from Task limit 1";

      using(IDbCommand dbCommand = Database.PrepareCommand(sql, connection))
      {
      using (IDataReader reader = dbCommand.ExecuteReader(CommandBehavior.SingleRow))
      {
      return reader.Read();
      }
      }
      }

      public static bool IsContainsData()
      {
      return IsContainsData(null);
      }

      #endregion

      #region Load

      public static Task Load(IDataReader dataReader, int offset)
      {
      Task task = new Task();

      task.ID = dataReader.GetInt32(0 + offset);
          
            if(!dataReader.IsDBNull(1 + offset))
            task.ParentTaskId = dataReader.GetInt32(1 + offset);
          
            if(!dataReader.IsDBNull(2 + offset))
            task.ServmanOrderNum = dataReader.GetString(2 + offset);
          task.ProjectId = dataReader.GetInt32(3 + offset);
          task.TaskTypeId = dataReader.GetInt32(4 + offset);
          
            if(!dataReader.IsDBNull(5 + offset))
            task.TaskStatusId = dataReader.GetInt32(5 + offset);
          
            if(!dataReader.IsDBNull(6 + offset))
            task.TaskFailTypeId = dataReader.GetInt32(6 + offset);
          task.IsReady = dataReader.GetBoolean(7 + offset);
          
            if(!dataReader.IsDBNull(8 + offset))
            task.Number = dataReader.GetString(8 + offset);
          
            if(!dataReader.IsDBNull(9 + offset))
            task.Sequence = dataReader.GetInt32(9 + offset);
          
            if(!dataReader.IsDBNull(10 + offset))
            task.CreateDate = dataReader.GetDateTime(10 + offset);
          
            if(!dataReader.IsDBNull(11 + offset))
            task.ServiceDate = dataReader.GetDateTime(11 + offset);
          
            if(!dataReader.IsDBNull(12 + offset))
            task.DurationMin = dataReader.GetInt32(12 + offset);
          
            if(!dataReader.IsDBNull(13 + offset))
            task.Description = dataReader.GetString(13 + offset);
          
            if(!dataReader.IsDBNull(14 + offset))
            task.Message = dataReader.GetString(14 + offset);
          
            if(!dataReader.IsDBNull(15 + offset))
            task.Notes = dataReader.GetString(15 + offset);
          
            if(!dataReader.IsDBNull(16 + offset))
            task.FailReason = dataReader.GetString(16 + offset);
          task.IsSentToServman = dataReader.GetBoolean(17 + offset);
          task.ClosedAmount = dataReader.GetDecimal(18 + offset);
          task.IsClosedAmountAutoCalculated = dataReader.GetBoolean(19 + offset);
          task.EstimatedClosedAmount = dataReader.GetDecimal(20 + offset);
          task.IsEstimatedClosedAmountAutoCalculated = dataReader.GetBoolean(21 + offset);
          task.IsReincluded = dataReader.GetBoolean(22 + offset);
          task.Modified = dataReader.GetDateTime(23 + offset);
          
            if(!dataReader.IsDBNull(24 + offset))
            task.LastSyncDate = dataReader.GetDateTime(24 + offset);
          
            if(!dataReader.IsDBNull(25 + offset))
            task.DumpedTaskId = dataReader.GetInt32(25 + offset);
          
            if(!dataReader.IsDBNull(26 + offset))
            task.DumpWorkTransactionId = dataReader.GetInt32(26 + offset);
          task.FailCount = dataReader.GetInt32(27 + offset);
          
            if(!dataReader.IsDBNull(28 + offset))
            task.LastFailDate = dataReader.GetDateTime(28 + offset);
          task.IsRugCleaningDepartment = dataReader.GetBoolean(29 + offset);
          task.DiscountPercentage = dataReader.GetInt32(30 + offset);
          
            if(!dataReader.IsDBNull(31 + offset))
            task.ReadyDate = dataReader.GetDateTime(31 + offset);
          

      return task;
      }

      public static Task Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From Task "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;
      public static void Delete(Task task, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?ID", task.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(Task task)
      {
        Delete(task, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From Task ";

      public static void Clear(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDeleteAll, connection))
      {
      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Clear()
      {
        Clear(null);
      }

      #endregion

      #region Find
      private const String SqlSelectAll = "Select "

      
        + " ID, "
      
        + " ParentTaskId, "
      
        + " ServmanOrderNum, "
      
        + " ProjectId, "
      
        + " TaskTypeId, "
      
        + " TaskStatusId, "
      
        + " TaskFailTypeId, "
      
        + " IsReady, "
      
        + " Number, "
      
        + " Sequence, "
      
        + " CreateDate, "
      
        + " ServiceDate, "
      
        + " DurationMin, "
      
        + " Description, "
      
        + " Message, "
      
        + " Notes, "
      
        + " FailReason, "
      
        + " IsSentToServman, "
      
        + " ClosedAmount, "
      
        + " IsClosedAmountAutoCalculated, "
      
        + " EstimatedClosedAmount, "
      
        + " IsEstimatedClosedAmountAutoCalculated, "
      
        + " IsReincluded, "
      
        + " Modified, "
      
        + " LastSyncDate, "
      
        + " DumpedTaskId, "
      
        + " DumpWorkTransactionId, "
      
        + " FailCount, "
      
        + " LastFailDate, "
      
        + " IsRugCleaningDepartment, "
      
        + " DiscountPercentage, "
      
        + " ReadyDate "
      

      + " From Task ";
      public static List<Task> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<Task> rv = new List<Task>();

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      while(dataReader.Read())
      {
      rv.Add(Load(dataReader));
      }

      }

      return rv;
      }

      }

      public static List<Task> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<Task> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (Task obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return ID == obj.ID && ParentTaskId == obj.ParentTaskId && ServmanOrderNum == obj.ServmanOrderNum && ProjectId == obj.ProjectId && TaskTypeId == obj.TaskTypeId && TaskStatusId == obj.TaskStatusId && TaskFailTypeId == obj.TaskFailTypeId && IsReady == obj.IsReady && Number == obj.Number && Sequence == obj.Sequence && CreateDate == obj.CreateDate && ServiceDate == obj.ServiceDate && DurationMin == obj.DurationMin && Description == obj.Description && Message == obj.Message && Notes == obj.Notes && FailReason == obj.FailReason && IsSentToServman == obj.IsSentToServman && ClosedAmount == obj.ClosedAmount && IsClosedAmountAutoCalculated == obj.IsClosedAmountAutoCalculated && EstimatedClosedAmount == obj.EstimatedClosedAmount && IsEstimatedClosedAmountAutoCalculated == obj.IsEstimatedClosedAmountAutoCalculated && IsReincluded == obj.IsReincluded && Modified == obj.Modified && LastSyncDate == obj.LastSyncDate && DumpedTaskId == obj.DumpedTaskId && DumpWorkTransactionId == obj.DumpWorkTransactionId && FailCount == obj.FailCount && LastFailDate == obj.LastFailDate && IsRugCleaningDepartment == obj.IsRugCleaningDepartment && DiscountPercentage == obj.DiscountPercentage && ReadyDate == obj.ReadyDate;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<Task> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(Task));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(Task item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<Task>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(Task));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<Task> itemsList
      = new List<Task>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is Task)
      itemsList.Add(deserializedObject as Task);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_iD;
      
        protected int? m_parentTaskId;
      
        protected String m_servmanOrderNum;
      
        protected int m_projectId;
      
        protected int m_taskTypeId;
      
        protected int? m_taskStatusId;
      
        protected int? m_taskFailTypeId;
      
        protected bool m_isReady;
      
        protected String m_number;
      
        protected int? m_sequence;
      
        protected DateTime? m_createDate;
      
        protected DateTime? m_serviceDate;
      
        protected int? m_durationMin;
      
        protected String m_description;
      
        protected String m_message;
      
        protected String m_notes;
      
        protected String m_failReason;
      
        protected bool m_isSentToServman;
      
        protected decimal m_closedAmount;
      
        protected bool m_isClosedAmountAutoCalculated;
      
        protected decimal m_estimatedClosedAmount;
      
        protected bool m_isEstimatedClosedAmountAutoCalculated;
      
        protected bool m_isReincluded;
      
        protected DateTime m_modified;
      
        protected DateTime? m_lastSyncDate;
      
        protected int? m_dumpedTaskId;
      
        protected int? m_dumpWorkTransactionId;
      
        protected int m_failCount;
      
        protected DateTime? m_lastFailDate;
      
        protected bool m_isRugCleaningDepartment;
      
        protected int m_discountPercentage;
      
        protected DateTime? m_readyDate;
      
      #endregion

      #region Constructors
      public Task(
      int 
          iD
      ) : this()
      {
      
        m_iD = iD;
      
      }

      


        public Task(
        int 
          iD,int? 
          parentTaskId,String 
          servmanOrderNum,int 
          projectId,int 
          taskTypeId,int? 
          taskStatusId,int? 
          taskFailTypeId,bool 
          isReady,String 
          number,int? 
          sequence,DateTime? 
          createDate,DateTime? 
          serviceDate,int? 
          durationMin,String 
          description,String 
          message,String 
          notes,String 
          failReason,bool 
          isSentToServman,decimal 
          closedAmount,bool 
          isClosedAmountAutoCalculated,decimal 
          estimatedClosedAmount,bool 
          isEstimatedClosedAmountAutoCalculated,bool 
          isReincluded,DateTime 
          modified,DateTime? 
          lastSyncDate,int? 
          dumpedTaskId,int? 
          dumpWorkTransactionId,int 
          failCount,DateTime? 
          lastFailDate,bool 
          isRugCleaningDepartment,int 
          discountPercentage,DateTime? 
          readyDate
        ) : this()
        {
        
          m_iD = iD;
        
          m_parentTaskId = parentTaskId;
        
          m_servmanOrderNum = servmanOrderNum;
        
          m_projectId = projectId;
        
          m_taskTypeId = taskTypeId;
        
          m_taskStatusId = taskStatusId;
        
          m_taskFailTypeId = taskFailTypeId;
        
          m_isReady = isReady;
        
          m_number = number;
        
          m_sequence = sequence;
        
          m_createDate = createDate;
        
          m_serviceDate = serviceDate;
        
          m_durationMin = durationMin;
        
          m_description = description;
        
          m_message = message;
        
          m_notes = notes;
        
          m_failReason = failReason;
        
          m_isSentToServman = isSentToServman;
        
          m_closedAmount = closedAmount;
        
          m_isClosedAmountAutoCalculated = isClosedAmountAutoCalculated;
        
          m_estimatedClosedAmount = estimatedClosedAmount;
        
          m_isEstimatedClosedAmountAutoCalculated = isEstimatedClosedAmountAutoCalculated;
        
          m_isReincluded = isReincluded;
        
          m_modified = modified;
        
          m_lastSyncDate = lastSyncDate;
        
          m_dumpedTaskId = dumpedTaskId;
        
          m_dumpWorkTransactionId = dumpWorkTransactionId;
        
          m_failCount = failCount;
        
          m_lastFailDate = lastFailDate;
        
          m_isRugCleaningDepartment = isRugCleaningDepartment;
        
          m_discountPercentage = discountPercentage;
        
          m_readyDate = readyDate;
        
        }


      
      #endregion

      
        [XmlElement]
        public int ID
        {
        get { return m_iD;}
        set { m_iD = value; }
        }
      
        [XmlElement]
        public int? ParentTaskId
        {
        get { return m_parentTaskId;}
        set { m_parentTaskId = value; }
        }
      
        [XmlElement]
        public String ServmanOrderNum
        {
        get { return m_servmanOrderNum;}
        set { m_servmanOrderNum = value; }
        }
      
        [XmlElement]
        public int ProjectId
        {
        get { return m_projectId;}
        set { m_projectId = value; }
        }
      
        [XmlElement]
        public int TaskTypeId
        {
        get { return m_taskTypeId;}
        set { m_taskTypeId = value; }
        }
      
        [XmlElement]
        public int? TaskStatusId
        {
        get { return m_taskStatusId;}
        set { m_taskStatusId = value; }
        }
      
        [XmlElement]
        public int? TaskFailTypeId
        {
        get { return m_taskFailTypeId;}
        set { m_taskFailTypeId = value; }
        }
      
        [XmlElement]
        public bool IsReady
        {
        get { return m_isReady;}
        set { m_isReady = value; }
        }
      
        [XmlElement]
        public String Number
        {
        get { return m_number;}
        set { m_number = value; }
        }
      
        [XmlElement]
        public int? Sequence
        {
        get { return m_sequence;}
        set { m_sequence = value; }
        }
      
        [XmlElement]
        public DateTime? CreateDate
        {
        get { return m_createDate;}
        set { m_createDate = value; }
        }
      
        [XmlElement]
        public DateTime? ServiceDate
        {
        get { return m_serviceDate;}
        set { m_serviceDate = value; }
        }
      
        [XmlElement]
        public int? DurationMin
        {
        get { return m_durationMin;}
        set { m_durationMin = value; }
        }
      
        [XmlElement]
        public String Description
        {
        get { return m_description;}
        set { m_description = value; }
        }
      
        [XmlElement]
        public String Message
        {
        get { return m_message;}
        set { m_message = value; }
        }
      
        [XmlElement]
        public String Notes
        {
        get { return m_notes;}
        set { m_notes = value; }
        }
      
        [XmlElement]
        public String FailReason
        {
        get { return m_failReason;}
        set { m_failReason = value; }
        }
      
        [XmlElement]
        public bool IsSentToServman
        {
        get { return m_isSentToServman;}
        set { m_isSentToServman = value; }
        }
      
        [XmlElement]
        public decimal ClosedAmount
        {
        get { return m_closedAmount;}
        set { m_closedAmount = value; }
        }
      
        [XmlElement]
        public bool IsClosedAmountAutoCalculated
        {
        get { return m_isClosedAmountAutoCalculated;}
        set { m_isClosedAmountAutoCalculated = value; }
        }
      
        [XmlElement]
        public decimal EstimatedClosedAmount
        {
        get { return m_estimatedClosedAmount;}
        set { m_estimatedClosedAmount = value; }
        }
      
        [XmlElement]
        public bool IsEstimatedClosedAmountAutoCalculated
        {
        get { return m_isEstimatedClosedAmountAutoCalculated;}
        set { m_isEstimatedClosedAmountAutoCalculated = value; }
        }
      
        [XmlElement]
        public bool IsReincluded
        {
        get { return m_isReincluded;}
        set { m_isReincluded = value; }
        }
      
        [XmlElement]
        public DateTime Modified
        {
        get { return m_modified;}
        set { m_modified = value; }
        }
      
        [XmlElement]
        public DateTime? LastSyncDate
        {
        get { return m_lastSyncDate;}
        set { m_lastSyncDate = value; }
        }
      
        [XmlElement]
        public int? DumpedTaskId
        {
        get { return m_dumpedTaskId;}
        set { m_dumpedTaskId = value; }
        }
      
        [XmlElement]
        public int? DumpWorkTransactionId
        {
        get { return m_dumpWorkTransactionId;}
        set { m_dumpWorkTransactionId = value; }
        }
      
        [XmlElement]
        public int FailCount
        {
        get { return m_failCount;}
        set { m_failCount = value; }
        }
      
        [XmlElement]
        public DateTime? LastFailDate
        {
        get { return m_lastFailDate;}
        set { m_lastFailDate = value; }
        }
      
        [XmlElement]
        public bool IsRugCleaningDepartment
        {
        get { return m_isRugCleaningDepartment;}
        set { m_isRugCleaningDepartment = value; }
        }
      
        [XmlElement]
        public int DiscountPercentage
        {
        get { return m_discountPercentage;}
        set { m_discountPercentage = value; }
        }
      
        [XmlElement]
        public DateTime? ReadyDate
        {
        get { return m_readyDate;}
        set { m_readyDate = value; }
        }
      

      public static int FieldsCount
      {
      get { return 32; }
      }


      public object Clone()
      {
      return MemberwiseClone();
      }

      }
      #endregion

      }

    