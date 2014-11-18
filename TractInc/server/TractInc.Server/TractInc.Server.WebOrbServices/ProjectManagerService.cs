using System;
using System.Collections;
using System.Collections.Generic;
using TractInc.Server.Domain;
using TractInc.Server.Domain.Package.ProjectManager;

namespace TractInc.Server.WebOrbServices
{
public class ProjectManagerService
{

    public ProjectManagerPackage GetProjectManagerPackage(int userId, int clientId) {
        User currentUser = User.FindByPrimaryKey(userId);
        if ( null == currentUser ) {
            throw new InvalidOperationException("userId is not valid");
        }

        Company company = Company.findUserCompany(currentUser);

        if ( null == company ) {
            throw new InvalidOperationException("User does not belongs to company.");
        }

        ProjectManagerPackage result = new ProjectManagerPackage();
        result.ProjectStatusList = ProjectStatus.Find();
        result.ClientList = Client.findByCompany(company);
        result.ContractList = Contract.findByCompany(company);
        result.AccountList = Account.findByCompany(company);

        if ( 0 != clientId ) {
            result.ProjectList = Project.findByCompanyAndClient(company, clientId);
        } else {
            result.ProjectList = Project.findByCompany(company);
        }

        result.CanAssignAccount = false;
        foreach (Role role in Role.findByUser(currentUser)) {
            if ( PermissionAssignment.HasPermission(role, 
                    ProjectManagerPackage.PROJECT_MANAGER_ASSIGN_ACCOUNT_PERM_ID) ) {
                result.CanAssignAccount = true;
            }
        }

        return result;
    }

    public List<Project> GetProjectList(int userId) {
        User currentUser = User.FindByPrimaryKey(userId);
        if ( null == currentUser ) {
            throw new InvalidOperationException("userId is not valid");
        }

        Company company = Company.findUserCompany(currentUser);

        if ( null == company ) {
            throw new InvalidOperationException("User does not belongs to company nor to client.");
        }

        return Project.findByCompany(company);
    }

    public void SaveProject(Project project) {
        Project.Save(project);
    }

    public void RemoveProject(Project project) {
        Project.Remove(project);
    }
}
}
