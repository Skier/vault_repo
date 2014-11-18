using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Servman.Domain;
using Servman.Service;

namespace Servman.Intuit.Weborb
{
    public class UserService
    {
        public User[] GetAll()
        {
            return Service.UserService.GetAll(ContextHelper.GetCurrentCustomer()).ToArray();
        }

        public User Save(User user)
        {
            return Service.UserService.Save(user, ContextHelper.GetCurrentCustomer());
        }

        public void SendInvitation(User user)
        {
            Service.UserService.SendInvitation(user, ContextHelper.GetCurrentCustomer());
        }
    }
}