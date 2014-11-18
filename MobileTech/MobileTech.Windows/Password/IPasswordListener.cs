using System;
using System.Collections.Generic;
using System.Text;

namespace MobileTech.Windows.UI.Password
{
    public interface IPasswordModel : IModel
    {
        PasswordName PasswordCommand { get; set; }
        bool PasswordPassed { get; set; }
        string PasswordUsed { get; set;}

    }
}
