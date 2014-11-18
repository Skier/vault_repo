using System;
using System.Collections.Generic;
using System.Text;
using MobileTech.ServiceLayer;
using MobileTech.Domain;
using System.IO;
using MobileTech.Data;



namespace MobileTech.Windows.UI.MainMenu
{
	public class MainMenuModel:IModel
    {
        #region Service


        public bool IsCommandAllow(CommandName command)
        {
            bool rv = false;

            if (Database.IsDatabaseExist() && (Route.IsContainsData()))
            {
                RouteStatusEnum routeStatus = Route.Current.Status;

                switch (command)
                {
                    case CommandName.StartDay:
                        rv = (routeStatus == RouteStatusEnum.IDL_TCOM_DONE
                         || routeStatus == RouteStatusEnum.EOP_DONE
                         || routeStatus == RouteStatusEnum.EOP_TCOM_DONE);
                        break;
                    case CommandName.TComm:
                        rv = (routeStatus == RouteStatusEnum.IDL
                         || routeStatus >= RouteStatusEnum.EOP_DONE);
                        break;
                    case CommandName.EndDay:
                        if (Route.Current.InventorySync)
                        {
                            rv = routeStatus == RouteStatusEnum.UNLOAD_DONE;
                        }
                        else
                        {
                             rv = routeStatus != RouteStatusEnum.EOP_DONE
                                 && routeStatus >= RouteStatusEnum.SOP_DONE;
                        }
                        break;
                    case CommandName.CustomerOperations:

                        if (Route.Current.InventorySync)
                            rv = routeStatus == RouteStatusEnum.LOAD_DONE;
                        else
                            rv = routeStatus == RouteStatusEnum.SOP_DONE;

                        break;

                    case CommandName.InventoryMenu:
                        if (Route.Current.InventorySync)
                        {
                            rv = routeStatus == RouteStatusEnum.SOP_DONE
                                || routeStatus == RouteStatusEnum.LOAD_DONE;
                        }
                        else
                            rv = true;

                        break;
                    case CommandName.SetupMenu:
                        rv = true;
                        break;

                }
            }
            else
            {
                switch (command)
                {
                    case CommandName.TComm:
                        if ((Configuration.Route != 0) && (Configuration.Location != 0))
                        {
                            rv = true;
                        }
                        break;
                    case CommandName.SetupMenu:
                        rv = true;
                        break;
                }
            }

            return rv;
        }

        #endregion

        #region IModel Members


        public void Init()
        {

        }

        #endregion
    }
}
