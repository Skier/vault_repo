using Dalworth.LeadCentral.Domain;

namespace Dalworth.LeadCentral.Service.Flex
{
    public class UserService
    {
        public User[] GetAll(string ticket)
        {
            return Service.UserService.GetAll(ticket).ToArray();
        }

        public User Save(string ticket, User user)
        {
            return Service.UserService.Save(ticket, user);
        }

        public void InviteUser(string ticket, User user, string message)
        {
            Service.UserService.SendInvitation(ticket, user, message);
        }

        public User GetUser(string ticket, string qbUserId)
        {
            return Service.UserService.GetUserByQbUserId(ticket, qbUserId);
        }

    }
}