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
    public partial class QbItem
    {
        public QbItem()
        {

        }

        #region FindByTaskType

        private const String SqlFindByTaskType = SqlSelectAll +
            @"join tasktypeqbItem on tasktypeqbItem.QbItemListId = qbItem.ListId
              where tasktypeqbItem.TaskTypeId = ?TaskTypeId";

        public static List<QbItem> FindByTaskType(TaskTypeEnum taskType, IDbConnection connection)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByTaskType, connection))
            {
                Database.PutParameter(dbCommand, "?TaskTypeId", (int)taskType);

                List<QbItem> rv = new List<QbItem>();
                

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        rv.Add(Load(dataReader));
                    }

                }

                return rv;
            }

        }

        #endregion

        #region FindByProjectType

        private const String SqlFindByProjectType = SqlSelectAll +
            @" join projecttypeqbitem  on qbItem.listid = projecttypeqbitem.qbitemlistid
                where projecttypeqbitem.projecttypeid = ?ProjectTypeId";

        public static List<QbItem> FindByProjectType(ProjectTypeEnum projectType, IDbConnection connection)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByProjectType, connection))
            {
                Database.PutParameter(dbCommand, "?ProjectTypeId", (int)projectType);

                List<QbItem> rv = new List<QbItem>();


                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        rv.Add(Load(dataReader));
                    }

                }

                return rv;
            }
        }

        #endregion 

        #region FindByQbItemType

        private const String SqlFindTaxItems = SqlSelectAll +
            @"where qbitemtypeId = ?QbItemTypeId";

        public static List<QbItem> FindByQbItemType(QbItemTypeEnum qbItemType, IDbConnection connection)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindTaxItems, connection))
            {
                Database.PutParameter(dbCommand, "?QbItemTypeId", (int)qbItemType);

                List<QbItem> rv = new List<QbItem>();

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        rv.Add(Load(dataReader));
                    }

                }

                return rv;
            }
        }
            
        #endregion 

    }

    public class QbItemRugCleaningCatalog
    {
        #region QbItemRubCleaningDiscount

        private static QbItem m_qbItemDiscount;
        public static QbItem QbItemDiscount 
        {
            get { return m_qbItemDiscount; }
        }

        #endregion

        #region QbItemRugCleaning

        private static QbItem m_qbItemRugCleaning;
        public static QbItem QbItemRugCleaning
        {
            get { return m_qbItemRugCleaning; }
        }

        #endregion 

        #region QbItemRugCleaningFlood

        private static QbItem m_qbItemRugCleaningFlood;
        public static QbItem QbItemRugCleaningFlood
        {
            get { return m_qbItemRugCleaningFlood; }
        }

        #endregion 

        #region QBItemRugCleaningPad

        private static QbItem m_qbItemRugCleaningPad;
        public static QbItem QbItemRugCleaningPad
        {
            get { return m_qbItemRugCleaningPad; }
        }

        #endregion 

        #region QbItemProtectant

        private static QbItem m_qbItemProtectant;
        public static QbItem QbItemProtectant
        {
            get { return m_qbItemProtectant; }
        }

        #endregion 

        #region QbItemMonth

        private static QbItem m_qbItemMoth;
        public static QbItem QbItemMoth
        {
            get { return m_qbItemMoth; }
        }

        #endregion 

        #region QbItemWrap

        private static QbItem m_qbItemWrap;
        public static QbItem QbItemWrap
        {
            get { return m_qbItemWrap; }
        }

        #endregion 

        #region QbItemOtherRevenue

        private static QbItem m_qbItemOtherRevenue;
        public static QbItem QbItemOtherRevenue
        {
            get { return m_qbItemOtherRevenue; }
        }

        #endregion 

        #region QbItemStorage 

        private static QbItem m_qbItemStorage;
        public static QbItem QbItemStorage
        {
            get { return m_qbItemStorage; }
        }
        #endregion 

        #region QbItemMinimumCharge

        private static QbItem m_qbItemMinimumCharge;
        public static QbItem QbItemMinimumCharge
        {
            get { return m_qbItemMinimumCharge; }
        }

        #endregion 

        #region QbItemTax 

        private static QbItem m_qbItemSalesTax;
        public static QbItem QbItemSalesTax
        {
            get { return m_qbItemSalesTax; }
        }
        
        #endregion 
    
        #region QbItemRugCleaningCatalog

        private QbItemRugCleaningCatalog() { }

        #endregion

        public static void Init()
        {
            if (m_qbItemRugCleaning != null)
                return;

            m_qbItemRugCleaning = QbItem.FindByPrimaryKey(Configuration.QuickBooksItemRugCleaningCostListId);
            m_qbItemRugCleaningFlood = QbItem.FindByPrimaryKey(Configuration.QuickBooksItemRugCleaningCostFloodListId);
            m_qbItemRugCleaningPad = QbItem.FindByPrimaryKey(Configuration.QuickBooksItemRugCleaningPadListId);
            m_qbItemProtectant = QbItem.FindByPrimaryKey(Configuration.QuickBooksItemRugCleaningProtectantListId);
            m_qbItemMoth = QbItem.FindByPrimaryKey(Configuration.QuickBooksItemRugCleaningMothListId);
            m_qbItemWrap = QbItem.FindByPrimaryKey(Configuration.QuickBooksItemRugCleaningWrapListId);
            m_qbItemOtherRevenue = QbItem.FindByPrimaryKey(Configuration.QuickBooksItemRugCleaningRevenueListId);
            m_qbItemMinimumCharge = QbItem.FindByPrimaryKey(Configuration.QuickBooksItemRugCleaningMinimumChargeListId);
            m_qbItemStorage = QbItem.FindByPrimaryKey(Configuration.QuickBooksItemRugCleaningStorageListId);
            m_qbItemSalesTax = QbItem.FindByPrimaryKey(Configuration.QuickBooksItemTaxRateListId);
            m_qbItemDiscount = QbItem.FindByPrimaryKey(Configuration.QuickBooksItemRugCleaningDiscountListId);
        }

        public static decimal GetDefaultCleaningRate(bool isPartOfFlood)
        {
           if (isPartOfFlood)
               return m_qbItemRugCleaningFlood.Price;
           else
               return m_qbItemRugCleaning.Price;
        }

        public static decimal MinimumCharge
        {
            get { return m_qbItemMinimumCharge!=null? m_qbItemMinimumCharge.Price:0; }
        }

        public static decimal ProtectorPricePerSqFoot
        {
            get { return m_qbItemProtectant!=null? m_qbItemProtectant.Price:0; }
        }

        public static decimal MothRepellantPricePerSqFoot
        {
            get { return m_qbItemMoth!=null?m_qbItemMoth.Price:0; }
        }

        public static decimal PadPricePerSqFoot
        {
            get { return m_qbItemRugCleaningPad!=null? m_qbItemRugCleaningPad.Price:0; }
        }

        public static decimal WrapPricePerSqFoot
        {
            get { return m_qbItemWrap!=null? m_qbItemWrap.Price:0; }
        }

        public static decimal TaxRate
        {
            get { return m_qbItemSalesTax!=null? m_qbItemSalesTax.TaxRate:0; }
        }
    }

    public class QbItemDefloodCatalog
    {
        #region Constructor

        private QbItemDefloodCatalog() { }

        #endregion

        #region QbItemDefloodRevenue

        private static QbItem m_qbItemDefloodRevenue;
        public static QbItem QbItemDefloodRevenue
        {
            get { return m_qbItemDefloodRevenue; }
        }

        #endregion

        #region QbItemTax

        private static QbItem m_qbItemSalesTax;
        public static QbItem QbItemSalesTax
        {
            get { return m_qbItemSalesTax; }
        }

        #endregion 

        #region Init

        public static void Init()
        {
            m_qbItemDefloodRevenue = QbItem.FindByPrimaryKey(Configuration.QuickBooksItemDefloodRevenueListId);
            m_qbItemSalesTax = QbItem.FindByPrimaryKey(Configuration.QuickBooksItemTaxRateListId);
        }

        #endregion
    }
}
      