using System;
using System.Collections;
using System.Collections.Generic;
using TractInc.Server.Domain;
using TractInc.Server.Domain.Package.StaffManager;

namespace TractInc.Server.WebOrbServices
{
public class StaffManagerService
{

    public StaffManagerPackage GetStaffManagerPackage(int userId) {
        User currentUser = User.FindByPrimaryKey(userId);
        if ( null == currentUser ) {
            throw new InvalidOperationException("userId is not valid");
        }

        Company company = Company.findUserCompany(currentUser);

        if ( null == company ) {
            throw new InvalidOperationException("User does not belongs to company.");
        }

        StaffManagerPackage result = new StaffManagerPackage();
        result.StaffCompany = company;
        result.AssetList = Asset.findByCompany(company, true);
        result.TeamList = Team.findByCompany(company, true);
        result.PersonList = Person.findByCompany(company);
        result.ContractList = Contract.findByCompany(company);
        result.ClientList = Client.findByCompany(company);
        result.ProjectList = Project.findByCompany(company);

        result.AssetTypeList = AssetType.Find();
        result.BillItemTypeList = BillItemType.Find();
        
        return result;
    }

    public List<Asset> GetAssetList(int userId) {
        User currentUser = User.FindByPrimaryKey(userId);
        if ( null == currentUser ) {
            throw new InvalidOperationException("userId is not valid");
        }

        Company company = Company.findUserCompany(currentUser);

        if ( null == company ) {
            throw new InvalidOperationException("User does not belongs to company.");
        }

        return Asset.findByCompany(company, true);
    }

    public void SaveAsset(Asset asset) {
        Asset.Save(asset);
    }

    public void RemoveAsset(Asset asset) {
        Asset.Remove(asset);
    }

    public List<Team> GetTeamList(int userId) {
        User currentUser = User.FindByPrimaryKey(userId);
        if ( null == currentUser ) {
            throw new InvalidOperationException("userId is not valid");
        }

        Company company = Company.findUserCompany(currentUser);

        if ( null == company ) {
            throw new InvalidOperationException("User does not belongs to company.");
        }

        return Team.findByCompany(company, true);
    }

    public void SaveTeam(Team team) {
        Team.Save(team);
    }

    public void RemoveTeam(Team team) {
        Team.Remove(team);
    }

    public List<TeamMember> GetTeamMemberList(int teamId) {
        Team team = Team.FindByPrimaryKey(teamId);
        if ( null == team ) {
            throw new InvalidOperationException("teamId is not valid");
        }

        return TeamMember.findByTeam(team, true);
    }

    public void SaveTeamMember(TeamMember teamMember) {
        TeamMember.Save(teamMember);
    }

    public void RemoveTeamMember(TeamMember teamMember) {
        TeamMember.Remove(teamMember);
    }

    public List<AssetRate> GetAssetRateList(int assetId) {
        Asset asset = Asset.FindByPrimaryKey(assetId);
        if ( null == asset ) {
            throw new InvalidOperationException("assetId is not valid");
        }

        return AssetRate.findByAsset(asset);
    }

    public void SaveAssetRates(List<AssetRate> rates) {
        AssetRate.Save(rates);
    }

    public void RemoveAssetRates(List<AssetRate> rates) {
        AssetRate.Remove(rates);
    }

    public List<TeamAssignment> GetTeamAssignmentList(int teamId) {
        Team team = Team.FindByPrimaryKey(teamId);
        if ( null == team ) {
            throw new InvalidOperationException("teamId is not valid");
        }

        return TeamAssignment.findByTeam(team);
    }

    public void SaveTeamAssignment(TeamAssignment teamAssignment) {
        TeamAssignment.Save(teamAssignment);
    }

    public void RemoveTeamAssignment(TeamAssignment teamAssignment) {
        TeamAssignment.Remove(teamAssignment);
    }
}
}
