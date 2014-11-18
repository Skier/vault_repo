using System;
namespace MobileTech.Windows.UI.Controls
{


    interface IMenuButton
    {
        ImageKeys Picture { get; set; }
#if WINCE
        void Select();
#endif
        bool ShowBorder { get; set; }
    }
}
