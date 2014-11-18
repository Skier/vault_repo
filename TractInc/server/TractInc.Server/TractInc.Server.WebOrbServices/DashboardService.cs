using TractInc.Server.Domain;
using TractInc.Server.Domain.Package.Dashboard;

namespace TractInc.Server.WebOrbServices
{

public class DashboardService
{
    public bool Ping()
    {
        return true;
    }

    public User Login(string login, string password)
    {
        return User.UserLogin(login, password);
    }

    public User SignUp(Person person, string login, string password)
    {
        return User.SignUp(person, login, password);
    }

    public bool RestorePassword(string login)
    {
        return User.SendPassword(login);
    }

    public DashboardPackage GetDashboardPackage(User user) {
        DashboardPackage result = new DashboardPackage();
        
        result.user = User.FindByPrimaryKey(user.UserId);
        result.user.RoleList = Role.findByUser(user);

        result.ModuleList = Module.findByUser(user);

        return result;
    }

}

}
