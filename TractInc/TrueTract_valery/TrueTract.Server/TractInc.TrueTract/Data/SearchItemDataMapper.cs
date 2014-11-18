using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TractInc.TrueTract.Entity;

namespace TractInc.TrueTract.Data
{
internal class SearchItemDataMapper
{

    #region Constants

    private const string SQL_GET_BY_ITEM_ID_AND_ITEM_TYPE_ID = @"
        SELECT * 
          FROM SearchItem 
         WHERE ItemId = @ItemId
           AND ItemTypeId = @ItemTypeId
    ";

    private const string SQL_SEARCH = @"
        SELECT * 
          FROM SearchItem 
         WHERE ItemValue LIKE @SearchString
    ";

    private const string SQL_SEARCH_PROJECTS_BY_CLIENT_ID = @"
        SELECT s.* 
          FROM SearchItem s
	        INNER JOIN Project p on s.ItemId = p.ProjectId
         WHERE ItemValue LIKE @SearchString
           AND s.ItemTypeId = 1
	       AND p.ClientId = @ClientId
    ";

    private const string SQL_SEARCH_DOCUMENTS_BY_CLIENT_ID = @"
        SELECT s.* 
          FROM SearchItem s
         WHERE ItemValue LIKE @SearchString
	       AND s.ItemTypeId = 2 
	       AND s.ItemId in (
			        Select distinct d.DocId
			          From [Document] d
				        inner join ProjectTabDocument td on td.DocumentId = d.DocID
				        inner join ProjectTab t on td.ProjectTabId = t.ProjectTabId
				        inner join Project p on t.ProjectId = p.ProjectId
			          where p.ClientId = @ClientId
			        )
    ";

    private const string SQL_GET_BY_ID = @"
        SELECT * 
          FROM SearchItem 
         WHERE SearchItemId = @SearchItemId
    ";

    private const string SQL_CREATE = @"
        INSERT INTO [SearchItem]
                   ([ItemTypeId],[ItemId],[ItemValue],[ItemXmlValue])
             VALUES (
                   @ItemTypeId,
                   @ItemId,
                   @ItemValue,
                   @ItemXmlValue)

        SELECT scope_identity();
    ";

    private const string SQL_UPDATE = @"
        UPDATE [SearchItem] set 
            ItemTypeId = @ItemTypeId,
            ItemId = @ItemId,
            ItemValue = @ItemValue,
            ItemXmlValue = @ItemXmlValue
        WHERE SearchItemId = @SearchItemId";
    
    private const string SQL_DELETE = @"
        DELETE [SearchItem] WHERE SearchItemId = @SearchItemId
    ";

    #endregion

    #region Methods
    
    public List<SearchItemInfo> SearchAll(SqlTransaction tran, string searchString)
    {
        List<SqlParameter> paramList = new List<SqlParameter>();

        paramList.Add(new SqlParameter("@SearchString", "%" + searchString + "%"));

        return Select(tran, SQL_SEARCH, paramList);
    }

    public List<SearchItemInfo> SearchProjects(SqlTransaction tran, string searchString, int clientId)
    {
        List<SqlParameter> paramList = new List<SqlParameter>();

        paramList.Add(new SqlParameter("@SearchString", "%" + searchString + "%"));
        paramList.Add(new SqlParameter("@ClientId", clientId));

        return Select(tran, SQL_SEARCH_PROJECTS_BY_CLIENT_ID, paramList);
    }

    public List<SearchItemInfo> SearchDocuments(SqlTransaction tran, string searchString, int clientId)
    {
        List<SqlParameter> paramList = new List<SqlParameter>();

        paramList.Add(new SqlParameter("@SearchString", "%" + searchString + "%"));
        paramList.Add(new SqlParameter("@ClientId", clientId));

        return Select(tran, SQL_SEARCH_DOCUMENTS_BY_CLIENT_ID, paramList);
    }

    public SearchItemInfo GetByItem(SqlTransaction tran, object item)
    {
        if (item is ProjectInfo)
        {
            ProjectInfo project = (ProjectInfo) item;
            return GetByItemIdAndItemTypeId(tran, project.ProjectId, 1);
        }
        else if (item is DocumentInfo)
        {
            DocumentInfo document = (DocumentInfo)item;
            return GetByItemIdAndItemTypeId(tran, document.DocID, 2);
        }
        else if (item is TractInfo)
        {
            TractInfo tract = (TractInfo)item;
            return GetByItemIdAndItemTypeId(tran, tract.TractId, 3);
        } else
        {
            throw new Exception("Unsupported item type");
        }
    }

    public SearchItemInfo GetById(SqlTransaction tran, int searchItemId)
    {
        List<SearchItemInfo> result;

        List<SqlParameter> paramList = new List<SqlParameter>();

        paramList.Add(new SqlParameter("@SearchItemId", searchItemId));

        result = Select(tran, SQL_GET_BY_ID, paramList);
        
        if (result != null && result.Count > 0)
        {
            return result[0];
        } else
        {
            return null;
        }
    }
    
    public SearchItemInfo Save(SqlTransaction tran, SearchItemInfo searchItem)
    {
        if (GetById(tran, searchItem.SearchItemId) != null)
            return Update(tran, searchItem);
        else
            return Create(tran, searchItem);
    }

    public SearchItemInfo Save(SqlTransaction tran, object item)
    {
        SearchItemInfo searchItem = GetByItem(tran, item);
        
        if (searchItem == null)
        {
            searchItem = new SearchItemInfo();
            searchItem.SearchItemId = 0;
        }

        if (item is ProjectInfo)
        {
            ProjectInfo project = (ProjectInfo)item;
            searchItem.ItemId = project.ProjectId;
            searchItem.ItemTypeId = 1;
            searchItem.ItemValue = project.toSearchString();
            searchItem.ItemXmlValue = project.toXml();
        }
        else if (item is DocumentInfo)
        {
            DocumentInfo document = (DocumentInfo)item;
            searchItem.ItemId = document.DocID;
            searchItem.ItemTypeId = 2;
            searchItem.ItemValue = document.toSearchString();
            searchItem.ItemXmlValue = document.toXml();
        }
        else if (item is TractInfo)
        {
            TractInfo tract = (TractInfo)item;
            searchItem.ItemId = tract.TractId;
            searchItem.ItemTypeId = 3;
            searchItem.ItemValue = tract.toSearchString();
            searchItem.ItemXmlValue = tract.toXml();
        }
        else
        {
            throw new Exception("Unsupported item type");
        }

        return Save(tran, searchItem);
    }

    public SearchItemInfo Create(SqlTransaction tran, SearchItemInfo searchItem)
    {
        List<SqlParameter> paramList = new List<SqlParameter>();

        paramList.Add(new SqlParameter("@ItemTypeId", searchItem.ItemTypeId));
        paramList.Add(new SqlParameter("@ItemId", searchItem.ItemId));
        paramList.Add(new SqlParameter("@ItemValue", searchItem.ItemValue));
        paramList.Add(new SqlParameter("@ItemXmlValue", searchItem.ItemXmlValue));

        searchItem.SearchItemId = int.Parse(
            SQLHelper.ExecuteScalar(tran, CommandType.Text, SQL_CREATE, paramList.ToArray()).ToString());

        return searchItem;
    }

    public SearchItemInfo Update(SqlTransaction tran, SearchItemInfo searchItem)
    {
        List<SqlParameter> paramList = new List<SqlParameter>();

        paramList.Add(new SqlParameter("@ItemTypeId", searchItem.ItemTypeId));
        paramList.Add(new SqlParameter("@ItemId", searchItem.ItemId));
        paramList.Add(new SqlParameter("@ItemValue", searchItem.ItemValue));
        paramList.Add(new SqlParameter("@ItemXmlValue", searchItem.ItemXmlValue));
        paramList.Add(new SqlParameter("@SearchItemId", searchItem.SearchItemId));

        SQLHelper.ExecuteNonQuery(tran, CommandType.Text, SQL_UPDATE, paramList.ToArray());

        return searchItem;
    }

    public void Delete(SqlTransaction tran, SearchItemInfo searchItem)
    {
        List<SqlParameter> paramList = new List<SqlParameter>();

        paramList.Add(new SqlParameter("@SearchItemId", searchItem.SearchItemId));

        SQLHelper.ExecuteNonQuery(tran, CommandType.Text, SQL_DELETE, paramList.ToArray());
    }    

    private List<SearchItemInfo> Select(SqlTransaction tran, string sql, List<SqlParameter> paramList)
    {
        List<SearchItemInfo> result = new List<SearchItemInfo>();

        using (SqlDataReader dataReader = SQLHelper.ExecuteReader(tran, CommandType.Text, sql, paramList.ToArray()))
        {
            while (dataReader.Read())
            {
                SearchItemInfo info = new SearchItemInfo();
                info.SearchItemId = dataReader.GetInt32(dataReader.GetOrdinal("SearchItemId"));
                info.ItemTypeId = dataReader.GetInt32(dataReader.GetOrdinal("ItemTypeId"));
                info.ItemId = dataReader.GetInt32(dataReader.GetOrdinal("ItemId"));
                info.ItemValue = dataReader.GetSqlString(dataReader.GetOrdinal("ItemValue")).ToString();
                info.ItemXmlValue = dataReader.GetSqlString(dataReader.GetOrdinal("ItemXmlValue")).ToString();

                result.Add(info);
            }
        }

        return result;
    }

    private SearchItemInfo GetByItemIdAndItemTypeId(SqlTransaction tran, int itemId, int itemTypeId)
    {
        List<SearchItemInfo> result;

        List<SqlParameter> paramList = new List<SqlParameter>();

        paramList.Add(new SqlParameter("@ItemId", itemId));
        paramList.Add(new SqlParameter("@ItemTypeId", itemTypeId));

        result = Select(tran, SQL_GET_BY_ITEM_ID_AND_ITEM_TYPE_ID, paramList);

        if (result != null && result.Count > 0)
        {
            return result[0];
        }
        else
        {
            return null;
        }
    }

    #endregion
}
}
