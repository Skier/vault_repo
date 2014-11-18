using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Dalworth.Server.Data;

namespace Dalworth.Server.Domain
{
    public partial class EquipmentTransaction
    {
        public EquipmentTransaction(){ }     

        #region FindByWorkTransaction

        private const string SqlFindByWorkTransaction =
            @"SELECT * FROM EquipmentTransaction 
            where WorkTransactionId = ?WorkTransactionId
            limit 1";

        public static EquipmentTransaction FindByWorkTransaction(WorkTransaction workTransaction)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByWorkTransaction))
            {
                Database.PutParameter(dbCommand, "?WorkTransactionId", workTransaction.ID);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                        return Load(dataReader);
                }
            }

            throw new DataNotFoundException("EquipmentTransaction not found");
        }


        #endregion

        #region DeleteTransactional

        public static void DeleteTransactional(EquipmentTransaction transaction)
        {
            WorkTransaction workTransaction = null;
            if (transaction.WorkTransactionId.HasValue)
                workTransaction = WorkTransaction.FindByPrimaryKey(transaction.WorkTransactionId.Value);
            List<EquipmentTransactionDetail> deletedDetails 
                = EquipmentTransactionDetail.FindByTransaction(transaction);
            EquipmentTransactionDetail.DeleteByTransaction(transaction);
            Delete(transaction);

            if (workTransaction != null 
                && workTransaction.WorkTransactionType == WorkTransactionTypeEnum.VisitEquipmentTransfer)
            {
                int? addressId = null;

                foreach (EquipmentTransactionDetail deletedDetail in deletedDetails)
                {
                    if (deletedDetail.AddressId.HasValue)
                    {
                        addressId = deletedDetail.AddressId.Value;
                        break;                        
                    }
                }

                if (!addressId.HasValue)
                    throw new DataNotFoundException("Address equipment details not found");
                List<EquipmentTransactionDetail> equipmentOnDate =
                    EquipmentTransactionDetail.FindOnDate(transaction.SequenceDate, null, addressId.Value, null);

                List<EquipmentTransactionDetail> futureCustomerDetails = EquipmentTransactionDetail.FindBy(
                    transaction.SequenceDate, DateTime.Now, null, addressId, null);

                AdjustFutureEquipment(equipmentOnDate, futureCustomerDetails);
            }
        }


        #endregion

        #region InsertTransactional

        public static void InsertTransactional(EquipmentTransaction transaction, List<EquipmentTransactionDetail> details,
            WorkTransaction workTransaction)
        {
            int? vanId = null;
            int? addressId = null;

            foreach (EquipmentTransactionDetail detail in details)
            {
                if (workTransaction.WorkTransactionType == WorkTransactionTypeEnum.StartDayDone
                    && detail.VanId.HasValue)
                {
                    vanId = detail.VanId;
                    break;
                }

                if (workTransaction.WorkTransactionType == WorkTransactionTypeEnum.VisitEquipmentTransfer
                    && detail.AddressId.HasValue)
                {
                    addressId = detail.AddressId;
                    break;
                }
            }

            while (EquipmentTransactionDetail.IsTransactionExist(
                transaction.SequenceDate, vanId, addressId))
            {
                transaction.SequenceDate = transaction.SequenceDate.AddSeconds(1);
            }
            Insert(transaction);

            List<EquipmentTransactionDetail> currentCustomerEquipmentQuantity = new List<EquipmentTransactionDetail>();
            foreach (EquipmentTransactionDetail transactionDetail in details)
            {
                transactionDetail.EquipmentTransactionId = transaction.ID;
                if (transactionDetail.AddressId.HasValue)
                    currentCustomerEquipmentQuantity.Add(transactionDetail);
            }                
            EquipmentTransactionDetail.Insert(details);

            if (workTransaction != null)
            {
                if (workTransaction.WorkTransactionType == WorkTransactionTypeEnum.StartDayDone)
                {
                    List<EquipmentTransactionDetail> dayVanDetails = EquipmentTransactionDetail.FindBy(
                        transaction.SequenceDate.Date, transaction.SequenceDate.Date.AddHours(23).AddMinutes(59),
                        details[0].VanId, null, transaction.ID);
                    AdjustFutureEquipment(details, dayVanDetails);
                } 
                else if (workTransaction.WorkTransactionType == WorkTransactionTypeEnum.VisitEquipmentTransfer)
                {
                    if (!addressId.HasValue)
                        throw new DataNotFoundException("Customer site EquipmentTransactionDetail not found");

                    List<EquipmentTransactionDetail> futureCustomerDetails = EquipmentTransactionDetail.FindBy(
                        transaction.SequenceDate, DateTime.Now, null, addressId, transaction.ID);
                    AdjustFutureEquipment(currentCustomerEquipmentQuantity, futureCustomerDetails);
                }
            }
        }


        #endregion

        #region AdjustFutureEquipment

        private static void AdjustFutureEquipment(List<EquipmentTransactionDetail> currentEquipmentQuantities,
            List<EquipmentTransactionDetail> detailsToAdjust)
        {
            Dictionary<int, int> equipmentQuantities = new Dictionary<int, int>();
            foreach (EquipmentTransactionDetail currentEquipment in currentEquipmentQuantities)
                equipmentQuantities.Add(currentEquipment.EquipmentTypeId, currentEquipment.Quantity);

            foreach (EquipmentTransactionDetail detail in detailsToAdjust)
            {
                if (detail.WorkTransaction.WorkTransactionType == WorkTransactionTypeEnum.StartDayDone)
                    continue;

                int equipmentTypeId = detail.EquipmentTypeId;
                equipmentQuantities[equipmentTypeId] += detail.QuantityChange;
                detail.Quantity = equipmentQuantities[equipmentTypeId];
                EquipmentTransactionDetail.Update(detail);
            }
        }

        #endregion
    }
}


      