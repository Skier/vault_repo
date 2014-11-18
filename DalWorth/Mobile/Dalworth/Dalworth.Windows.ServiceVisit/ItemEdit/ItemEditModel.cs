using System;
using System.Collections.Generic;
using System.Text;
using Dalworth.Domain;
using Dalworth.Domain.SyncService;
using Dalworth.Windows.ServiceVisit.ServiceVisit;
using Item=Dalworth.Domain.SyncService.Item;
using ItemShapeEnum=Dalworth.Domain.ItemShapeEnum;
using ItemTypeEnum=Dalworth.Domain.ItemTypeEnum;

namespace Dalworth.Windows.ServiceVisit.ItemEdit
{
    public enum RugAction
    {
        Add,
        Edit,
        View
    }

    public class ItemEditModel : IModel
    {
        #region VisitTotalWithoutCurrentRug

        private decimal m_visitTotalWithoutCurrentRug;
        public decimal VisitTotalWithoutCurrentRug
        {
            get { return m_visitTotalWithoutCurrentRug; }
            set { m_visitTotalWithoutCurrentRug = value; }
        }

        #endregion

        #region Task

        private TaskPackage m_task;
        public TaskPackage Task
        {
            get { return m_task; }
            set { m_task = value; }
        }

        #endregion

        #region RugAction

        private RugAction m_rugAction;
        public RugAction RugAction
        {
            get { return m_rugAction; }
            set { m_rugAction = value; }
        }

        #endregion

        #region CurrentRugIndex

        private int m_currentRugIndex;
        public int CurrentRugIndex
        {
            get { return m_currentRugIndex; }
            set { m_currentRugIndex = value; }
        }

        #endregion

        #region CurrentRug

        private Item m_currentRug;
        public Item CurrentRug
        {
            get { return m_currentRug; }
        }

        #endregion        

        #region IsRugPickup

        public bool IsRugPickup
        {
            get { return Task.Task.TaskTypeId == (int)TaskTypeEnum.RugPickup; }
        }

        #endregion        

        #region Init

        public void Init()
        {
            List<Item> items = new List<Item>(Task.Items);

            m_visitTotalWithoutCurrentRug = decimal.Zero;
            for (int i = 0; i < items.Count; i++)
            {
                if (i != CurrentRugIndex)
                    m_visitTotalWithoutCurrentRug += items[i].TotalCost;
            }


            if (RugAction == RugAction.Add)
            {                
                m_currentRug = new Item();
                CurrentRug.ItemTypeId = (int) ItemTypeEnum.Rug;
                CurrentRug.SerialNumber = "undefined";
                CurrentRug.ItemShapeId = (int) ItemShapeEnum.Rectangle;
                CurrentRug.Width = 0;
                CurrentRug.Height = 0;
                CurrentRug.Diameter = 0;
                CurrentRug.IsProtectorApplied = false;
                CurrentRug.IsPaddingApplied = false;
                CurrentRug.IsMothRepelApplied = false;
                CurrentRug.IsRapApplied = false;
                CurrentRug.CleanCost = 0;
                CurrentRug.ProtectorCost = 0;
                CurrentRug.PaddingCost = 0;
                CurrentRug.MothRepelCost = 0;
                CurrentRug.RapCost = 0;
                CurrentRug.OtherCost = 0;
                CurrentRug.SubTotalCost = 0;
                CurrentRug.TaxCost = 0;
                CurrentRug.TotalCost = 0;
            }
            else if (RugAction == RugAction.Edit || RugAction == RugAction.View)
            {
                Item inputItem = items[CurrentRugIndex];

                m_currentRug = new Item();
                CurrentRug.ItemTypeId = inputItem.ItemTypeId;
                CurrentRug.SerialNumber = inputItem.SerialNumber;
                CurrentRug.ItemShapeId = inputItem.ItemShapeId;
                CurrentRug.Width = inputItem.Width;
                CurrentRug.Height = inputItem.Height;
                CurrentRug.Diameter = inputItem.Diameter;
                CurrentRug.IsProtectorApplied = inputItem.IsProtectorApplied;
                CurrentRug.IsPaddingApplied = inputItem.IsPaddingApplied;
                CurrentRug.IsMothRepelApplied = inputItem.IsMothRepelApplied;
                CurrentRug.IsRapApplied = inputItem.IsRapApplied;
                CurrentRug.CleanCost = inputItem.CleanCost;
                CurrentRug.ProtectorCost = inputItem.ProtectorCost;
                CurrentRug.PaddingCost = inputItem.PaddingCost;
                CurrentRug.MothRepelCost = inputItem.MothRepelCost;
                CurrentRug.RapCost = inputItem.RapCost;
                CurrentRug.OtherCost = inputItem.OtherCost;
                CurrentRug.SubTotalCost = inputItem.SubTotalCost;
                CurrentRug.TaxCost = inputItem.TaxCost;
                CurrentRug.TotalCost = inputItem.TotalCost;
                
            }
        }

        #endregion
    }
}
