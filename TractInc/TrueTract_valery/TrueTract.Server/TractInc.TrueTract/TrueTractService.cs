using System.Collections.Generic;
using System.Data.SqlClient;
using TractInc.TrueTract.Data;
using TractInc.TrueTract.Entity;

namespace TractInc.TrueTract
{
public class TrueTractService
{

    public List<ClientInfo> GetClientListByUser(int userId)
    {
        List<ClientInfo> result;

        using (SqlConnection conn = SQLHelper.CreateConnection())
        {
            conn.Open();

            SqlTransaction tran = conn.BeginTransaction();

            result = (new ClientDataMapper()).GetByUser(tran, userId);

            tran.Commit();
        }

        return result;
    }

    public List<UserGroupInfo> GetGroupListByUser(int userId)
    {
        List<UserGroupInfo> result;

        using (SqlConnection conn = SQLHelper.CreateConnection())
        {
            conn.Open();

            SqlTransaction tran = conn.BeginTransaction();

            result = (new GroupDataMapper()).SelectByUserId(tran, userId);

            tran.Commit();
        }
        
        return result;
    }

    public void SaveUserGroup(int groupId, string groupName)
    {
        using (SqlConnection conn = SQLHelper.CreateConnection())
        {
            conn.Open();

            SqlTransaction tran = conn.BeginTransaction();

            (new GroupDataMapper()).Update(tran, groupId, groupName);

            tran.Commit();
        }
    }

    public int CreateUserGroup(string groupName, int userId)
    {
        int groupId;
        
        using (SqlConnection conn = SQLHelper.CreateConnection())
        {
            conn.Open();

            SqlTransaction tran = conn.BeginTransaction();

            groupId = (new GroupDataMapper()).Create(tran, groupName);
            (new GroupUserDataMapper()).Create(tran, userId, groupId);

            tran.Commit();
        }
        
        return groupId;
    }

    public void DeleteUserGroup(int groupId)
    {
        using (SqlConnection conn = SQLHelper.CreateConnection())
        {
            conn.Open();

            SqlTransaction tran = conn.BeginTransaction();

            (new GroupDataMapper()).Delete(tran, groupId);

            tran.Commit();
        }
    }

    public void AddDocumentToGroup(int groupId, string docBranchUid)
    {
        using (SqlConnection conn = SQLHelper.CreateConnection())
        {
            conn.Open();

            SqlTransaction tran = conn.BeginTransaction();

            GroupItemDataMapper groupItemDM = new GroupItemDataMapper();

            if (!groupItemDM.IsGroupContains(tran, groupId, docBranchUid))
            {
                groupItemDM.Create(tran, groupId, docBranchUid);
            }

            tran.Commit();
        }
    }

    public void RemoveDocumentFromGroup(int groupId, string docBranchUid)
    {
        using (SqlConnection conn = SQLHelper.CreateConnection())
        {
            conn.Open();

            SqlTransaction tran = conn.BeginTransaction();

            GroupItemDataMapper groupItemDM = new GroupItemDataMapper();
            groupItemDM.Delete(tran, groupId, docBranchUid);
            
            tran.Commit();
        }
    }
    
    public List<SearchItemInfo> SearchAll(string searchString)
    {
        List<SearchItemInfo> result;
        
        using (SqlConnection conn = SQLHelper.CreateConnection())
        {
            conn.Open();

            SqlTransaction tran = conn.BeginTransaction();

            SearchItemDataMapper searchDM = new SearchItemDataMapper();

            result = searchDM.SearchAll(tran, searchString);

            tran.Commit();
        }

        return result;
    }

    public List<SearchItemInfo> SearchByClient(string searchString, int clientId)
    {
        List<SearchItemInfo> result;

        using (SqlConnection conn = SQLHelper.CreateConnection())
        {
            conn.Open();

            SqlTransaction tran = conn.BeginTransaction();

            SearchItemDataMapper searchDM = new SearchItemDataMapper();

            result = searchDM.SearchProjects(tran, searchString, clientId);

            result.AddRange(searchDM.SearchDocuments(tran, searchString, clientId));

            tran.Commit();
        }

        return result;
    }
    
    public SearchItemInfo SaveSearchItem(SearchItemInfo searchItem)
    {
        SearchItemInfo result;

        using (SqlConnection conn = SQLHelper.CreateConnection())
        {
            conn.Open();

            SqlTransaction tran = conn.BeginTransaction();

            SearchItemDataMapper searchDM = new SearchItemDataMapper();

            result = searchDM.Save(tran, searchItem);

            tran.Commit();
        }

        return result;
    }

    public SearchItemInfo SaveSearchItem(object item)
    {
        SearchItemInfo result;

        using (SqlConnection conn = SQLHelper.CreateConnection())
        {
            conn.Open();

            SqlTransaction tran = conn.BeginTransaction();

            SearchItemDataMapper searchDM = new SearchItemDataMapper();

            result = searchDM.Save(tran, item);

            tran.Commit();
        }

        return result;
    }

    public SearchItemInfo GetSearchItemByItem(object item)
    {
        SearchItemInfo result;

        using (SqlConnection conn = SQLHelper.CreateConnection())
        {
            conn.Open();

            SqlTransaction tran = conn.BeginTransaction();

            SearchItemDataMapper searchDM = new SearchItemDataMapper();

            result = searchDM.GetByItem(tran, item);

            tran.Commit();
        }

        return result;
    }

}
}
