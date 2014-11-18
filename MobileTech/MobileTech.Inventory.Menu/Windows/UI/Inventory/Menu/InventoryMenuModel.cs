using System;
using System.Collections.Generic;
using System.Text;
using MobileTech.Domain;

namespace MobileTech.Windows.UI.Inventory.Menu
{
    public class InventoryMenuModel : IModel
    {
        bool m_unloadAllow;

        public bool UnloadAllow
        {
            get { return m_unloadAllow; }
            set { m_unloadAllow = value; }
        }
        bool m_loadAllow;

        public bool LoadAllow
        {
            get { return m_loadAllow; }
            set { m_loadAllow = value; }
        }


        #region IModel Members

        public void Init()
        {
            RouteStatusEnum routeStatus = Route.Current.Status;

            if (Route.Current.InventorySync)
            {
                m_unloadAllow = routeStatus  == RouteStatusEnum.LOAD_DONE;
                m_loadAllow = routeStatus == RouteStatusEnum.SOP_DONE;
            }
            else
            {
                m_loadAllow = true;
                m_unloadAllow = true;
            }
        }

        #endregion


    }
}
