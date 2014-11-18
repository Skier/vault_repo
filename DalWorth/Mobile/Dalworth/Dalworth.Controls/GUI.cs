using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using System.Reflection;
//using System.Drawing;


namespace Dalworth.Controls
{

    public enum ImageKeys
    {
        None,
        About,
        AddSmall,
        ARCollection,
        BackSmall,
        CancelSmall,
        Close,
        Continue,
        CustList,
        CustomerService,
        CustomerServiceRed,
        Delete,
        DetailSmall,
        Done,
        DoneSmall,
        EndDay,
        EndDayDone,
        Events,
        Exit,
        House,
        Information,
        Inventory,
        Invoice,
        Load,
        LoadRequest,
        LoadTransfer,
        Merchandising,
        MobileTech,
        Order,
        PenTarg,
        PrintReport,
        ReturnToStock,
        Review,
        ReviewVisits,
        RouteBook,
        Save,
        SelectSmall,
        Setup,
        Sort,
        StartDay,
        SwitchRoute,
        SysInfo,
        TCommSetup,
        TransmitData,
        TrkDrvr,
        Unload,
        Cash,
        Check,
        CreditCard,
        Tools,
        Calc,
        Key0,
        Key1,
        Key2,
        Key3,
        Key4,
        Key5,
        Key6,
        Key7,
        Key8,
        Key9,
        KeyRight,
        KeyEnter,
        KeyLeft,
        JobHistory,
        MessageToDispatch,
        MessageToTechnician
    }


    /*
  
    static public class GUI
    {
        private static Image[] imgList = new Image[150];

        public static Image GetImage(ImageKeys ik)
        {
            return null;

            if (ik == ImageKeys.None)
            {
                return null;
            }
            else
            {
                return imgList[(int)ik];
            }
        }
        public static void SetImage(ImageKeys ik, string imagePath)
        {
            imgList[(int)ik] = new Bitmap(imagePath);
        }

        public static void LoadImages(string bitmapDir)
        {

            try
            {
                GUI.SetImage(ImageKeys.About, bitmapDir + "About.png");
                GUI.SetImage(ImageKeys.AboutFocus, bitmapDir + "AboutFocus.png");
                GUI.SetImage(ImageKeys.AboutDisabled, bitmapDir + "AboutDisabled.png");
                GUI.SetImage(ImageKeys.AddSmall, bitmapDir + "Add_Small.png");
                GUI.SetImage(ImageKeys.Add_SmallFocus, bitmapDir + "Add_SmallFocus.png");
                GUI.SetImage(ImageKeys.Add_SmallDisabled, bitmapDir + "Add_SmallDisabled.png");
                GUI.SetImage(ImageKeys.ARCollection, bitmapDir + "ARCollection.png");
                GUI.SetImage(ImageKeys.ARCollectionFocus, bitmapDir + "ARCollectionFocus.png");
                GUI.SetImage(ImageKeys.ARCollectionDisabled, bitmapDir + "ARCollectionDisabled.png");
                GUI.SetImage(ImageKeys.BackSmall, bitmapDir + "Back_Small.png");
                GUI.SetImage(ImageKeys.Back_SmallFocus, bitmapDir + "Back_SmallFocus.png");
                GUI.SetImage(ImageKeys.Back_SmallDisabled, bitmapDir + "Back_SmallDisabled.png");
                GUI.SetImage(ImageKeys.Close, bitmapDir + "Close.png");
                GUI.SetImage(ImageKeys.CloseFocus, bitmapDir + "CloseFocus.png");
                GUI.SetImage(ImageKeys.CloseDisabled, bitmapDir + "CloseDisabled.png");
                GUI.SetImage(ImageKeys.CancelSmall, bitmapDir + "Cancel_Small.png");
                GUI.SetImage(ImageKeys.Cancel_SmallFocus, bitmapDir + "Cancel_SmallFocus.png");
                GUI.SetImage(ImageKeys.Cancel_SmallDisabled, bitmapDir + "Cancel_SmallDisabled.png");
                GUI.SetImage(ImageKeys.Continue, bitmapDir + "Continue.png");
                GUI.SetImage(ImageKeys.ContinueFocus, bitmapDir + "ContinueFocus.png");
                GUI.SetImage(ImageKeys.ContinueDisabled, bitmapDir + "ContinueDisabled.png");
                GUI.SetImage(ImageKeys.CustList, bitmapDir + "CustList.png");
                GUI.SetImage(ImageKeys.CustListFocus, bitmapDir + "CustListFocus.png");
                GUI.SetImage(ImageKeys.CustListDisabled, bitmapDir + "CustListDisabled.png");
                GUI.SetImage(ImageKeys.CustomerService, bitmapDir + "CustomerService.png");
                GUI.SetImage(ImageKeys.CustomerServiceFocus, bitmapDir + "CustomerServiceFocus.png");
                GUI.SetImage(ImageKeys.CustomerServiceDisabled, bitmapDir + "CustomerServiceDisabled.png");
                GUI.SetImage(ImageKeys.Delete, bitmapDir + "Delete.png");
                GUI.SetImage(ImageKeys.DeleteFocus, bitmapDir + "DeleteFocus.png");
                GUI.SetImage(ImageKeys.DeleteDisabled, bitmapDir + "DeleteDisabled.png");
                GUI.SetImage(ImageKeys.DetailSmall, bitmapDir + "Detail_Small.png");
                GUI.SetImage(ImageKeys.Detail_SmallFocus, bitmapDir + "Detail_SmallFocus.png");
                GUI.SetImage(ImageKeys.Detail_SmallDisabled, bitmapDir + "Detail_SmallDisabled.png");
                GUI.SetImage(ImageKeys.Done, bitmapDir + "Done.png");
                GUI.SetImage(ImageKeys.DoneFocus, bitmapDir + "DoneFocus.png");
                GUI.SetImage(ImageKeys.DoneDisabled, bitmapDir + "DoneDisabled.png");
                GUI.SetImage(ImageKeys.DoneSmall, bitmapDir + "Done_Small.png");
                GUI.SetImage(ImageKeys.Done_SmallFocus, bitmapDir + "Done_SmallFocus.png");
                GUI.SetImage(ImageKeys.Done_SmallDisabled, bitmapDir + "Done_SmallDisabled.png");
                GUI.SetImage(ImageKeys.EndDay, bitmapDir + "EndDay.png");
                GUI.SetImage(ImageKeys.EndDayFocus, bitmapDir + "EndDayFocus.png");
                GUI.SetImage(ImageKeys.EndDayDisabled, bitmapDir + "EndDayDisabled.png");
                GUI.SetImage(ImageKeys.EndDayDone, bitmapDir + "EndDayDone.png");
                GUI.SetImage(ImageKeys.EndDayDoneFocus, bitmapDir + "EndDayDoneFocus.png");
                GUI.SetImage(ImageKeys.EndDayDoneDisabled, bitmapDir + "EndDayDoneDisabled.png");
                GUI.SetImage(ImageKeys.Events, bitmapDir + "Events.png");
                GUI.SetImage(ImageKeys.EventsFocus, bitmapDir + "EventsFocus.png");
                GUI.SetImage(ImageKeys.EventsDisabled, bitmapDir + "EventsDisabled.png");
                GUI.SetImage(ImageKeys.Exit, bitmapDir + "Exit.png");
                GUI.SetImage(ImageKeys.ExitFocus, bitmapDir + "ExitFocus.png");
                GUI.SetImage(ImageKeys.ExitDisabled, bitmapDir + "ExitDisabled.png");
                GUI.SetImage(ImageKeys.House, bitmapDir + "House.png");
                GUI.SetImage(ImageKeys.HouseFocus, bitmapDir + "HouseFocus.png");
                GUI.SetImage(ImageKeys.HouseDisabled, bitmapDir + "HouseDisabled.png");
                GUI.SetImage(ImageKeys.Information, bitmapDir + "Information.png");
                GUI.SetImage(ImageKeys.InformationFocus, bitmapDir + "InformationFocus.png");
                GUI.SetImage(ImageKeys.InformationDisabled, bitmapDir + "InformationDisabled.png");
                GUI.SetImage(ImageKeys.Inventory, bitmapDir + "Inventory.png");
                GUI.SetImage(ImageKeys.InventoryFocus, bitmapDir + "InventoryFocus.png");
                GUI.SetImage(ImageKeys.InventoryDisabled, bitmapDir + "InventoryDisabled.png");
                GUI.SetImage(ImageKeys.Invoice, bitmapDir + "Invoice.png");
                GUI.SetImage(ImageKeys.InvoiceFocus, bitmapDir + "InvoiceFocus.png");
                GUI.SetImage(ImageKeys.InvoiceDisabled, bitmapDir + "InvoiceDisabled.png");
                GUI.SetImage(ImageKeys.Load, bitmapDir + "Load.png");
                GUI.SetImage(ImageKeys.LoadFocus, bitmapDir + "LoadFocus.png");
                GUI.SetImage(ImageKeys.LoadDisabled, bitmapDir + "LoadDisabled.png");
                GUI.SetImage(ImageKeys.LoadRequest, bitmapDir + "LoadRequest.png");
                GUI.SetImage(ImageKeys.LoadRequestFocus, bitmapDir + "LoadRequestFocus.png");
                GUI.SetImage(ImageKeys.LoadRequestDisabled, bitmapDir + "LoadRequestDisabled.png");
                GUI.SetImage(ImageKeys.LoadTransfer, bitmapDir + "LoadTransfer.png");
                GUI.SetImage(ImageKeys.LoadTransferFocus, bitmapDir + "LoadTransferFocus.png");
                GUI.SetImage(ImageKeys.LoadTransferDisabled, bitmapDir + "LoadTransferDisabled.png");
                GUI.SetImage(ImageKeys.Merchandising, bitmapDir + "Merchandising.png");
                GUI.SetImage(ImageKeys.MerchandisingFocus, bitmapDir + "MerchandisingFocus.png");
                GUI.SetImage(ImageKeys.MerchandisingDisabled, bitmapDir + "MerchandisingDisabled.png");
                GUI.SetImage(ImageKeys.MobileTech, bitmapDir + "MobileTech.bmp");
                GUI.SetImage(ImageKeys.Order, bitmapDir + "Order.png");
                GUI.SetImage(ImageKeys.OrderFocus, bitmapDir + "OrderFocus.png");
                GUI.SetImage(ImageKeys.OrderDisabled, bitmapDir + "OrderDisabled.png");
                GUI.SetImage(ImageKeys.PenTarg, bitmapDir + "PenTarg.png");
                GUI.SetImage(ImageKeys.PenTargFocus, bitmapDir + "PenTargFocus.png");
                GUI.SetImage(ImageKeys.PenTargDisabled, bitmapDir + "PenTargDisabled.png");
                GUI.SetImage(ImageKeys.PrintReport, bitmapDir + "PrintReport.png");
                GUI.SetImage(ImageKeys.PrintReportFocus, bitmapDir + "PrintReportFocus.png");
                GUI.SetImage(ImageKeys.PrintReportDisabled, bitmapDir + "PrintReportDisabled.png");
                GUI.SetImage(ImageKeys.ReturnToStock, bitmapDir + "ReturnToStock.png");
                GUI.SetImage(ImageKeys.ReturnToStockFocus, bitmapDir + "ReturnToStockFocus.png");
                GUI.SetImage(ImageKeys.ReturnToStockDisabled, bitmapDir + "ReturnToStockDisabled.png");
                GUI.SetImage(ImageKeys.Review, bitmapDir + "Review.png");
                GUI.SetImage(ImageKeys.ReviewFocus, bitmapDir + "ReviewFocus.png");
                GUI.SetImage(ImageKeys.ReviewDisabled, bitmapDir + "ReviewDisabled.png");
                GUI.SetImage(ImageKeys.ReviewVisits, bitmapDir + "ReviewVisits.png");
                GUI.SetImage(ImageKeys.ReviewVisitsFocus, bitmapDir + "ReviewVisitsFocus.png");
                GUI.SetImage(ImageKeys.ReviewVisitsDisabled, bitmapDir + "ReviewVisitsDisabled.png");
                GUI.SetImage(ImageKeys.RouteBook, bitmapDir + "RouteBook.png");
                GUI.SetImage(ImageKeys.RouteBookFocus, bitmapDir + "RouteBookFocus.png");
                GUI.SetImage(ImageKeys.RouteBookDisabled, bitmapDir + "RouteBookDisabled.png");
                GUI.SetImage(ImageKeys.Save, bitmapDir + "Save.png");
                GUI.SetImage(ImageKeys.SaveFocus, bitmapDir + "SaveFocus.png");
                GUI.SetImage(ImageKeys.SaveDisabled, bitmapDir + "SaveDisabled.png");
                GUI.SetImage(ImageKeys.SelectSmall, bitmapDir + "Select_Small.png");
                GUI.SetImage(ImageKeys.Select_SmallFocus, bitmapDir + "Select_SmallFocus.png");
                GUI.SetImage(ImageKeys.Select_SmallDisabled, bitmapDir + "Select_SmallDisabled.png");
                GUI.SetImage(ImageKeys.Setup, bitmapDir + "Setup.png");
                GUI.SetImage(ImageKeys.SetupFocus, bitmapDir + "SetupFocus.png");
                GUI.SetImage(ImageKeys.SetupDisabled, bitmapDir + "SetupDisabled.png");
                GUI.SetImage(ImageKeys.Sort, bitmapDir + "Sort.png");
                GUI.SetImage(ImageKeys.SortFocus, bitmapDir + "SortFocus.png");
                GUI.SetImage(ImageKeys.SortDisabled, bitmapDir + "SortDisabled.png");
                GUI.SetImage(ImageKeys.StartDay, bitmapDir + "StartDay.png");
                GUI.SetImage(ImageKeys.StartDayFocus, bitmapDir + "StartDayFocus.png");
                GUI.SetImage(ImageKeys.StartDayDisabled, bitmapDir + "StartDayDisabled.png");
                GUI.SetImage(ImageKeys.SwitchRoute, bitmapDir + "SwitchRoute.png");
                GUI.SetImage(ImageKeys.SwitchRouteFocus, bitmapDir + "SwitchRouteFocus.png");
                GUI.SetImage(ImageKeys.SwitchRouteDisabled, bitmapDir + "SwitchRouteDisabled.png");
                GUI.SetImage(ImageKeys.SysInfo, bitmapDir + "SysInfo.png");
                GUI.SetImage(ImageKeys.SysInfoFocus, bitmapDir + "SysInfoFocus.png");
                GUI.SetImage(ImageKeys.SysInfoDisabled, bitmapDir + "SysInfoDisabled.png");
                GUI.SetImage(ImageKeys.TCommSetup, bitmapDir + "TCommSetup.png");
                GUI.SetImage(ImageKeys.TCommSetupFocus, bitmapDir + "TCommSetupFocus.png");
                GUI.SetImage(ImageKeys.TCommSetupDisabled, bitmapDir + "TCommSetupDisabled.png");
                GUI.SetImage(ImageKeys.TransmitData, bitmapDir + "TransmitData.png");
                GUI.SetImage(ImageKeys.TransmitDataFocus, bitmapDir + "TransmitDataFocus.png");
                GUI.SetImage(ImageKeys.TransmitDataDisabled, bitmapDir + "TransmitDataDisabled.png");
                GUI.SetImage(ImageKeys.TrkDrvr, bitmapDir + "TrkDrvr.png");
                GUI.SetImage(ImageKeys.TrkDrvrFocus, bitmapDir + "TrkDrvrFocus.png");
                GUI.SetImage(ImageKeys.TrkDrvrDisabled, bitmapDir + "TrkDrvrDisabled.png");
                GUI.SetImage(ImageKeys.Unload, bitmapDir + "Unload.png");
                GUI.SetImage(ImageKeys.UnloadFocus, bitmapDir + "UnloadFocus.png");
                GUI.SetImage(ImageKeys.UnloadDisabled, bitmapDir + "UnloadDisabled.png");
            }
            catch (Exception ex)
            {
                throw new  MobileTechException(ex);
            }

        }
    }
     */
}
