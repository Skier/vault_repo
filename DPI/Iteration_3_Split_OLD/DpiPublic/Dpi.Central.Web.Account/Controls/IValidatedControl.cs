using System;
using System.Web.UI;

namespace Dpi.Central.Web.Account.Controls
{
    public class ValidateEventArgs : EventArgs
    {
        public bool IsValid = true;
        public string ErrorMessage = null;
    }

    public delegate void ValidateEventHandler(object sender, ValidateEventArgs ea);

    public interface IValidatedControl : IValidator
    {
        /// <summary>
        /// Raised from Validate method. Used for custom validation.
        /// </summary>
        event ValidateEventHandler ServerValidate;

        /// <summary>
        /// Determines if control validates itself. 
        /// If true control is added to Page.Validators collestion.
        /// </summary>
        bool PerformValidation { get; set; }

        /// <summary>
        /// Defines if the error message label shown when control's value is invelid
        /// </summary>
        bool ShowErrorMessage { get; set; }

        /// <summary>
        /// CSS class applied to error message label when control's value is not valid
        /// </summary>
        string ErrorMessageCssClass { get; set; }

        /// <summary>
        /// CSS class applied to control when its value is not valid
        /// </summary>
        string InvalidCssClass { get; set; }
    }
}