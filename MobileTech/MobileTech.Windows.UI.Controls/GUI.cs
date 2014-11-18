using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using System.Reflection;
//using System.Drawing;

namespace MobileTech
{

}
namespace MobileTech.Windows.UI
{
    public enum ImageKeys
    {
        None,
        About,
        AboutFocus,
        AboutDisabled,
        Add_Small,
        Add_SmallFocus,
        Add_SmallDisabled,
        ARCollection,
        ARCollectionFocus,
        ARCollectionDisabled,
        Back_Small,
        Back_SmallFocus,
        Back_SmallDisabled,
        Cancel_Small,
        Cancel_SmallDisabled,
        Cancel_SmallFocus,
        Close,
        CloseFocus,
        CloseDisabled,
        Continue,
        ContinueFocus,
        ContinueDisabled,
        CustList,
        CustListFocus,
        CustListDisabled,
        CustomerService,
        CustomerServiceFocus,
        CustomerServiceDisabled,
        Detail_Small,
        Detail_SmallFocus,
        Detail_SmallDisabled,
        Done,
        DoneFocus,
        DoneDisabled,
        Done_Small,
        Done_SmallFocus,
        Done_SmallDisabled,
        EndDay,
        EndDayFocus,
        EndDayDisabled,
        EndDayDone,
        EndDayDoneFocus,
        EndDayDoneDisabled,
        Events,
        EventsFocus,
        EventsDisabled,
        Exit,
        ExitFocus,
        ExitDisabled,
        House,
        HouseFocus,
        HouseDisabled,
        Information,
        InformationFocus,
        InformationDisabled,
        Inventory,
        InventoryFocus,
        InventoryDisabled,
        Invoice,
        InvoiceFocus,
        InvoiceDisabled,
        Load,
        LoadFocus,
        LoadDisabled,
        LoadRequest,
        LoadRequestFocus,
        LoadRequestDisabled,
        LoadTransfer,
        LoadTransferFocus,
        LoadTransferDisabled,
        Merchandising,
        MerchandisingFocus,
        MerchandisingDisabled,
        MobileTech,
        Order,
        OrderFocus,
        OrderDisabled,
        PenTarg,
        PenTargFocus,
        PenTargDisabled,
        PrintReport,
        PrintReportFocus,
        PrintReportDisabled,
        ReturnToStock,
        ReturnToStockFocus,
        ReturnToStockDisabled,
        Review,
        ReviewFocus,
        ReviewDisabled,
        ReviewVisits,
        ReviewVisitsFocus,
        ReviewVisitsDisabled,
        Save,
        SaveFocus,
        SaveDisabled,
        Select_Small,
        Select_SmallFocus,
        Select_SmallDisabled,
        Setup,
        SetupFocus,
        SetupDisabled,
        StartDay,
        StartDayFocus,
        StartDayDisabled,
        SwitchRoute,
        SwitchRouteFocus,
        SwitchRouteDisabled,
        SysInfo,
        SysInfoFocus,
        SysInfoDisabled,
        TCommSetup,
        TCommSetupFocus,
        TCommSetupDisabled,
        TransmitData,
        TransmitDataFocus,
        TransmitDataDisabled,
        TrkDrvr,
        TrkDrvrFocus,
        TrkDrvrDisabled,
        Unload,
        UnloadFocus,
        UnloadDisabled
    }
   
    static public class GUI
    {
        private static Image[] imgList = new Image[150];

        public static Image GetImage(ImageKeys ik)
        {
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
    }
}
