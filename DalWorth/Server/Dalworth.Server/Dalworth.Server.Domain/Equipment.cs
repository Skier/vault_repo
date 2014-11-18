using System;
using System.Collections.Generic;
using System.Data;
using System.Xml.Serialization;
using Dalworth.Server.Data;

namespace Dalworth.Server.Domain
{
    public enum EquipmentLocationTypeEnum
    {
        InventoryRoom = 1,
        Van = 2,
        Customer = 3,
        Lost = 4
    }

    public enum EquipmentIssueStatusEnum
    {
        All = 1,
        NotResolved = 2,
        Resolved = 3
    }

    public partial class Equipment
    {
        public Equipment(){ }

        #region EquipmentType

        [XmlIgnore]
        public EquipmentType EquipmentType
        {
            get
            {
                return EquipmentType.Instance.GetType(EquipmentTypeId);
            }

            set
            {
                EquipmentTypeId = value.ID;
            }
        }

        #endregion

        #region EquipmentTypeText

        [XmlIgnore]
        public string EquipmentTypeText
        {
            get { return EquipmentType.Type; }
        }

        #endregion

        #region EquipmentStatus

        [XmlIgnore]
        public EquipmentStatusEnum EquipmentStatus
        {
            get { return (EquipmentStatusEnum)m_equipmentStatusId; }
            set { m_equipmentStatusId = (int)value; }
        }

        #endregion

        #region EquipmentLocationType

        [XmlIgnore]
        public EquipmentLocationTypeEnum EquipmentLocationType
        {
            get
            {
                if (InventoryRoomId.HasValue)
                    return EquipmentLocationTypeEnum.InventoryRoom;
                if (VanId.HasValue)
                    return EquipmentLocationTypeEnum.Van;
                if (AddressId.HasValue)
                    return EquipmentLocationTypeEnum.Customer;

                return EquipmentLocationTypeEnum.Lost;                
            }
        }

        #endregion

        #region EquipmentStatusText

        [XmlIgnore]
        public string EquipmentStatusText
        {
            get { return Domain.EquipmentStatus.GetText(EquipmentStatus); }
        }

        #endregion

        #region FindEquipmentWrappers

        private const string SqlFindEquipmentWrappers =
            @"SELECT * FROM Equipment e
                left join InventoryRoom ir on e.InventoryRoomId = ir.ID
                left join Van v on e.VanId = v.ID
                left join Address a on e.AddressId = a.ID
                left join Customer c on c.AddressId = a.ID
                left join CustomerAddressAdditional caa on caa.AddressId = a.ID
                left join Customer c2 on c2.ID = caa.CustomerId
                left join Area air on ir.AreaId = air.ID
                left join Area av on v.AreaId = av.ID
                left join Area aa on a.AreaId = aa.ID";

        public static List<EquipmentWrapper> FindEquipmentWrappers()
        {
            List<EquipmentWrapper> result = new List<EquipmentWrapper>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindEquipmentWrappers))
            {

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        result.Add(EquipmentWrapper.Load(dataReader));
                    }                        

                }
            }

            return result;            
        }

        #endregion

        #region FindEquipmentHistoryWrappers

        private static string SqlFindEquipmentHistoryWrappers =
            @"SELECT etd.EquipmentId, e.EquipmentTypeId, etd.EquipmentStatusId, etd.InventoryRoomId, etd.VanId, etd.AddressId, e.SerialNumber,
                ir.*, v.*, a.*, c.*, caa.*, c2.*, air.*, av.*, aa.*
                ,et.*, wt.*, disp.*, tech.*, etd.*
                FROM equipmenttransactiondetail etd
                inner join equipment e on e.ID = etd.EquipmentId
                left join InventoryRoom ir on etd.InventoryRoomId = ir.ID
                left join Van v on etd.VanId = v.ID
                left join Address a on etd.AddressId = a.ID
                left join Customer c on c.AddressId = a.ID
                left join CustomerAddressAdditional caa on caa.AddressId = a.ID
                left join Customer c2 on c2.ID = caa.CustomerId
                left join Area air on ir.AreaId = air.ID
                left join Area av on v.AreaId = av.ID
                left join Area aa on a.AreaId = aa.ID

                inner join EquipmentTransaction et on et.ID = etd.EquipmentTransactionId
                left join WorkTransaction wt on wt.ID = et.WorkTransactionId
                inner join Employee disp on disp.ID = et.EmployeeId
                left join Work w on w.ID = wt.WorkId
                left join Employee tech on tech.ID = w.TechnicianEmployeeId

                where {0} 1=1
                order by et.SequnceDate desc, et.TransactionDate desc";

        public static List<EquipmentHistoryWrapper> FindEquipmentHistoryWrappers(DateTime? startDate, 
            IList<EquipmentWrapper> equipments)
        {
            string additionalConditions = string.Empty;

            if (startDate.HasValue)
                additionalConditions += "SequnceDate >= ?SequnceDate and ";

            if (equipments.Count != 0)
            {
                additionalConditions += "( ";

                foreach (EquipmentWrapper wrapper in equipments)
                {
                    additionalConditions += "etd.EquipmentId = ?EquipmentId" + wrapper.Equipment.ID 
                        + " or ";
                }
                additionalConditions = additionalConditions.Remove(additionalConditions.Length - 4);
                additionalConditions += ") and";
            }

            List<EquipmentHistoryWrapper> result = new List<EquipmentHistoryWrapper>();

            using (IDbCommand dbCommand = Database.PrepareCommand(
                string.Format(SqlFindEquipmentHistoryWrappers, additionalConditions)))
            {
                if (startDate.HasValue)
                    Database.PutParameter(dbCommand, "?SequnceDate", startDate.Value);

                foreach (EquipmentWrapper wrapper in equipments)
                {
                    Database.PutParameter(dbCommand, "?EquipmentId" + wrapper.Equipment.ID,
                        wrapper.Equipment.ID);
                }                

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        result.Add(EquipmentHistoryWrapper.Load(dataReader));
                    }

                }
            }

            return result;
        }

        #endregion

        #region FindEquipmentIssueWrappers

        private static string SqlFindEquipmentIssueWrappers =
            @"SELECT etd.EquipmentId, e.EquipmentTypeId, etd.EquipmentStatusId, etd.InventoryRoomId, etd.VanId, etd.AddressId, e.SerialNumber,
                ir.*, v.*, a.*, c.*, caa.*, c2.*, air.*, av.*, aa.*, et.*, wt.*, disp.*, tech.*, etd.*,
                prevState.EquipmentId, ePrev.EquipmentTypeId, prevState.EquipmentStatusId, prevState.InventoryRoomId, prevState.VanId, prevState.AddressId, ePrev.SerialNumber,
                irPrev.*, vPrev.*, aPrev.*, cPrev.*, caaPrev.*, c2Prev.*, airPrev.*, avPrev.*, aaPrev.*
              FROM EquipmentTransactionDetail etd
                inner join equipment e on e.ID = etd.EquipmentId
                left join InventoryRoom ir on etd.InventoryRoomId = ir.ID
                left join Van v on etd.VanId = v.ID
                left join Address a on etd.AddressId = a.ID
                left join Customer c on c.AddressId = a.ID
                left join CustomerAddressAdditional caa on caa.AddressId = a.ID
                left join Customer c2 on c2.ID = caa.CustomerId
                left join Area air on ir.AreaId = air.ID
                left join Area av on v.AreaId = av.ID
                left join Area aa on a.AreaId = aa.ID
                inner join EquipmentTransaction et on et.ID = etd.EquipmentTransactionId
                left join WorkTransaction wt on wt.ID = et.WorkTransactionId
                inner join Employee disp on disp.ID = et.EmployeeId
                left join Work w on w.ID = wt.WorkId
                left join Employee tech on tech.ID = w.TechnicianEmployeeId                
                left join Visit vs on vs.ID = wt.VisitId

                    
                inner join EquipmentTransactionDetailLink etdl on etdl.EquipmentTransactionDetailId = etd.ID
                inner join EquipmentTransactionDetail prevState on prevState.ID = etdl.PrevEquipmentTransactionDetailId
                inner join EquipmentTransaction prevEt on prevEt.ID = prevState.EquipmentTransactionId
                inner join equipment ePrev on ePrev.ID = prevState.EquipmentId
                left join InventoryRoom irPrev on prevState.InventoryRoomId = irPrev.ID
                left join Van vPrev on prevState.VanId = vPrev.ID
                left join Address aPrev on prevState.AddressId = aPrev.ID
                left join Customer cPrev on cPrev.AddressId = aPrev.ID
                left join CustomerAddressAdditional caaPrev on caaPrev.AddressId = aPrev.ID
                left join Customer c2Prev on c2Prev.ID = caaPrev.CustomerId
                left join Area airPrev on irPrev.AreaId = airPrev.ID
                left join Area avPrev on vPrev.AreaId = avPrev.ID
                left join Area aaPrev on aPrev.AreaId = aaPrev.ID

                where
                et.WorkTransactionId is not null
                {0}
                and (
                    (wt.WorkTransactionTypeId = 1 -- start day
                      and (etd.VanId is not null 
                           and (
                            (prevState.VanId is not null and etd.VanId != prevState.VanId and DATE(prevEt.SequnceDate) = DATE(et.SequnceDate))
                            or prevState.AddressId is not null
                            )
                           )
                    )
                    or (wt.WorkTransactionTypeId = 11 -- service visit
                      and (
                           (etd.InventoryRoomId is null and etd.VanId is null and etd.AddressId is null) -- Lost
                           or (etd.AddressId = vs.ServiceAddressId and (prevState.AddressId != vs.ServiceAddressId or prevState.AddressId is null) and (prevState.VanId != w.VanId or prevState.VanId is null)) -- unload to customer not from van
                           or (etd.VanId = w.VanId and (prevState.VanId != w.VanId or prevState.VanId is null) and (prevState.AddressId != vs.ServiceAddressId or prevState.AddressId is null)) -- pickup not from customer
                           )
                    )
                    )                    
                    order by et.SequnceDate desc, etd.ID desc";

        public static List<EquipmentIssueWrapper> FindEquipmentIssueWrappers(DateTime? startDate,
            EquipmentIssueStatusEnum issueStatus)
        {
            string additionalConditions = string.Empty;

            if (startDate.HasValue)
                additionalConditions += " and et.SequnceDate > ?SequnceDate";

            if (issueStatus == EquipmentIssueStatusEnum.Resolved)
                additionalConditions += " and etd.ResolvedByEmployeeId is not null";
            else if (issueStatus == EquipmentIssueStatusEnum.NotResolved)
                additionalConditions += " and etd.ResolvedByEmployeeId is null";

            List<EquipmentIssueWrapper> result = new List<EquipmentIssueWrapper>();

            using (IDbCommand dbCommand = Database.PrepareCommand(
                string.Format(SqlFindEquipmentIssueWrappers, additionalConditions)))
            {
                if (startDate.HasValue)
                    Database.PutParameter(dbCommand, "?SequnceDate", startDate.Value);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        result.Add(EquipmentIssueWrapper.Load(dataReader));
                    }

                }
            }

            return result;
        }

        #endregion

        #region ResolveEquipmentIssues

        private static string SqlResolveEquipmentIssues =
            @"update EquipmentTransactionDetail etd
                inner join EquipmentTransaction et on et.ID = etd.EquipmentTransactionId
                set etd.ResolvedByEmployeeId = ?ResolvedByEmployeeId
                where et.SequnceDate <= ?SequnceDate
                    and et.WorkTransactionId is not null
                    and etd.ResolvedByEmployeeId is null
                    and etd.EquipmentId = ?EquipmentId";

        public static void ResolveEquipmentIssues(List<EquipmentIssueWrapper> equipments, int dispatchId)
        {
            foreach (EquipmentIssueWrapper wrapper in equipments)
            {
                if (wrapper.IsResolved)
                    continue;

                using (IDbCommand dbCommand = Database.PrepareCommand(SqlResolveEquipmentIssues))
                {
                    Database.PutParameter(dbCommand, "?ResolvedByEmployeeId", dispatchId);
                    Database.PutParameter(dbCommand, "?SequnceDate", wrapper.EquipmentTransaction.SequnceDate);
                    Database.PutParameter(dbCommand, "?EquipmentId", wrapper.Equipment.ID);

                    dbCommand.ExecuteNonQuery();
                }                
            }
        }

        #endregion

        #region FindBy Number and Type

        private const string SqlFindNumberAndType =
            @"select * from Equipment
                where EquipmentTypeId = ?EquipmentTypeId
                    and SerialNumber = ?SerialNumber";

        public static Equipment FindBy(EquipmentType type, string number, IDbConnection connection)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindNumberAndType, connection))
            {
                Database.PutParameter(dbCommand, "?EquipmentTypeId", type.ID);
                Database.PutParameter(dbCommand, "?SerialNumber", number);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                        return Load(dataReader);
                }
            }

            throw new DataNotFoundException("Equipment not found");
        }

        public static Equipment FindBy(EquipmentType type, string number)
        {
            return FindBy(type, number, null);
        }

        #endregion

        #region FindBy Number

        private const string SqlFindByNumber =
            @"select * from Equipment
                where SerialNumber = ?SerialNumber";

        public static Equipment FindBy(string serialNumber, IDbConnection connection)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByNumber, connection))
            {
                Database.PutParameter(dbCommand, "?SerialNumber", serialNumber);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                        return Load(dataReader);
                }
            }

            throw new DataNotFoundException("Equipment not found");
        }

        public static Equipment FindBy(string serialNumber)
        {
            return FindBy(serialNumber, null);
        }

        #endregion

        #region FindBy Serial Numbers

        public static List<Equipment> FindBy(List<string> serialNumbers, IDbConnection connection)
        {
            List<Equipment> result = new List<Equipment>();

            string SqlFindBySerialNumbers =
                @"select * from Equipment
                    where SerialNumber in (";

            if (serialNumbers.Count == 0)
                return result;

            foreach (string serialNumber in serialNumbers)
                SqlFindBySerialNumbers += "'" + serialNumber + "', ";
            SqlFindBySerialNumbers = SqlFindBySerialNumbers.Remove(SqlFindBySerialNumbers.Length - 2, 2);
            SqlFindBySerialNumbers += ")";

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindBySerialNumbers, connection))
            {
                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                        result.Add(Load(dataReader));
                }
            }

            return result;
        }

        public static List<Equipment> FindBy(List<string> serialNumbers)
        {
            return FindBy(serialNumbers, null);
        }

        #endregion

        #region IsExistTransactionAfter

        private const string SqlIsExistTransactionAfter =
            @"SELECT EXISTS (SELECT * FROM equipmenttransactiondetail etd
                inner join EquipmentTransaction et on et.ID = etd.EquipmentTransactionId
              where EquipmentId = ?EquipmentId
                and SequnceDate > ?SequnceDate)";


        public static bool IsExistTransactionAfter(Equipment equipment, DateTime date, IDbConnection connection)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlIsExistTransactionAfter, connection))
            {
                Database.PutParameter(dbCommand, "?EquipmentId", equipment.ID);
                Database.PutParameter(dbCommand, "?SequnceDate", date);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                        return dataReader.GetBoolean(0);
                }
            }

            throw new Exception("IsExistTransactionAfter method failed");
        }

        public static bool IsExistTransactionAfter(Equipment equipment, DateTime date)
        {
            return IsExistTransactionAfter(equipment, date, null);
        }

        #endregion

        #region DeleteTransactional

        public static void DeleteTransactional(EquipmentTransaction equipmentTransaction)
        {
            List<EquipmentTransactionDetail> details
                = EquipmentTransactionDetail.FindByTransaction(equipmentTransaction);
            EquipmentTransactionDetail.DeleteByTransaction(equipmentTransaction);
            EquipmentTransaction.Delete(equipmentTransaction);

            foreach (EquipmentTransactionDetail detail in details)
            {
                EquipmentTransactionDetail lastDetail = EquipmentTransactionDetail.FindLastBeforeDate(
                    detail.EquipmentId, equipmentTransaction.SequnceDate);

                Equipment originalItem = FindByPrimaryKey(lastDetail.EquipmentId);

                Equipment equipment = new Equipment(
                    lastDetail.EquipmentId,
                    originalItem.EquipmentTypeId,
                    lastDetail.EquipmentStatusId,
                    lastDetail.InventoryRoomId,
                    lastDetail.VanId,
                    lastDetail.AddressId,
                    originalItem.SerialNumber);

                ProcessDelayedEquipmentTransaction(equipment, 
                                                   lastDetail.EquipmentTransaction.SequnceDate, null);
            }
        }

        #endregion

        #region UpdateTransactional

        //updates equipment creating EquipmentTransaction
        //Sequence date - if null reatime, if not delayed

        public static void UpdateTransactional(int dispatchId, WorkTransaction workTransaction, 
            List<Equipment> equipments, string notes, DateTime? sequenceDate, IDbConnection connection)
        {
            DateTime virtualDate;
            DateTime transactionDate = DateTime.Now;
            if (sequenceDate == null)
                virtualDate = transactionDate;
            else
                virtualDate = sequenceDate.Value;

            EquipmentTransaction transaction = new EquipmentTransaction(0, null,
                dispatchId, virtualDate, transactionDate, notes);
            if (workTransaction != null)
                transaction.WorkTransactionId = workTransaction.ID;
            EquipmentTransaction.Insert(transaction, connection);

            foreach (Equipment equipment in equipments)
            {
                EquipmentTransactionDetail detail = new EquipmentTransactionDetail(0,
                    transaction.ID,
                    equipment.ID,
                    equipment.EquipmentStatusId,
                    equipment.InventoryRoomId,
                    equipment.VanId,
                    equipment.AddressId, null, false);
                EquipmentTransactionDetail.Insert(detail, connection);             

                if (sequenceDate == null)
                {                    
                    Update(equipment, connection);
                } else
                {
                    ProcessDelayedEquipmentTransaction(equipment, sequenceDate.Value, connection);                            
                }                                                
            }                            
        }

        private static void ProcessDelayedEquipmentTransaction(Equipment equipment, DateTime sequenceDate, 
            IDbConnection connection)
        {
            if (equipment.AddressId == null)
            {
                EquipmentTransaction nextLostTransactoin
                    = EquipmentTransaction.FindNextLostTransactionOnWork(equipment,
                        sequenceDate, connection);

                if (nextLostTransactoin != null)
                {
                    if (!HadEquipmentBeInTransaction(nextLostTransactoin,
                        equipment, connection))
                    {
                        EquipmentTransactionDetail detailToDelete
                            = EquipmentTransactionDetail.FindByTransaction(
                            nextLostTransactoin, equipment, connection);
                        EquipmentTransactionDetail.Delete(detailToDelete, connection);
                    }
                }
            }
            
            if (equipment.VanId == null)
            {
                EquipmentTransaction nextUnloadOnStartDay
                    = EquipmentTransaction.FindNextUnloadTransactionOnStartDay(equipment,
                        sequenceDate, connection);                

                if (nextUnloadOnStartDay != null)
                {
                    if (!HadEquipmentBeInTransaction(nextUnloadOnStartDay,
                        equipment, connection))
                    {
                        EquipmentTransactionDetail detailToDelete
                            = EquipmentTransactionDetail.FindByTransaction(
                                nextUnloadOnStartDay, equipment, connection);
                        EquipmentTransactionDetail.Delete(detailToDelete, connection);
                    }                    
                }
            }

            if (equipment.AddressId != null)
            {
                EquipmentTransaction nextCustomerServiceVisitTransactoin
                    = EquipmentTransaction.FindNextCustomerServiceVisitTransaction(
                        equipment.AddressId.Value, sequenceDate, connection);

                if (nextCustomerServiceVisitTransactoin != null)
                {
                    if (!EquipmentTransaction.IsEquipmentParticipatedInTransaction(
                        nextCustomerServiceVisitTransactoin, equipment, connection))
                    {
                        if (HadEquipmentBeOnCustomerInTransaction(equipment.AddressId.Value,
                            nextCustomerServiceVisitTransactoin, equipment, null))
                        {
                            EquipmentTransactionDetail autoLostDetail = new EquipmentTransactionDetail(
                                0,
                                nextCustomerServiceVisitTransactoin.ID,
                                equipment.ID,
                                equipment.EquipmentStatusId,
                                null, null, null, null, true);
                            EquipmentTransactionDetail.Insert(autoLostDetail, connection);

                            Equipment lostEquipment = new Equipment(equipment.ID,
                                equipment.EquipmentTypeId,
                                equipment.EquipmentStatusId,
                                null, null, null,
                                equipment.SerialNumber);

                            if (!IsExistTransactionAfter(lostEquipment,
                                nextCustomerServiceVisitTransactoin.SequnceDate, connection))
                            {
                                Update(lostEquipment, connection);
                            }

                        }
                    } 
                }
            }


            if (equipment.VanId != null)
            {
                Van van = new Van(equipment.VanId.Value);

                EquipmentTransaction nextVanStartDayTransactoin
                    = EquipmentTransaction.FindNextVanStartDayTransaction(
                        van, sequenceDate, connection);

                if (nextVanStartDayTransactoin != null)
                {
                    if (!EquipmentTransaction.IsEquipmentParticipatedInTransaction(
                            nextVanStartDayTransactoin, equipment, connection))
                    {
                        if (HadEquipmentBeOnVanInTransaction(van, nextVanStartDayTransactoin,
                            equipment, connection))
                        {
                            EquipmentTransactionDetail autoUnloadDetail = new EquipmentTransactionDetail(
                                0,
                                nextVanStartDayTransactoin.ID,
                                equipment.ID,
                                equipment.EquipmentStatusId,
                                1, //default inventory room
                                null, null, null, true);
                            EquipmentTransactionDetail.Insert(autoUnloadDetail, connection);

                            Equipment unloadEquipment = new Equipment(equipment.ID, 
                                equipment.EquipmentTypeId,
                                equipment.EquipmentStatusId,
                                1, null, null,
                                equipment.SerialNumber);

                            if (!IsExistTransactionAfter(unloadEquipment, 
                                nextVanStartDayTransactoin.SequnceDate, connection))
                            {
                                Update(unloadEquipment, connection);                  
                            }                                
                        }
                    }
                }
            }            

            if (!IsExistTransactionAfter(equipment, sequenceDate, connection))
                Update(equipment, connection);                  
        }

        public static void UpdateTransactional(int dispatchId, WorkTransaction workTransaction,
            List<Equipment> equipments, string notes, DateTime? sequenceDate)
        {
            UpdateTransactional(dispatchId, workTransaction, equipments, notes, sequenceDate, null);
        }

        public static void UpdateTransactional(int dispatchId, WorkTransaction workTransaction,
            Equipment equipment, string notes, DateTime? sequenceDate)
        {
            List<Equipment> equipments = new List<Equipment>();
            equipments.Add(equipment);
            UpdateTransactional(dispatchId, workTransaction, equipments, notes, sequenceDate, null);
        }

        #endregion

        #region FindOnVan

        private const string SqlFindOnVanRealtime =
            @"select * from Equipment
                where VanId = ?VanId";

        private const string SqlFindOnVanHistorical =
            @"select e.ID, e.EquipmentTypeId, InventoryOnDate.EquipmentStatusId,
                InventoryOnDate.InventoryRoomId, InventoryOnDate.VanId,
                InventoryOnDate.AddressId, e.SerialNumber
              from
              (
                SELECT etd.* FROM equipmenttransactiondetail etd
                inner join EquipmentTransaction et on et.ID = etd.EquipmentTransactionId
                inner join
                (
                    SELECT EquipmentId, Max(SequnceDate) SequnceDateMax FROM equipmenttransactiondetail etd2
                    inner join EquipmentTransaction et2 on et2.ID = etd2.EquipmentTransactionId
                    where SequnceDate <= ?SequnceDate and et2.ID != ?IgnoredEquipmentTransactionId
                    group by EquipmentId
                ) temp on temp.EquipmentId = etd.EquipmentId and temp.SequnceDateMax = et.SequnceDate
                  where et.ID != ?IgnoredEquipmentTransactionId
                  group by etd.EquipmentId
              ) InventoryOnDate
                inner join Equipment e on InventoryOnDate.EquipmentId = e.ID
                    where InventoryOnDate.VanId = ?VanId";


        //if date = null it is realtime, else for specified date
        public static List<Equipment> FindOnVan(Van van, DateTime? date, 
                       int? ignoredEquipmentTransactionId, IDbConnection connection)
        {
            List<Equipment> result = new List<Equipment>();

            using (IDbCommand dbCommand = Database.PrepareCommand(
                date.HasValue ? SqlFindOnVanHistorical : SqlFindOnVanRealtime, connection))
            {
                if (date.HasValue)
                    Database.PutParameter(dbCommand, "?SequnceDate", date.Value);
                Database.PutParameter(dbCommand, "?VanId", van.ID);
                Database.PutParameter(dbCommand, "?IgnoredEquipmentTransactionId",
                    ignoredEquipmentTransactionId.HasValue ? ignoredEquipmentTransactionId.Value : 0);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                        result.Add(Load(dataReader));
                }
            }

            return result;
        }

        public static List<Equipment> FindOnVan(Van van, DateTime? date, int? ignoredEquipmentTransactionId)
        {
            return FindOnVan(van, date, ignoredEquipmentTransactionId, null);
        }

        #endregion

        #region FindOnInventoryRoom

        private const string SqlFindOnInventoryRoom =
            @"select * from Equipment
                where InventoryRoomId = ?InventoryRoomId";
        
        public static List<Equipment> FindOnInventoryRoom(InventoryRoom inventoryRoom)
        {
            List<Equipment> result = new List<Equipment>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindOnInventoryRoom))
            {
                Database.PutParameter(dbCommand, "?InventoryRoomId", inventoryRoom.ID);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                        result.Add(Load(dataReader));
                }
            }

            return result;
        }

        #endregion

        #region FindLost

        private const string SqlFindLost =
            @"select * from Equipment
                where InventoryRoomId is null
                    and VanId is null
                    and AddressId is null";

        public static List<Equipment> FindLost()
        {
            List<Equipment> result = new List<Equipment>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindLost))
            {
                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                        result.Add(Load(dataReader));
                }
            }

            return result;
        }

        #endregion

        #region HadEquipmentBeOnVanInTransaction

        public static bool HadEquipmentBeOnVanInTransaction(Van van, EquipmentTransaction transaction, 
                                                            Equipment equipment, IDbConnection connection)
        {
            List<Equipment> vanEquipments = FindOnVan(van, transaction.SequnceDate, null, connection);
            foreach (Equipment vanEquipment in vanEquipments)
            {
                if (vanEquipment.ID == equipment.ID)
                    return true;
            }
            return false;
        }

        #endregion

        #region FindOnCustomerSite

        private const string SqlFindOnCustomerSiteRealtime =
            @"select * from Equipment
                where AddressId = ?AddressId";

        private const string SqlFindOnCustomerSiteHistorical =
            @"select e.ID, e.EquipmentTypeId, InventoryOnDate.EquipmentStatusId,
                InventoryOnDate.InventoryRoomId, InventoryOnDate.VanId,
                InventoryOnDate.AddressId, e.SerialNumber
              from
              (
                SELECT etd.* FROM equipmenttransactiondetail etd
                inner join EquipmentTransaction et on et.ID = etd.EquipmentTransactionId
                inner join
                (
                    SELECT EquipmentId, Max(SequnceDate) SequnceDateMax FROM equipmenttransactiondetail etd2
                    inner join EquipmentTransaction et2 on et2.ID = etd2.EquipmentTransactionId
                    where SequnceDate <= ?SequnceDate and et2.ID != ?IgnoredEquipmentTransactionId
                    group by EquipmentId
                ) temp on temp.EquipmentId = etd.EquipmentId and temp.SequnceDateMax = et.SequnceDate
                  where et.ID != ?IgnoredEquipmentTransactionId
                  group by etd.EquipmentId
              ) InventoryOnDate
                inner join Equipment e on InventoryOnDate.EquipmentId = e.ID
                    where InventoryOnDate.AddressId = ?AddressId";


        public static List<Equipment> FindOnCustomerSite(int customerAddressId, DateTime? date, 
            int? ignoredEquipmentTransactionId, IDbConnection connection)
        {
            List<Equipment> result = new List<Equipment>();

            using (IDbCommand dbCommand = Database.PrepareCommand(
                date.HasValue ? SqlFindOnCustomerSiteHistorical : SqlFindOnCustomerSiteRealtime, connection))
            {
                if (date.HasValue)
                    Database.PutParameter(dbCommand, "?SequnceDate", date.Value);
                Database.PutParameter(dbCommand, "?AddressId", customerAddressId);
                Database.PutParameter(dbCommand, "?IgnoredEquipmentTransactionId",
                    ignoredEquipmentTransactionId.HasValue ? ignoredEquipmentTransactionId.Value : 0);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                        result.Add(Load(dataReader));
                }
            }

            return result;
        }

        public static List<Equipment> FindOnCustomerSite(int customerAddressId, DateTime? date, 
            int? ignoredEquipmentTransactionId)
        {
            return FindOnCustomerSite(customerAddressId, date, ignoredEquipmentTransactionId, null);
        }

        #endregion        

        #region HadEquipmentBeOnCustomerInTransaction

        public static bool HadEquipmentBeOnCustomerInTransaction(int addressId, 
            EquipmentTransaction transaction, Equipment equipment, IDbConnection connection)
        {
            return HadEquipmentBeOnCustomerInTransaction(addressId, transaction, equipment, null, null);
        }

        public static bool HadEquipmentBeOnCustomerInTransaction(int addressId, EquipmentTransaction transaction,
            Equipment equipment, int? ignoredEquipmentTransactionId, IDbConnection connection)
        {
            List<Equipment> customerEquipments = FindOnCustomerSite(addressId, 
                transaction.SequnceDate, ignoredEquipmentTransactionId, connection);
            foreach (Equipment customerEquipment in customerEquipments)
            {
                if (customerEquipment.ID == equipment.ID)
                    return true;
            }
            return false;
        }


        #endregion        

        #region HadEquipmentBeInTransaction

        public static bool HadEquipmentBeInTransaction(EquipmentTransaction transaction,
                                            Equipment equipment, IDbConnection connection)
        {
            if (transaction.WorkTransactionId.HasValue)
            {
                WorkTransaction workTransaction 
                    = WorkTransaction.FindByPrimaryKey(transaction.WorkTransactionId.Value, connection);

                if (workTransaction.WorkTransactionType == WorkTransactionTypeEnum.StartDayDone)
                {
                    Work work = Work.FindByPrimaryKey(workTransaction.WorkId, connection);
                    Van van = new Van(work.VanId.Value);
                    return HadEquipmentBeOnVanInTransaction(van, transaction, equipment, connection);
                }

                if (workTransaction.WorkTransactionType == WorkTransactionTypeEnum.VisitEquipmentTransfer)
                {
                    Visit visit = Visit.FindByPrimaryKey(workTransaction.VisitId.Value);
                    return HadEquipmentBeOnCustomerInTransaction(visit.ServiceAddressId.Value, 
                        transaction, equipment, connection);
                }                    
            }

            return false;
        }

        #endregion

        #region FindOnDate

        private const string SqlFindFindOnDateHistorical =
            @"select e.ID, e.EquipmentTypeId, InventoryOnDate.EquipmentStatusId,
                InventoryOnDate.InventoryRoomId, InventoryOnDate.VanId,
                InventoryOnDate.AddressId, e.SerialNumber
              from
              (
                SELECT etd.* FROM equipmenttransactiondetail etd
                inner join EquipmentTransaction et on et.ID = etd.EquipmentTransactionId
                inner join
                (
                    SELECT EquipmentId, Max(SequnceDate) SequnceDateMax FROM equipmenttransactiondetail etd2
                    inner join EquipmentTransaction et2 on et2.ID = etd2.EquipmentTransactionId
                    where SequnceDate <= ?SequnceDate and et2.ID != ?IgnoredEquipmentTransactionId
                    group by EquipmentId
                ) temp on temp.EquipmentId = etd.EquipmentId and temp.SequnceDateMax = et.SequnceDate
                  where et.ID != ?IgnoredEquipmentTransactionId
                  group by etd.EquipmentId
              ) InventoryOnDate
                inner join Equipment e on InventoryOnDate.EquipmentId = e.ID
                    where e.SerialNumber = ?SerialNumber";


        public static Equipment FindOnDate(string serialNumber, DateTime? date, 
            int? ignoredEquipmentTransactionId)
        {
            if (date == null)
                return FindBy(serialNumber);

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindFindOnDateHistorical))
            {
                Database.PutParameter(dbCommand, "?SequnceDate", date.Value);
                Database.PutParameter(dbCommand, "?SerialNumber", serialNumber);
                Database.PutParameter(dbCommand, "?IgnoredEquipmentTransactionId",
                    ignoredEquipmentTransactionId.HasValue ? ignoredEquipmentTransactionId.Value : 0);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                        return Load(dataReader);
                }
            }

            throw new DataNotFoundException("FindOnDate failed");
        }

        #endregion        

        #region GetCollectionSerialNumbers

        public static List<string> GetCollectionSerialNumbers(IList<EquipmentNumber> equipmentNumbers)
        {
            List<string> result = new List<string>();

            foreach (EquipmentNumber number in equipmentNumbers)
            {
                if (number.SerialNumber1 != null && number.SerialNumber1 != string.Empty)
                    result.Add(number.SerialNumber1);

                if (number.SerialNumber2 != null && number.SerialNumber2 != string.Empty)
                    result.Add(number.SerialNumber2);

                if (number.SerialNumber3 != null && number.SerialNumber3 != string.Empty)
                    result.Add(number.SerialNumber3);

                if (number.SerialNumber4 != null && number.SerialNumber4 != string.Empty)
                    result.Add(number.SerialNumber4);
            }

            return result;
        }

        #endregion

        #region GetEquipmentNumbers

        public static List<EquipmentNumber> GetEquipmentNumbers(List<Equipment> equipments)
        {
            List<EquipmentNumber> result = new List<EquipmentNumber>();

            foreach (Equipment equipment in equipments)
            {
                List<EquipmentNumber> notFullyFilled = result.FindAll(Match);

                EquipmentNumber currentRow = null;

                foreach (EquipmentNumber number in notFullyFilled)
                {
                    if (number.EquipmentType.Equals(equipment.EquipmentType))
                    {
                        currentRow = number;
                        break;
                    }                        
                }
                
                if (currentRow == null)
                {
                    currentRow = new EquipmentNumber(equipment.EquipmentType);
                    result.Add(currentRow);
                }

                currentRow.PutIntoFirstEmptyCell(equipment.SerialNumber);                
            }

            return result;
        }

        private static bool Match(EquipmentNumber obj)
        {
            return !obj.IsFilled;
        }

        #endregion

        #region FindVanEquipmentQuantities

        private const string SqlFindVanEquipmentQuantities =
            @"select ID, Quantity from EquipmentType et
                left join
                (SELECT EquipmentTypeId, count(*) Quantity FROM equipment e
                where VanId = ?VanId
                group by EquipmentTypeId) temp on et.ID = temp.EquipmentTypeId
                order by ID";

        //key - EquipmentTypeId, value - quantity
        public static Dictionary<int, int> FindVanEquipmentQuantities(Van van, DateTime? date)
        {
            Dictionary<int, int> result = new Dictionary<int, int>();

            if (date != null)
            {
                List<Equipment> equipments = FindOnVan(van, date, null);
                List<EquipmentType> equipmentTypes = EquipmentType.Find();

                foreach (EquipmentType type in equipmentTypes)
                {
                    int typeQuantity = 0;
                    foreach (Equipment equipment in equipments)
                    {
                        if (equipment.EquipmentType.Equals(type))
                            typeQuantity++;
                    }

                    result.Add(type.ID, typeQuantity);
                }

                return result;
            }
           
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindVanEquipmentQuantities))
            {
                Database.PutParameter(dbCommand, "?VanId", van.ID);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        result.Add(dataReader.GetInt32(0),
                            dataReader.IsDBNull(1) ? 0 : dataReader.GetInt32(1));
                    }
                        
                }
            }

            return result;
            
        }

        #endregion

        #region FindByTransaction

        private const string SqlFindByTransaction =
            @"SELECT e.ID, e.EquipmentTypeId, etd.EquipmentStatusId,
                     etd.InventoryRoomId, etd.VanId, etd.AddressId, e.SerialNumber
              FROM EquipmentTransactionDetail etd
                inner join Equipment e on e.ID = etd.EquipmentId
                where EquipmentTransactionId = ?EquipmentTransactionId";

        public static List<Equipment> FindByTransaction(EquipmentTransaction transaction)
        {
            List<Equipment> result = new List<Equipment>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByTransaction))
            {
                Database.PutParameter(dbCommand, "?EquipmentTransactionId", transaction.ID);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                        result.Add(Load(dataReader));
                }
            }

            return result;
        }

        #endregion
    }

    public class EquipmentWrapper : ICloneable
    {
        private Domain.Equipment m_equipment;
        private Area m_area;
        private Customer m_customer;

        private InventoryRoom m_inventoryRoom;
        private Van m_van;
        private Address m_address;

        #region Constructors

        public EquipmentWrapper(Equipment equipment, Area area, Customer customer, string location,
            InventoryRoom inventoryRoom, Van van, Address address)
        {
            m_equipment = equipment;
            m_area = area;
            m_customer = customer;
            m_location = location;
            m_inventoryRoom = inventoryRoom;
            m_van = van;
            m_address = address;
        }

        public EquipmentWrapper(Equipment equipment, Area area, Customer customer, string location) 
            : this(equipment, area, customer, location, null, null, null) {}

        public EquipmentWrapper(Equipment equipment) : this(equipment, null, null, string.Empty) { }        

        #endregion

        #region Load

        public static EquipmentWrapper Load(IDataReader dataReader)
        {
            return Load(dataReader, 0);
        }

        public static EquipmentWrapper Load(IDataReader dataReader, int offset)
        {
            Equipment equipment = Equipment.Load(dataReader, offset);
            string location = string.Empty;
            Area area = null;
            Customer customer = null;

            InventoryRoom inventoryRoom = null;
            Van van = null;
            Address address = null;

            if (equipment.InventoryRoomId.HasValue)
            {
                inventoryRoom
                    = InventoryRoom.Load(dataReader, Equipment.FieldsCount + offset);
                location = inventoryRoom.Name;

                area = Area.Load(dataReader, Equipment.FieldsCount + InventoryRoom.FieldsCount
                                             + Van.FieldsCount + Address.FieldsCount
                                             + Customer.FieldsCount + CustomerAddressAdditional.FieldsCount
                                             + Customer.FieldsCount + offset);
            }
            else if (equipment.VanId.HasValue)
            {
                van = Van.Load(dataReader, Equipment.FieldsCount + InventoryRoom.FieldsCount + offset);
                location = van.LicensePlateNumber;

                area = Area.Load(dataReader, Equipment.FieldsCount + InventoryRoom.FieldsCount
                                             + Van.FieldsCount + Address.FieldsCount
                                             + Customer.FieldsCount + CustomerAddressAdditional.FieldsCount
                                             + Customer.FieldsCount + Area.FieldsCount + offset);

            }
            else if (equipment.AddressId.HasValue)
            {
                address
                    = Address.Load(dataReader, Equipment.FieldsCount
                                               + InventoryRoom.FieldsCount + Van.FieldsCount + offset);

                if (address.AreaId.HasValue)
                {
                    area = Area.Load(dataReader, Equipment.FieldsCount + InventoryRoom.FieldsCount
                                                 + Van.FieldsCount + Address.FieldsCount
                                                 + Customer.FieldsCount + CustomerAddressAdditional.FieldsCount
                                                 + Customer.FieldsCount + Area.FieldsCount + Area.FieldsCount + offset);
                }

                if (!dataReader.IsDBNull(Equipment.FieldsCount + InventoryRoom.FieldsCount
                                         + Van.FieldsCount + Address.FieldsCount + offset))
                {
                    //we have found primary address
                    customer
                        = Customer.Load(dataReader, Equipment.FieldsCount + InventoryRoom.FieldsCount
                                                    + Van.FieldsCount + Address.FieldsCount + offset);
                }
                else
                {
                    //one of alternative addresses
                    customer
                        = Customer.Load(dataReader, Equipment.FieldsCount + InventoryRoom.FieldsCount
                                                    + Van.FieldsCount + Address.FieldsCount
                                                    + Customer.FieldsCount + CustomerAddressAdditional.FieldsCount + offset);
                }

                location = address.AddressFirstLine
                           + "\n" + address.AddressSecondLine;
            }

            return new EquipmentWrapper(equipment, area, customer, location, inventoryRoom, van, address);
        }

        #endregion

        #region FieldsCount

        public static int FieldsCount
        {
            get
            {
                return Equipment.FieldsCount + InventoryRoom.FieldsCount
                       + Van.FieldsCount + Address.FieldsCount
                       + Customer.FieldsCount + CustomerAddressAdditional.FieldsCount
                       + Customer.FieldsCount + 3*Area.FieldsCount;
            }
        }

        #endregion

        #region InventoryRoom

        public InventoryRoom InventoryRoom
        {
            get { return m_inventoryRoom; }
        }

        #endregion

        #region Van

        public Van Van
        {
            get { return m_van; }
        }

        #endregion

        #region Address

        public Address Address
        {
            get { return m_address; }
        }

        #endregion

        #region Equipment

        public Equipment Equipment
        {
            get { return m_equipment; }
        }

        #endregion

        #region Area

        public Area Area
        {
            get { return m_area; }
        }

        #endregion

        #region Customer

        public Customer Customer
        {
            get { return m_customer; }
        }

        #endregion

        #region SerialNumber

        public string SerialNumber
        {
            get { return m_equipment.SerialNumber; }
        }

        #endregion

        #region TypeId

        public int TypeId
        {
            get { return m_equipment.EquipmentTypeId; }
        }

        #endregion

        #region TypeText

        public string TypeText
        {
            get { return m_equipment.EquipmentType.Type; }
        }

        #endregion

        #region StatusId

        public int StatusId
        {
            get { return m_equipment.EquipmentStatusId; }
            set { m_equipment.EquipmentStatusId = value; }
        }

        #endregion        

        #region AreaId

        public int? AreaId
        {
            get
            {                
                return m_area == null ? null : (int?)m_area.ID;
            }
        }

        #endregion

        #region AreaText

        public string AreaText
        {
            get { return m_area == null ? string.Empty : m_area.Name; }
        }

        #endregion

        #region CustomerName

        public string CustomerName
        {
            get
            {
                return m_customer == null ? string.Empty : m_customer.DisplayName;
            }
        }

        #endregion

        #region LocationTypeId

        public int LocationTypeId
        {
            get { return (int)m_equipment.EquipmentLocationType; }
        }

        #endregion

        #region Location

        private string m_location;
        public string Location
        {
            get { return m_location; }
        }

        #endregion

        #region LocationOneLine

        public string LocationOneLine
        {
            get { return m_location.Replace("\n", "; "); }
        }

        #endregion


        #region LocationGroupHeader

        public string LocationGroupHeader
        {
            get
            {
                if (m_equipment.EquipmentLocationType == EquipmentLocationTypeEnum.Lost)
                    return "Lost";
                if (m_equipment.EquipmentLocationType == EquipmentLocationTypeEnum.Van)
                    return "Van - " + m_location;
                if (m_equipment.EquipmentLocationType == EquipmentLocationTypeEnum.Customer)
                    return CustomerName + " - " + LocationOneLine;

                return LocationOneLine;
            }
        }

        #endregion

        #region LocationAllInOne

        public string LocationAllInOne
        {
            get
            {
                string location = string.Empty;

                if (m_equipment.EquipmentLocationType == EquipmentLocationTypeEnum.Customer)
                    location += CustomerName + "\n" + LocationOneLine;
                else if (m_equipment.EquipmentLocationType == EquipmentLocationTypeEnum.Lost)
                    location += "Lost";
                else if (m_equipment.EquipmentLocationType == EquipmentLocationTypeEnum.Van)
                    location += "Van - " + m_location;
                else if (m_equipment.EquipmentLocationType == EquipmentLocationTypeEnum.InventoryRoom)
                    location += "Inventory Room - " + m_location;

                return location;
            }
        }

        #endregion

        #region Equals and HashCode

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            EquipmentWrapper equipmentWrapper = obj as EquipmentWrapper;
            if (equipmentWrapper == null) return false;
            return Equals(m_equipment.ID, equipmentWrapper.m_equipment.ID);
        }

        public override int GetHashCode()
        {
            return m_equipment.ID.GetHashCode();
        }

        #endregion

        #region Clone

        public object Clone()
        {
            return new EquipmentWrapper(
                (Equipment)m_equipment.Clone(), 
                m_area == null ? null : (Area)m_area.Clone(), 
                m_customer == null? null : (Customer)m_customer.Clone(), 
                m_location);
        }

        #endregion
    }

    public class EquipmentHistoryWrapper : EquipmentWrapper {

        private EquipmentTransaction m_equipmentTransaction;
        private EquipmentTransactionDetail m_equipmentTransactionDetail;
        private WorkTransaction m_workTransaction;
        private Employee m_dispatch;
        private Employee m_technician;

        #region Own data classes

        public EquipmentTransaction EquipmentTransaction
        {
            get { return m_equipmentTransaction; }
        }

        public EquipmentTransactionDetail EquipmentTransactionDetail
        {
            get { return m_equipmentTransactionDetail; }
        }

        public WorkTransaction WorkTransaction
        {
            get { return m_workTransaction; }
        }

        public Employee Dispatch
        {
            get { return m_dispatch; }
        }

        public Employee Technician
        {
            get { return m_technician; }
        }

        #endregion

        #region Constructor

        public EquipmentHistoryWrapper(EquipmentWrapper equipmentWrapper, EquipmentTransaction equipmentTransaction,
            WorkTransaction workTransaction, Employee dispatch, Employee technician, 
            EquipmentTransactionDetail equipmentTransactionDetail)
            : base(equipmentWrapper.Equipment, equipmentWrapper.Area,
                equipmentWrapper.Customer, equipmentWrapper.Location)
        {
            m_equipmentTransaction = equipmentTransaction;
            m_workTransaction = workTransaction;
            m_dispatch = dispatch;
            m_technician = technician;
            m_equipmentTransactionDetail = equipmentTransactionDetail;
        }

        #endregion

        #region Load

        public static new EquipmentHistoryWrapper Load(IDataReader dataReader)
        {
            EquipmentWrapper equipmentWrapper = EquipmentWrapper.Load(dataReader);
            
            EquipmentTransaction equipmentTransaction
                = EquipmentTransaction.Load(dataReader, EquipmentWrapper.FieldsCount);

            WorkTransaction workTransaction = null;
            if (equipmentTransaction.WorkTransactionId.HasValue)
            {
                workTransaction = WorkTransaction.Load(dataReader, EquipmentWrapper.FieldsCount +
                    EquipmentTransaction.FieldsCount);
            }

            Employee dispatch = Employee.Load(dataReader, EquipmentWrapper.FieldsCount +
                EquipmentTransaction.FieldsCount + WorkTransaction.FieldsCount);

            Employee technician = null;
            if (workTransaction != null)
            {
                technician = Employee.Load(dataReader, EquipmentWrapper.FieldsCount +
                   EquipmentTransaction.FieldsCount + WorkTransaction.FieldsCount +
                   Employee.FieldsCount);
            }

            EquipmentTransactionDetail detail = EquipmentTransactionDetail.Load(dataReader,
                EquipmentWrapper.FieldsCount + EquipmentTransaction.FieldsCount
                + WorkTransaction.FieldsCount + 2*Employee.FieldsCount);

            return new EquipmentHistoryWrapper(equipmentWrapper,
                equipmentTransaction, workTransaction, dispatch, technician, detail);
        }

        #endregion

        #region FieldsCount

        public static new int FieldsCount
        {
            get
            {
                return EquipmentWrapper.FieldsCount 
                    + EquipmentTransaction.FieldsCount + WorkTransaction.FieldsCount +
                    2 * Employee.FieldsCount + EquipmentTransactionDetail.FieldsCount;
            }
        }

        #endregion

        #region SequenceDate

        public DateTime SequenceDate
        {
            get { return m_equipmentTransaction.SequnceDate; }
        }

        #endregion

        #region TransactionTypeText

        public string TransactionTypeText
        {
            get
            {
                if (m_workTransaction == null)
                    return "Standalone";
                if (m_workTransaction.WorkTransactionType == WorkTransactionTypeEnum.StartDayDone)
                    return "Start Day";
                if (m_workTransaction.WorkTransactionType == WorkTransactionTypeEnum.Completed)
                    return "End Day";
                if (m_workTransaction.WorkTransactionType == WorkTransactionTypeEnum.VisitEquipmentTransfer)
                    return "Service Visit";
                return string.Empty;
            }
        }

        #endregion

        #region SerialNumberAndEquipmentType

        public string SerialNumberAndEquipmentType
        {
            get
            {
                return SerialNumber + "/" + TypeText;
            }
        }

        #endregion

        #region DispatchName

        public string DispatchName
        {
            get { return m_dispatch.DisplayName; }
        }

        #endregion

        #region TechnicianName

        public string TechnicianName
        {
            get
            {
                if (m_technician != null)
                    return m_technician.DisplayName;
                return string.Empty;
            }
        }

        #endregion

        #region Location

        public new string Location
        {

            get { return base.Location.Replace("\n", "; "); }
        }

        #endregion
    }

    public class EquipmentIssueWrapper : EquipmentHistoryWrapper
    {
        private EquipmentWrapper m_previousState;
        private bool m_hasUnresolvedIssues;

        #region Constructor

        public EquipmentIssueWrapper(EquipmentHistoryWrapper equipmentHistoryWrapper, 
              EquipmentWrapper previousState)
            : base(equipmentHistoryWrapper, equipmentHistoryWrapper.EquipmentTransaction,
                equipmentHistoryWrapper.WorkTransaction, equipmentHistoryWrapper.Dispatch,
                equipmentHistoryWrapper.Technician, equipmentHistoryWrapper.EquipmentTransactionDetail)
        {
            m_previousState = previousState;
        }

        #endregion

        #region Load

        public static new EquipmentIssueWrapper Load(IDataReader dataReader)
        {
            EquipmentHistoryWrapper equipmentHistoryWrapper = EquipmentHistoryWrapper.Load(dataReader);
            EquipmentWrapper previousWrapper = EquipmentWrapper.Load(dataReader, 
                EquipmentHistoryWrapper.FieldsCount);

            return new EquipmentIssueWrapper(equipmentHistoryWrapper, previousWrapper);
        }

        #endregion

        #region CurrentLocation

        public string CurrentLocation
        {
            get
            {
                return LocationAllInOne;
            }
        }

        #endregion

        #region PreviousLocation

        public string PreviousLocation
        {
            get
            {
                return m_previousState.LocationAllInOne;
            }
        }

        #endregion

        #region Issue

        public string Issue
        {
            get
            {
                if (WorkTransaction.WorkTransactionType == WorkTransactionTypeEnum.StartDayDone)
                {
                    if (m_previousState.Van != null)
                        return "Load from another Van";

                    if (m_previousState.Customer != null)
                        return "Load from Customer site";
                }
                    

                if (WorkTransaction.WorkTransactionType == WorkTransactionTypeEnum.VisitEquipmentTransfer)
                {
                    if (Equipment.InventoryRoomId == null && Equipment.VanId == null && Equipment.AddressId == null)
                        return "Equipment is lost";
                    if (Equipment.AddressId != null)
                        return "Drop off not from Van";
                    if (Equipment.VanId != null)
                        return "Pickup not from Customer site";
                } 

                return string.Empty;
            }
        }

        #endregion

        #region IsResolved

        public bool IsResolved
        {
            get
            {
                return EquipmentTransactionDetail.ResolvedByEmployeeId.HasValue;
            }
        }

        #endregion

        #region ResolvedStatusId

        public int ResolvedStatusId
        {
            get
            {
                if (IsResolved)
                    return 1;
                return 0;
            }
        }

        #endregion

        #region HasUnresolvedIssues

        public bool HasUnresolvedIssues
        {
            get { return m_hasUnresolvedIssues; }
            set { m_hasUnresolvedIssues = value; }
        }

        #endregion
    }

    public class EquipmentNumber
    {
        private string m_serialNumber1;
        private string m_serialNumber2;
        private string m_serialNumber3;
        private string m_serialNumber4;
        private EquipmentType m_equipmentType;

        #region Constructor

        public EquipmentNumber(EquipmentType equipmentType)
        {            
            m_equipmentType = equipmentType;
            m_serialNumber1 = string.Empty;
            m_serialNumber2 = string.Empty;
            m_serialNumber3 = string.Empty;
            m_serialNumber4 = string.Empty;
        }

        #endregion

        #region EquipmentType

        public EquipmentType EquipmentType
        {
            get { return m_equipmentType; }
        }

        #endregion

        #region EquipmentTypeName

        public string EquipmentTypeName
        {
            get { return m_equipmentType.Type; }
        }

        #endregion        

        #region Serial Numbers

        public string SerialNumber1
        {
            get { return m_serialNumber1; }
            set { m_serialNumber1 = value; }
        }

        public string SerialNumber2
        {
            get { return m_serialNumber2; }
            set { m_serialNumber2 = value; }
        }

        public string SerialNumber3
        {
            get { return m_serialNumber3; }
            set { m_serialNumber3 = value; }
        }

        public string SerialNumber4
        {
            get { return m_serialNumber4; }
            set { m_serialNumber4 = value; }
        }

        #endregion

        #region IsFilled

        public bool IsFilled
        {
            get
            {
                if (SerialNumber1 != null && SerialNumber1 != string.Empty
                    && SerialNumber2 != null && SerialNumber2 != string.Empty
                    && SerialNumber3 != null && SerialNumber3 != string.Empty
                    && SerialNumber4 != null && SerialNumber4 != string.Empty)
                {
                    return true;
                }

                return false;
            }
        }

        #endregion

        #region PutIntoFirstEmptyCell

        public void PutIntoFirstEmptyCell(string serialNumber)
        {
            if (IsFilled)
                throw new Exception("All cells are filled");

            if (SerialNumber1 == null || SerialNumber1 == string.Empty)
                SerialNumber1 = serialNumber;
            else if (SerialNumber2 == null || SerialNumber2 == string.Empty)
                SerialNumber2 = serialNumber;
            else if (SerialNumber3 == null || SerialNumber3 == string.Empty)
                SerialNumber3 = serialNumber;
            else if (SerialNumber4 == null || SerialNumber4 == string.Empty)
                SerialNumber4 = serialNumber;
        }

        #endregion

        #region InsertEquipmentIntoCollection

        public static void InsertEquipmentIntoCollection(IList<EquipmentNumber> collection,
                EquipmentTransactionDetail detail, Dictionary<int, Equipment> equipmentMap)
        {
            Equipment baseEquipment = equipmentMap[detail.EquipmentId];

            foreach (EquipmentNumber number in collection)
            {
                if (!number.EquipmentType.Equals(baseEquipment.EquipmentType))
                    continue;

                if (number.IsFilled)
                    continue;

                number.PutIntoFirstEmptyCell(baseEquipment.SerialNumber);

                if (number.SerialNumber3 != string.Empty && number.SerialNumber4 == string.Empty)
                    collection.Add(new EquipmentNumber(baseEquipment.EquipmentType));

                return;
            }
        }

        #endregion        
    }
}
      