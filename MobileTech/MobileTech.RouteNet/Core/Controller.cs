using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using MobileTech.ServiceLayer;
using System.Diagnostics;
using System.Drawing;
using MobileTech.Windows.UI.StartDay;
using MobileTech.Windows.UI.EndDay;
using MobileTech.Windows.UI.DatabaseManager;
using MobileTech.Windows.UI.CustomerOperations;
using MobileTech.Windows.UI.MainMenu;
using MobileTech.Windows.UI.TComm;
using MobileTech.Windows.UI.Inventory.Menu;
using MobileTech.Windows.UI.Inventory.Load;
using MobileTech.Windows.UI.ItemSearch;
using MobileTech.Windows.UI.SelectItem;
using MobileTech.Windows.UI.RouteSetup;
using MobileTech.Windows.UI.Setup.Menu;
using MobileTech.Windows.UI.Password;
using System.Threading;
using System.IO;



namespace MobileTech.Windows.UI
{
    public class Controller : IApplication
    {
        #region Constructor
        private Controller(){ }
        #endregion

        #region MainForm
        Form m_mainForm;

        public Form MainForm
        {
            get
            {
                if (m_mainForm == null)
                    m_mainForm = new Forms.MainView();

                return m_mainForm;
            }
        }
        #endregion

        #region Instance

        private static Controller m_instance;

        public static Controller Instance
        {
            get
            {
                if (m_instance == null)
                    m_instance = new Controller();

                return m_instance;
            }
        }

        #endregion

        #region IApplication

        #region Execute

        public void Execute(CommandName command, bool async)
        {
            Execute(command, null, async);
        }

        public void Execute(CommandName command)
        {
            Execute(command, null, true);
        }

        public void Execute(CommandName command, Object data)
        {
            Execute(command, data, true);

        }

        public void Execute(CommandName command, Object data, bool async)
        {
            Form form = CreateFormInstance(command, data);

            ShowForm(form, async);
        }

        #endregion


        #endregion

        #region CreateFormInstance

        Form CreateFormInstance(CommandName command, Object data)
        {
            Form view = null;
            Object viewData = null;

             switch (command)
            {
                case CommandName.SetupRoute:
                    view = new RouteSetupView();
                    viewData = new RouteSetupModel();
                    break;
                case CommandName.SetupMenu:
                    view = new SetupMenu();
                    viewData = new SetupMenuModel();
                    break;
                case CommandName.StartDay:
                    view = new StartDayView();
                    viewData = new StartDayModel();
                    break;
                case CommandName.EndDay:
                    view = new EndDayView();
                    viewData = new EndDayModel();
                    break;
                case CommandName.Events:
                    view = new EventsView();
                    viewData = new EventsModel();
                    break;
                case CommandName.CustomerSelection:
                    view = new CustomerOperations.CustomerSelection.CustomerSelectionView();
                    break;
                case CommandName.CustomerOperations:
                    view = new CustomerOperations.Menu.CustomerOperationsMenuView();
                    break;
                case CommandName.InvoiceItemEntry:
                    view = new CustomerOperations.Invoice.InvoiceView();
                    break;
                case CommandName.Invoice:
                    throw new Exception("Command removed");
                case CommandName.CustomerOperationsCommit:
                    view = new CustomerOperations.Commit.CustomerOperationsCommitView();
                    break;
                case CommandName.MainMenu:
                    view = new MainMenuView();
                    viewData = new MainMenuModel();
                    break;
                case CommandName.TComm:
                    view = new TCommView();
                    viewData = new TCommModel();
                    break;
                case CommandName.InventoryMenu:
                    view = new InventoryMenu();
                    viewData = new InventoryMenuModel();
                     break;
                case CommandName.InventoryLoad:
                    view = new Inventory.Load.Menu.InventoryLoadMenuView();
                    break;
                case CommandName.ItemSearch:
                    view = new ItemSearchView();
                    break;
                case CommandName.SelectItem:
                    view = new SelectItemView();
                    break;
                case CommandName.Odometer:
                    view = new Odometer.OdometerView();
                    break;
                case CommandName.InventoryUnload:
                    view = new Inventory.Unload.Menu.InventoryUnloadMenuView();
                    break;
                case CommandName.InventoryUnloadCommit:
                    view = new Inventory.Unload.Commit.InventoryUnloadCommitView();
                    break;
                case CommandName.InventoryUnloadGood:
                    view = new Inventory.Unload.Good.InventoryUnloadGoodView();
                    break;
                case CommandName.InventoryUnloadDamage:
                    view = new Inventory.Unload.Damage.InventoryUnloadDamageView();
                    break;
                case CommandName.InventoryUnloadEquipment:
                    view = new Inventory.Unload.Equipment.InventoryUnloadEquipmentView();
                    break;
                case CommandName.InventoryLoadCommit:
                    view = new Inventory.Load.Commit.InventoryLoadCommitView();
                    break;
                case CommandName.InventoryLoadGood:
                    view = new Inventory.Load.Good.InventoryLoadGoodView();
                    break;
                case CommandName.InventoryLoadDamage:
                    view = new Inventory.Load.Damage.InventoryLoadDamageView();
                    break;
                case CommandName.InventoryLoadEquipment:
                    view = new Inventory.Load.Equipment.InventoryLoadEquipmentView();
                    break;
                case CommandName.Password:
                    view = new PasswordView();
                    break;
                default:
                    throw new MobileTechInvalidCommandExeption(command);
            }


            if (view != null)
            {
                (view as IView).App = this;

                using (WaitCursor cursor = new WaitCursor())
                {
                    (view as IView).InitModel();


                    if (viewData != null)
                    {
                        if(viewData is IModel)
                            ((IModel)viewData).Init();

                        ((IView)view).BindData(viewData);
                    }
            

                    if (data != null)
                        ((IView)view).BindData(data);
                    
                }

            }

            return view;
        }

        #endregion

        #region ShowForm

        public void ShowForm(Form form, bool async)
        {

#if WIN32
                form.Top = 0;

                if (m_mainForm.WindowState != FormWindowState.Normal)
                    m_mainForm.WindowState = FormWindowState.Normal;
           
                 form.Bounds = m_mainForm.Bounds;
                 form.Size = m_mainForm.Size;
                 form.StartPosition = FormStartPosition.Manual;

                 form.SizeGripStyle = SizeGripStyle.Hide;
#endif

#if WINCE
            form.MinimizeBox = false;
#endif


            if (async)
                form.Show();
            else
                form.ShowDialog();
        }


        #endregion
    }
}
