using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace MobileTech.Windows.UI
{
    public static class CommonResources
    {
        #region ChangeCulture

        public static void ChangeCulture(CultureInfo cultureInfo)
        {
            CommonUIResources.Culture = cultureInfo;
        }

        #endregion

        #region Messages

        #region Invalid Password
        public static String MsgInvalidPassword
        {
            get
            {
                return CommonUIResources.MsgInvalidPassword;
            }
        }
        #endregion

        #region Invalid Entry
        public static String MsgInvalidEntry
        {
            get
            {
                return CommonUIResources.MsgInvalidEntry;
            }
        }
        #endregion

        #region Do you want discard changes ?
        public static String MsgDoYouWantDiscardChanges
        {
            get
            {
                return CommonUIResources.MsgDoYouWantDiscardChanges;
            }
        }
        #endregion

        #region All changes will be lost, exit ?
        public static String MsgAllChangesWillBeLostExit
        {
            get
            {
                return CommonUIResources.MsgAllChangesWillBeLostExit;
            }
        }
        #endregion

        #endregion

        #region Buttons

        #region Done

        public static String BtnDone
        {
            get
            {
                return CommonUIResources.BtnDone;
            }
        }

        #endregion

        #region Finish

        public static String BtnFinish
        {
            get
            {
                return CommonUIResources.BtnFinish;
            }
        }

        #endregion

        #region Exit

        public static String BtnExit
        {
            get
            {
                return CommonUIResources.BtnExit;
            }
        }

        #endregion

        #region Find
        public static String BtnFind
        {
            get
            {
                return CommonUIResources.BtnFind;
            }
        }
        #endregion

        #region Add item

        public static String BtnAddItem
        {
            get
            {
                return CommonUIResources.BtnAddItem;
            }
        }

        #endregion

        #region Final

        public static String BtnFinal
        {
            get
            {
                return CommonUIResources.BtnFinal;
            }
        }

        #endregion

        #region Back

        public static String BtnBack
        {
            get
            {
                return CommonUIResources.BtnBack;
            }
        }

        #endregion

        #region Continue

        public static String BtnContinue
        {
            get
            {
                return CommonUIResources.BtnContinue;
            }
        }

        #endregion

        #region Select

        public static String BtnSelect
        {
            get
            {
                return CommonUIResources.BtnSelect;
            }
        }

        #endregion

        #endregion

        #region Dictionary

        #region Catalog

        public static String DcCatalog
        {
            get
            {
                return CommonUIResources.DcCatalog;
            }
        }

        #endregion

        #region Name

        public static String DcName
        {
            get
            {
                return CommonUIResources.DcName;
            }
        }

        #endregion

        #region Number

        public static String DcNumber
        {
            get
            {
                return CommonUIResources.DcNumber;
            }
        }

        #endregion

        #region Quantity

        public static String DcQuantity
        {
            get
            {
                return CommonUIResources.DcQuantity;
            }
        }

        #endregion

        #region Qty

        public static String DcQty
        {
            get
            {
                return CommonUIResources.DcQty;
            }
        }

        #endregion

        #region Product

        public static String DcProduct
        {
            get
            {
                return CommonUIResources.DcProduct;
            }
        }

        #endregion

        #region Equipment

        public static String DcEquipment
        {
            get
            {
                return CommonUIResources.DcEquipment;
            }
        }

        #endregion

        #region Good

        public static String DcGood
        {
            get
            {
                return CommonUIResources.DcGood;
            }
        }

        #endregion

        #region Damage

        public static String DcDamage
        {
            get
            {
                return CommonUIResources.DcDamage;
            }
        }

        #endregion

        #region Unload

        public static String DcUnload
        {
            get
            {
                return CommonUIResources.DcUnload;
            }
        }

        #endregion

        #region Truck

        public static String DcTruck
        {
            get
            {
                return CommonUIResources.DcTruck;
            }
        }

        #endregion

        #endregion

        #region Dialog caption
        public static String DialogCaption
        {
            get
            {
                return CommonUIResources.DialogCaption;
            }
        }
        #endregion

        #region Enum

        #region StorageType

        public static String EnmStorageTypeStore
        {
            get
            {
                return CommonUIResources.EnmStorageTypeStore;
            }
        }

        public static String EnmStorageTypeBin
        {
            get
            {
                return CommonUIResources.EnmStorageTypeBin;
            }
        }

        #endregion

        #endregion
    }
}
