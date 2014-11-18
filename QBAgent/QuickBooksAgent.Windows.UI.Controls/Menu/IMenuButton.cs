using System;
namespace QuickBooksAgent.Windows.UI.Controls
{


    interface IMenuButton //: IJoystickElement
    {
        ImageKeys Picture { get; set; }
#if WINCE
        void Select();
#endif
        bool ShowBorder { get; set; }
    }
}
