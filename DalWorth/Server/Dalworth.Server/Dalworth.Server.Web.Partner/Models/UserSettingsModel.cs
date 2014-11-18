using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Dalworth.Server.Data;
using Dalworth.Server.Domain;

namespace Dalworth.Server.Web.Partner.Models
{
    public class PersonalInfoModel
    {
        public PersonalInfoModel()
        {
            WebUser webUser = PartnerMembershipProvider.GetCurrentUser();

            if (webUser.OrderSourceId.HasValue)
            {
                using (IDbConnection connection = Connection.Instance.GetTemporaryDbConnection())
                {
                    connection.Open();
                    m_orderSource = Domain.OrderSource.FindByPrimaryKey(
                        webUser.OrderSourceId.Value, connection).Name;
                }                                
            }

            Email = webUser.Login;
            FirstName = webUser.FirstName;
            LastName = webUser.LastName;
        }

        private string m_orderSource;
        public string OrderSource
        {
            get { return m_orderSource; }
        }

        [Required(ErrorMessage = "Please enter email address")]
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Please enter valid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter Last Name")]
        public string LastName { get; set; }

        public string Save()
        {
            WebUser webUser = PartnerMembershipProvider.GetCurrentUser();

            webUser.FirstName = FirstName;
            webUser.LastName = LastName;

            bool isLoginChanged = false;
            if (webUser.Login != Email)
            {
                webUser.Login = Email;
                webUser.Email = Email;
                isLoginChanged = true;
            }

            using (IDbConnection connection = Connection.Instance.GetTemporaryDbConnection())
            {
                connection.Open();
                WebUser existingUser = WebUser.FindByLogin(webUser.Login, connection);
                if (existingUser != null && existingUser.ID != webUser.ID)
                {
                    return string.Format("Email address {0} is already used by another user. Please enter another email address",
                        Email);
                }

                if (isLoginChanged)
                {   
                    FormsAuthenticationService formsService = new FormsAuthenticationService();
                    formsService.SignOut();
                    formsService.SignIn(webUser.Login, false);                    
                }                    

                WebUser.Update(webUser, connection);
                return string.Empty;
            }
        }
    }

    public class PhoneInfoModel
    {
        public PhoneInfoModel()
        {
            WebUser webUser = PartnerMembershipProvider.GetCurrentUser();
            if (!webUser.OrderSourceId.HasValue)
                return;

            using (IDbConnection connection = Connection.Instance.GetTemporaryDbConnection())
            {
                connection.Open();
                TrackingPhones = Domain.TrackingPhone.FindByOrderSource(webUser.OrderSourceId.Value, connection);
                OwnPhones = OrderSourceOwnPhone.FindByOrderSource(webUser.OrderSourceId.Value, connection);
            }
        }

        public List<Domain.TrackingPhone> TrackingPhones { get; set; }
        public List<OrderSourceOwnPhone> OwnPhones { get; set; }

        public void DeleteOwnPhone(int id)
        {
            using (IDbConnection connection = Connection.Instance.GetTemporaryDbConnection())
            {
                connection.Open();
                OrderSourceOwnPhone.Delete(new OrderSourceOwnPhone(id), connection);
            }
        }

        public string AddOwnPhone(string phoneNumber)
        {            
            using (IDbConnection connection = Connection.Instance.GetTemporaryDbConnection())
            {
                connection.Open();
                if (!string.IsNullOrEmpty(phoneNumber))
                    phoneNumber = string.Join(null, System.Text.RegularExpressions.Regex.Split(phoneNumber, "[^\\d]"));
                OrderSourceOwnPhone duplicatedPhone = OrderSourceOwnPhone.FindByPhoneNumber(
                    phoneNumber, connection);
                WebUser currentUser = PartnerMembershipProvider.GetCurrentUser();
                if (duplicatedPhone != null)
                {                    
                    if (currentUser.OrderSourceId == duplicatedPhone.OrderSourceId)
                        return "Phone number is duplicated.";
                    return string.Format("Unable to add new Personal phone. Phone number {0} is used by another partner.", phoneNumber);
                }
                OrderSourceOwnPhone newPhone = new OrderSourceOwnPhone(0, currentUser.OrderSourceId.Value, phoneNumber);
                OrderSourceOwnPhone.Insert(newPhone, connection);
                return string.Empty;
            }
        }
    }

    public class OwnPhone
    {
        [Required(ErrorMessage = "Please enter Phone number")]
        [RegularExpression(@"^([0-9]( |-)?)?(\(?[0-9]{3}\)?|[0-9]{3})( |-)?([0-9]{3}( |-)?[0-9]{4}|[a-zA-Z0-9]{7})$",
            ErrorMessage = "Invalid Phone number")]
        public string PhoneNumber { get; set; }
    }
}
