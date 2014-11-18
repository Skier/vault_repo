using System;

namespace Dalworth.Controls.Menu
{


    interface IMenuButton
    {
        ImageKeys Picture { get; set; }
        void Select();
        bool ShowBorder { get; set; }
    }
}
