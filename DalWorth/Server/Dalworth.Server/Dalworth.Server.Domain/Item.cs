using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using Dalworth.Server.Data;

namespace Dalworth.Server.Domain
{    

    public partial class Item
    {
        public const double PI = 3.14;

        public Item(){ }

        #region GetDefaultCleaningRate

        public static decimal GetDefaultCleaningRate(bool isPartOfFlood)
        {
            return QbItemRugCleaningCatalog.GetDefaultCleaningRate(isPartOfFlood);
        }

        #endregion

        #region ItemShape

        [XmlIgnore]
        public ItemShapeEnum? ItemShape
        {
            get { return (ItemShapeEnum?) m_itemShapeId; }
            set { m_itemShapeId = (int?) value; }
        }

        #endregion

        #region ItemType

        [XmlIgnore]
        public ItemTypeEnum ItemType
        {
            get { return (ItemTypeEnum)m_itemTypeId; }
            set { m_itemTypeId = (int)value; }
        }

        #endregion

        #region ItemShortSpec

        public string ItemShortSpec
        {
            get
            {
                if (ItemShape == ItemShapeEnum.Rectangle)
                {


                    return "Rect, " + Utils.RemoveTrailingZeros(Width)
                           + "x" + Utils.RemoveTrailingZeros(Height)
                           + ", " + (Width * Height).ToString("0.00")
                           + "SF";
                }
                else if (ItemShape == ItemShapeEnum.Round)
                {
                    return "Round, D" + Utils.RemoveTrailingZeros(Diameter)
                           + ", " + ((decimal.ToDouble(Diameter * Diameter) * PI) / 4).ToString("0.00")
                           + "SF";
                }
                return string.Empty;
            }
        }

        #endregion

        #region IsValid

        public bool IsValid
        {
            get
            {
                if (!ItemShape.HasValue)
                    return false;

                if (ItemShape == ItemShapeEnum.Rectangle
                    && (Width == 0 || Height == 0))
                {
                    return false;
                }

                if (ItemShape == ItemShapeEnum.Round && Diameter == 0)
                    return false;

                return true;
            }
        }

        #endregion                

        #region FindByTask

        private const string SqlFindByTask =
            @"SELECT * FROM Item
                where TaskId = ?TaskId";

        public static List<Item> FindByTask(Task task, IDbConnection connection)
        {
            List<Item> items = new List<Item>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByTask, connection))
            {
                Database.PutParameter(dbCommand, "?TaskId", task.ID);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        items.Add(Load(dataReader));
                    }
                }
            }
            return items;
        }

        public static List<Item> FindByTask(Task task)
        {
            return FindByTask(task, null);
        }


        #endregion

        #region GetItemsCost

        public static decimal GetItemsCost(List<Item> items, bool isPartOfFlood, decimal discountPercentage)
        {
            decimal result = decimal.Zero;

            if (items == null)
                return result;

            if (isPartOfFlood)
            {
                foreach (Item item in items)
                    result += item.SubTotalCost;

                result = result*(1 - discountPercentage/100);
            } else
            {
                foreach (Item item in items)
                    result += item.CleanCost;

                result = Math.Max(result, QbItemRugCleaningCatalog.MinimumCharge);
                result = result * (1 - discountPercentage / 100);

                foreach (Item item in items)
                {
                    result += item.ProtectorCost;
                    result += item.PaddingCost;
                    result += item.MothRepelCost;
                    result += item.RapCost;
                    result += item.OtherCost;
                }                    
            }
                
            return result;            
        }

        #endregion

        #region ParseFromServman

        private const string DECIMAL_MASK = @"[0-9]+(\.[0-9]+)?";

        public static List<Item> ParseFromServman(string text)
        {
            List<Item> result = new List<Item>();

            while (text != string.Empty)
            {                
                Match match = Regex.Match(text, @"([0-9]+[ ]*[@\-\(][ ]*)?" + DECIMAL_MASK + "[ ]*[xX][ ]*" + DECIMAL_MASK);
                if (match.Success)
                {
                    string currentMatch = match.Value;

                    if (text.Length - 1 >= match.Index + match.Length)
                        text = text.Substring(match.Index + match.Length);
                    else
                        text = string.Empty;                    

                    Match rugsDimensionsMatch = Regex.Match(currentMatch, DECIMAL_MASK + "[ ]*[xX][ ]*" + DECIMAL_MASK);
                    Match widthMatch = Regex.Match(rugsDimensionsMatch.Value, DECIMAL_MASK);
                    decimal width = decimal.Parse(widthMatch.Value);
                    decimal height = decimal.Parse(widthMatch.NextMatch().Value);

                    currentMatch = currentMatch.Substring(0, rugsDimensionsMatch.Index);

                    int quantity = 1;
                    Match quantityMatch = Regex.Match(currentMatch, @"[0-9]+[ ]*[@\-\(][ ]*");
                    if (quantityMatch.Success)
                        quantity = int.Parse(Regex.Match(quantityMatch.Value, @"[0-9]+").Value);
                    if (quantity > 50)
                        quantity = 1;

                    for (int i = 0; i < quantity; i++)
                    {
                        Item item = new Item();
                        item.CleaningRate = GetDefaultCleaningRate(false);
                        ItemRecalcWrapper wrapper = new ItemRecalcWrapper(item);
                        wrapper.ItemShape = ItemShapeEnum.Rectangle;                        
                        wrapper.Width = width;
                        wrapper.Height = height;
                        wrapper.NestedItem.ItemType = ItemTypeEnum.Rug;
                        result.Add(wrapper.NestedItem);                        
                    }
                }
                else
                    return result;                
            }

            return result;
        }

        #endregion

        #region GetDefaultRug

        public static Item GetDefaultRug()
        {
            Item item = new Item();
            item.CleaningRate = GetDefaultCleaningRate(false);
            ItemRecalcWrapper wrapper = new ItemRecalcWrapper(item);
            wrapper.ItemShape = ItemShapeEnum.Rectangle;
            wrapper.Width = 1;
            wrapper.Height = 1;
            wrapper.NestedItem.ItemType = ItemTypeEnum.Rug;
            return wrapper.NestedItem;
        }

        #endregion

    }

    public class ItemRecalcWrapper
    {       
        private Item m_item;
        public Item NestedItem
        {
            get { return m_item; }
        }

        public decimal CleaningRate
        {
            get { return m_item.CleaningRate; }
            set
            {
                m_item.CleaningRate = value;
                UpdateCosts();
            }
        }


        public ItemRecalcWrapper(Item item)
        {
            m_item = item;
        }
        

        public String ItemShortSpec
        {
            get { return m_item.ItemShortSpec; }
        }       

        public bool IsValid
        {
            get { return m_item.IsValid; }
        }

        public decimal SquareFootage
        {
            get
            {
                if (ItemShape == ItemShapeEnum.Rectangle)
                    return Height * Width;
                else if (ItemShape == ItemShapeEnum.Round)
                    return (decimal) Item.PI * (Diameter * Diameter / 4);

                return decimal.Zero;
            }
        }

        public string Description
        {
            get
            {
                if (ItemShape == ItemShapeEnum.Rectangle)
                    return Height + "X" + Width;
                else if (ItemShape == ItemShapeEnum.Round)
                    return "Round D=" + Diameter;
                else
                    return string.Empty;
            }
        }

        public ItemShapeEnum? ItemShape
        {
            get { return m_item.ItemShape; }
            set
            {
                m_item.ItemShape = value;
                UpdateCosts();
            }
        }

        public int ID
        {
            get { return m_item.ID; }
            set { m_item.ID = value; }
        }       
        
        public String SerialNumber
        {
            get { return m_item.SerialNumber; }
            set { m_item.SerialNumber = value; }
        }       
        
        public decimal Width
        {
            get { return m_item.Width; }
            set
            {
                m_item.Width = value;
                UpdateCosts();
            }
        }

        
        public decimal Height
        {
            get { return m_item.Height; }
            set
            {
                m_item.Height = value;
                UpdateCosts();
            }
        }

        
        public decimal Diameter
        {
            get { return m_item.Diameter; }
            set
            {
                m_item.Diameter = value;
                UpdateCosts();
            }
        }

        
        public bool IsProtectorApplied
        {
            get { return m_item.IsProtectorApplied; }
            set
            {
                m_item.IsProtectorApplied = value;
                UpdateCosts();
            }
        }

        
        public bool IsPaddingApplied
        {
            get { return m_item.IsPaddingApplied; }
            set
            {
                m_item.IsPaddingApplied = value;
                UpdateCosts();
            }
        }

        
        public bool IsMothRepelApplied
        {
            get { return m_item.IsMothRepelApplied; }
            set
            {
                m_item.IsMothRepelApplied = value;
                UpdateCosts();
            }
        }

        
        public bool IsRapApplied
        {
            get { return m_item.IsRapApplied; }
            set
            {
                m_item.IsRapApplied = value;
                UpdateCosts();
            }
        }

        
        public decimal CleanCost
        {
            get { return m_item.CleanCost; }
            set { m_item.CleanCost = value; }
        }

        
        public decimal ProtectorCost
        {
            get { return m_item.ProtectorCost; }
            set { m_item.ProtectorCost = value; }
        }

        
        public decimal PaddingCost
        {
            get { return m_item.PaddingCost; }
            set { m_item.PaddingCost = value; }
        }

        
        public decimal MothRepelCost
        {
            get { return m_item.MothRepelCost; }
            set { m_item.MothRepelCost = value; }
        }

        
        public decimal RapCost
        {
            get { return m_item.RapCost; }
            set { m_item.RapCost = value; }
        }

        
        public decimal OtherCost
        {
            get { return m_item.OtherCost; }
            set
            {
                m_item.OtherCost = value;
                UpdateCosts();
            }
        }

        
        public decimal SubTotalCost
        {
            get { return m_item.SubTotalCost; }
            set { m_item.SubTotalCost = value; }
        }

        
        public decimal TaxCost
        {
            get { return m_item.TaxCost; }
            set { m_item.TaxCost = value; }
        }

        
        public decimal TotalCost
        {
            get { return m_item.TotalCost; }
            set { m_item.TotalCost = value; }
        }       
 
        private void UpdateCosts()
        {
            decimal squareFootage = SquareFootage;

            CleanCost = squareFootage * m_item.CleaningRate;
            ProtectorCost = IsProtectorApplied ? squareFootage * QbItemRugCleaningCatalog.ProtectorPricePerSqFoot : decimal.Zero;
            PaddingCost = IsPaddingApplied ? squareFootage * QbItemRugCleaningCatalog.PadPricePerSqFoot : decimal.Zero;
            MothRepelCost = IsMothRepelApplied ? squareFootage * QbItemRugCleaningCatalog.MothRepellantPricePerSqFoot : decimal.Zero;
            RapCost = IsRapApplied ? QbItemRugCleaningCatalog.WrapPricePerSqFoot : decimal.Zero;

            SubTotalCost = CleanCost + ProtectorCost + PaddingCost + MothRepelCost
                           + RapCost + OtherCost;

            TaxCost = (SubTotalCost* QbItemRugCleaningCatalog.TaxRate) / 100;

            TotalCost = SubTotalCost + TaxCost;
        }
    }
}
      