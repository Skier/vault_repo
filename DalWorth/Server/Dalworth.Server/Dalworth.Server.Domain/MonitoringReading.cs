using System;
using System.Collections.Generic;
using System.Data;
using System.Xml.Serialization;
using Dalworth.Server.Data;

namespace Dalworth.Server.Domain
{
    public partial class MonitoringReading
    {
        public MonitoringReading(){ }

        #region MonitoringReadingType

        [XmlIgnore]
        public MonitoringReadingTypeEnum MonitoringReadingType
        {
            get { return (MonitoringReadingTypeEnum)m_monitoringReadingTypeId; }
            set { m_monitoringReadingTypeId = (int)value; }
        }

        #endregion

        #region MonitoringReadingTypeText

        [XmlIgnore]
        public string MonitoringReadingTypeText
        {
            get
            {
                string result = Domain.MonitoringReadingType.GetText(MonitoringReadingType);

                if ((MonitoringReadingType == MonitoringReadingTypeEnum.Dehumidifier
                    || MonitoringReadingType == MonitoringReadingTypeEnum.ACUnit)
                    && EquipmentSerialNumber != null
                    && EquipmentSerialNumber != string.Empty)
                {
                    result += " - " + EquipmentSerialNumber;
                }

                return result;
            }
        }

        #endregion

        #region IsRemoveAllowed

        private bool m_isRemoveAllowed;
        public bool IsRemoveAllowed
        {
            get
            {
                if (MonitoringReadingType == MonitoringReadingTypeEnum.Inside
                    || MonitoringReadingType == MonitoringReadingTypeEnum.Outside
                    || MonitoringReadingType == MonitoringReadingTypeEnum.Unaffected)
                {
                    return false;
                }

                return m_isRemoveAllowed;
            }
            set { m_isRemoveAllowed = value; }
        }

        #endregion

        #region PreviousTemperature

        private decimal m_previousTemperature;       
        public decimal PreviousTemperature
        {
            get { return m_previousTemperature; }
            set { m_previousTemperature = value; }
        }

        #endregion

        #region PreviousRelativeHumidity

        private decimal m_previousRelativeHumidity;
        public decimal PreviousRelativeHumidity
        {
            get { return m_previousRelativeHumidity; }
            set { m_previousRelativeHumidity = value; }
        }

        #endregion

        #region PreviousGpp

        private decimal m_previousGpp;
        public decimal PreviousGpp
        {
            get { return m_previousGpp; }
            set { m_previousGpp = value; }
        }

        #endregion

        #region PreviousReadingText

        public string PreviousReadingText
        {
            get
            {
                return GetReadingText(m_previousTemperature, m_previousRelativeHumidity, m_previousGpp);
            }
        }

        #endregion

        #region ReadingText

        private string GetReadingText(decimal temperature, decimal relativeHumidity, decimal gpp)
        {
            if (temperature == decimal.Zero && relativeHumidity == decimal.Zero && gpp == decimal.Zero)
                return string.Empty;

            return string.Format("{0}/{1}-{2}", Utils.RemoveTrailingZeros(temperature),
                Utils.RemoveTrailingZeros(relativeHumidity), Utils.RemoveTrailingZeros(gpp));            
        }

        public string ReadingText
        {
            get
            {
                return GetReadingText(m_temperature, m_relativeHumidity, m_gpp);
            }
            set
            {
                if (value == string.Empty)
                {
                    m_temperature = decimal.Zero;
                    m_relativeHumidity = decimal.Zero;
                    m_gpp = decimal.Zero;
                    return;
                }

                m_temperature = int.Parse(value.Substring(0, value.IndexOf('/')));
                string relHumAndGpp = value.Substring(value.IndexOf('/') + 1);
                m_relativeHumidity = int.Parse(relHumAndGpp.Substring(0, relHumAndGpp.IndexOf('-')));
                m_gpp = int.Parse(value.Substring(value.IndexOf('-') + 1));
            }
        }

        #endregion

        #region DefaultReadings

        public static List<MonitoringReading> DefaultReadings
        {
            get
            {
                List<MonitoringReading> readings = new List<MonitoringReading>();
                MonitoringReading reading;

                reading = new MonitoringReading();
                reading.MonitoringReadingType = MonitoringReadingTypeEnum.Outside;
                readings.Add(reading);
                reading = new MonitoringReading();
                reading.MonitoringReadingType = MonitoringReadingTypeEnum.Inside;
                readings.Add(reading);
                reading = new MonitoringReading();
                reading.MonitoringReadingType = MonitoringReadingTypeEnum.Unaffected;
                readings.Add(reading);
                reading = new MonitoringReading();
                reading.MonitoringReadingType = MonitoringReadingTypeEnum.Dehumidifier;
                reading.IsRemoveAllowed = true;
                readings.Add(reading);
                reading = new MonitoringReading();
                reading.MonitoringReadingType = MonitoringReadingTypeEnum.ACUnit;
                reading.IsRemoveAllowed = true;
                readings.Add(reading);

                return readings;
            }
        }

        #endregion                     

        #region FindBy Task

        private const string SqlFindByTask =
            @"SELECT *
            FROM MonitoringReading
                WHERE MonitoringTaskId = ?MonitoringTaskId";

        public static List<MonitoringReading> FindBy(Task task, IDbConnection connection)
        {
            List<MonitoringReading> result = new List<MonitoringReading>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByTask, connection))
            {
                Database.PutParameter(dbCommand, "?MonitoringTaskId", task.ID);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                        result.Add(Load(dataReader));
                }
            }

            return result;
        }

        #endregion        

        #region FindBy Deflood

        private const string SqlFindByDeflood =
            @"SELECT *
              FROM MonitoringReading
            where MonitoringTaskId in (
            select ID from Task
            where TaskTypeId = 5 and DumpedTaskId is null and TaskStatusId = 2 
                and ParentTaskId = ?DefloodTaskId)
            order by MonitoringTaskId, ID";

        public static List<MonitoringReading> FindByDeflood(Task defloodTask)
        {
            List<MonitoringReading> result = new List<MonitoringReading>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByDeflood))
            {
                Database.PutParameter(dbCommand, "?DefloodTaskId", defloodTask.ID);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                        result.Add(Load(dataReader));
                }
            }

            return result;
        }

        #endregion

        #region FindByDefloodGrouped

        //key - task id
        public static Dictionary<int, List<MonitoringReading>> FindByDefloodGrouped(Task defloodTask)
        {
            List<MonitoringReading> readings = FindByDeflood(defloodTask);
            Dictionary<int, List<MonitoringReading>> result = new Dictionary<int, List<MonitoringReading>>();

            foreach (MonitoringReading reading in readings)
            {
                if (!result.ContainsKey(reading.MonitoringTaskId))
                    result.Add(reading.MonitoringTaskId, new List<MonitoringReading>());

                result[reading.MonitoringTaskId].Add(reading);
            }

            return result;
        }

        #endregion

        #region DeleteBy Task

        private const string SqlDeleteByTask =
            @"DELETE
            FROM MonitoringReading
                WHERE MonitoringTaskId = ?MonitoringTaskId";

        public static void DeleteBy(Task task, IDbConnection connection)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlDeleteByTask, connection))
            {
                Database.PutParameter(dbCommand, "?MonitoringTaskId", task.ID);
                dbCommand.ExecuteNonQuery();
            }
        }

        #endregion        

        #region FindPreviousMonitoringReadings

        private const string SqlFindPreviousMonitoringReadings =
            @"SELECT * FROM MonitoringReading
                WHERE MonitoringTaskId =
                    (SELECT ID FROM task t
                    WHERE TaskTypeId = 5 AND ParentTaskId = ?DefloodTaskId
                    AND ID < ?CurrentMonitoringId AND DumpedTaskId is null
                    ORDER BY ID desc
                    LIMIT 1)";


        public static List<MonitoringReading> FindPreviousMonitoringReadings(Task monitoringTask)
        {
            List<MonitoringReading> readings = new List<MonitoringReading>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindPreviousMonitoringReadings))
            {
                Database.PutParameter(dbCommand, "?DefloodTaskId", monitoringTask.ParentTaskId.Value);
                Database.PutParameter(dbCommand, "?CurrentMonitoringId", monitoringTask.ID);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                        readings.Add(Load(dataReader));
                }
            }
            return readings;
        }

        #endregion

        #region IsValid

        public bool IsValid
        {
            get
            {
                if ((m_temperature == decimal.Zero || m_relativeHumidity == decimal.Zero
                     || m_gpp == decimal.Zero)
                    && (MonitoringReadingType == MonitoringReadingTypeEnum.Inside
                        || MonitoringReadingType == MonitoringReadingTypeEnum.Outside))
                {
                    return false;
                }
                return true;
            }
        }

        #endregion

    }

    public class ReadingHistory
    {
        #region Constructor

        public ReadingHistory(DateTime readingDate, Employee technician, 
                              List<MonitoringReading> readings)
        {
            m_readingDate = readingDate;
            m_technician = technician;
            m_readings = readings;
        }

        #endregion

        #region ReadingDate

        private DateTime m_readingDate;
        public DateTime ReadingDate
        {
            get { return m_readingDate; }
            set { m_readingDate = value; }
        }

        #endregion

        #region Technician

        private Employee m_technician;
        public Employee Technician
        {
            get { return m_technician; }
            set { m_technician = value; }
        }

        #endregion

        #region TechnicianName

        public string TechnicianName
        {
            get
            {
                return m_technician.DisplayName;
            }
        }

        #endregion


        #region Readings

        private List<MonitoringReading> m_readings;
        public List<MonitoringReading> Readings
        {
            get { return m_readings; }
            set { m_readings = value; }
        }

        #endregion

        #region Find

        private const string SqlFind =
            @"select e.*, wd.*, wtt.* from WorkTransactionTask wtt
            inner join WorkTransaction wt on wt.ID = wtt.WorkTransactionId
            inner join Work w on w.ID = wt.WorkId
            inner join Employee e on e.ID = w.TechnicianEmployeeId
            inner join WorkDetail wd on wd.WorkId = w.ID and wd.VisitId = wt.VisitId
            where WorkTransactionTaskActionId = 1
            and TaskId in (
            select ID from task
            where TaskTypeId = 5 and DumpedTaskId is null and TaskStatusId = 2 and ParentTaskId = ?DefloodTaskId)
            order by wd.TimeComplete";

        public static List<ReadingHistory> Find(Task defloodTask)
        {
            List<ReadingHistory> result = new List<ReadingHistory>();

            Dictionary<int, List<MonitoringReading>> readingsMap
                = MonitoringReading.FindByDefloodGrouped(defloodTask);

            if (readingsMap.Count == 0)
                return result;

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFind))
            {
                Database.PutParameter(dbCommand, "?DefloodTaskId", defloodTask.ID);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        Employee technician = Employee.Load(dataReader);
                        WorkDetail workDetail = WorkDetail.Load(dataReader, Employee.FieldsCount);
                        WorkTransactionTask workTransactionTask = WorkTransactionTask.Load(
                            dataReader, Employee.FieldsCount + WorkDetail.FieldsCount);

                        ReadingHistory readingHeader = new ReadingHistory(workDetail.TimeComplete.Value,
                            technician, readingsMap[workTransactionTask.TaskId]);
                        result.Add(readingHeader);
                    }                        
                }
            }
            
            return result;            
        }

        #endregion
    }
}
      