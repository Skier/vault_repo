using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TractInc.TrueTract.Entity;

namespace TractInc.TrueTract.Data
{
internal class ProjectTabContactDataMapper
{

    #region Constants

    private const string SQL_GET_BY_PPROJECT_TAB_ID = @"
        SELECT * 
          FROM TT_ProjectTabContact 
         WHERE ProjectTabId = @ProjectTabId
    ";

    private const string SQL_GET_BY_PPROJECT_ID = @"
        SELECT c.* 
          FROM TT_ProjectTabContact c 
            INNER JOIN TT_ProjectTab t on c.ProjectTabId = t.ProjectTabId
         WHERE t.ProjectId = @ProjectId
    ";

    private const string SQL_CREATE = @"
        INSERT INTO [TT_ProjectTabContact]
                   ([ProjectTabId], [ContactType],
                   [ContactName], [FirstName], [MiddleName], [LastName],
                   [EntityRelationship], [PhoneNumber], [Email],
                   [IsActive], [IsEntity])
             VALUES (
                   @ProjectTabId,
                   @ContactType,
                   @ContactName,
                   @FirstName,
                   @MiddleName,
                   @LastName,
                   @EntityRelationship,
                   @PhoneNumber,
                   @Email,
                   @IsActive,
                   @IsEntity)

        SELECT scope_identity();
    ";

    private const string SQL_UPDATE = @"
        UPDATE [TT_ProjectTabContact] set 
            ProjectTabId = @ProjectTabId,
            ContactType = @ContactType,
            ContactName = @ContactName,
            FirstName = @FirstName,
            MiddleName = @MiddleName,
            LastName = @LastName,
            EntityRelationship = @EntityRelationship,
            PhysicalAddress = @PhysicalAddressId,
            MailingAddress = @MailingAddressId,
            PhoneNumber = @PhoneNumber,
            Email = @Email,
            IsActive = @IsActive,
            IsEntity = @IsEntity
         WHERE ProjectTabContactId = @ProjectTabContactId";
    
    private const string SQL_DELETE = @"
        DELETE [TT_ProjectTabContact] WHERE ProjectTabContactId = @ProjectTabContactId
    ";
    
    #endregion

    #region Fields

    private AddressDataMapper m_addressDM;

    #endregion

    #region Methods

    public List<ProjectTabContactInfo> GetByProjectTabId(SqlTransaction tran, int projectTabId)
    {
        List<SqlParameter> paramList = new List<SqlParameter>();

        paramList.Add(new SqlParameter("@ProjectTabId", projectTabId));
        
        return Select(tran, SQL_GET_BY_PPROJECT_TAB_ID, paramList);
    }

    public List<ProjectTabContactInfo> GetByProjectId(SqlTransaction tran, int projectId)
    {
        List<SqlParameter> paramList = new List<SqlParameter>();

        paramList.Add(new SqlParameter("@ProjectId", projectId));

        return Select(tran, SQL_GET_BY_PPROJECT_ID, paramList);
    }

    public ProjectTabContactInfo Create(SqlTransaction tran, ProjectTabContactInfo projectTabContact)
    {
        List<SqlParameter> paramList = new List<SqlParameter>();

        paramList.Add(new SqlParameter("@ProjectTabId", projectTabContact.ProjectTabId));
        paramList.Add(new SqlParameter("@ContactType", projectTabContact.ContactType));
        paramList.Add(new SqlParameter("@ContactName", projectTabContact.ContactName));
        paramList.Add(new SqlParameter("@FirstName", projectTabContact.FirstName));
        paramList.Add(new SqlParameter("@MiddleName", projectTabContact.MiddleName));
        paramList.Add(new SqlParameter("@LastName", projectTabContact.LastName));
        paramList.Add(new SqlParameter("@EntityRelationship", projectTabContact.EntityRelationship));
        paramList.Add(new SqlParameter("@PhoneNumber", projectTabContact.PhoneNumber));
        paramList.Add(new SqlParameter("@Email", projectTabContact.Email));
        paramList.Add(new SqlParameter("@IsActive", projectTabContact.IsActive));
        paramList.Add(new SqlParameter("@IsEntity", projectTabContact.IsEntity));

        projectTabContact.ProjectTabContactId = int.Parse(
            SQLHelper.ExecuteScalar(tran, CommandType.Text, SQL_CREATE, paramList.ToArray()).ToString());
        
        if (projectTabContact.PhysicalAddress != null)
        {
            projectTabContact.PhysicalAddress = AddressDM.Create(tran, projectTabContact.PhysicalAddress);
        }

        if (projectTabContact.MailingAddress != null)
        {
            projectTabContact.MailingAddress = AddressDM.Create(tran, projectTabContact.MailingAddress);
        }

        Update(tran, projectTabContact);

        return projectTabContact;
    }

    public void Update(SqlTransaction tran, ProjectTabContactInfo projectTabContact)
    {
        List<SqlParameter> paramList = new List<SqlParameter>();

        if (projectTabContact.PhysicalAddress != null)
        {
            if (projectTabContact.PhysicalAddress.AddressId == 0)
                projectTabContact.PhysicalAddress = AddressDM.Create(tran, projectTabContact.PhysicalAddress);
            else
                AddressDM.Update(tran, projectTabContact.PhysicalAddress);
        }

        if (projectTabContact.MailingAddress != null)
        {
            if (projectTabContact.MailingAddress.AddressId == 0)
                projectTabContact.MailingAddress = AddressDM.Create(tran, projectTabContact.MailingAddress);
            else
                AddressDM.Update(tran, projectTabContact.MailingAddress);
        }

        paramList.Add(new SqlParameter("@ProjectTabContactId", projectTabContact.ProjectTabContactId));
        paramList.Add(new SqlParameter("@ProjectTabId", projectTabContact.ProjectTabId));
        paramList.Add(new SqlParameter("@ContactType", projectTabContact.ContactType));
        paramList.Add(new SqlParameter("@ContactName", projectTabContact.ContactName));
        paramList.Add(new SqlParameter("@FirstName", projectTabContact.FirstName));
        paramList.Add(new SqlParameter("@MiddleName", projectTabContact.MiddleName));
        paramList.Add(new SqlParameter("@LastName", projectTabContact.LastName));
        paramList.Add(new SqlParameter("@EntityRelationship", projectTabContact.EntityRelationship));
        paramList.Add(new SqlParameter("@PhoneNumber", projectTabContact.PhoneNumber));
        paramList.Add(new SqlParameter("@Email", projectTabContact.Email));
        paramList.Add(new SqlParameter("@IsActive", projectTabContact.IsActive));
        paramList.Add(new SqlParameter("@IsEntity", projectTabContact.IsEntity));

        if (projectTabContact.PhysicalAddress != null)
            paramList.Add(new SqlParameter("@PhysicalAddressId", projectTabContact.PhysicalAddress.AddressId));
        else
            paramList.Add(new SqlParameter("@PhysicalAddressId", null));

        if (projectTabContact.MailingAddress != null)
            paramList.Add(new SqlParameter("@MailingAddressId", projectTabContact.MailingAddress.AddressId));
        else
            paramList.Add(new SqlParameter("@MailingAddressId", null));

        SQLHelper.ExecuteNonQuery(tran, CommandType.Text, SQL_UPDATE, paramList.ToArray());
    }
    
    public void Delete(SqlTransaction tran, ProjectTabContactInfo projectTabContact)
    {
        List<SqlParameter> paramList = new List<SqlParameter>();

        paramList.Add(new SqlParameter("@ProjectTabContactId", projectTabContact.ProjectTabContactId));

        SQLHelper.ExecuteNonQuery(tran, CommandType.Text, SQL_DELETE, paramList.ToArray());

        AddressDM.Delete(tran, projectTabContact.PhysicalAddress);
        AddressDM.Delete(tran, projectTabContact.MailingAddress);
        
    }    

    private List<ProjectTabContactInfo> Select(SqlTransaction tran, string sql, List<SqlParameter> paramList)
    {
        List<ProjectTabContactInfo> result = new List<ProjectTabContactInfo>();

        using (SqlDataReader dataReader = SQLHelper.ExecuteReader(tran, CommandType.Text, sql, paramList.ToArray()))
        {
            while (dataReader.Read())
            {
                ProjectTabContactInfo info = new ProjectTabContactInfo();
                
                info.ProjectTabContactId = dataReader.GetInt32(dataReader.GetOrdinal("ProjectTabContactId"));
                info.ProjectTabId = dataReader.GetInt32(dataReader.GetOrdinal("ProjectTabId"));
                info.ContactType = dataReader.GetSqlString(dataReader.GetOrdinal("ContactType")).ToString();
                info.ContactName = dataReader.GetSqlString(dataReader.GetOrdinal("ContactName")).ToString();
                info.FirstName = dataReader.GetSqlString(dataReader.GetOrdinal("FirstName")).ToString();
                info.MiddleName = dataReader.GetSqlString(dataReader.GetOrdinal("MiddleName")).ToString();
                info.LastName = dataReader.GetSqlString(dataReader.GetOrdinal("LastName")).ToString();
                info.EntityRelationship = dataReader.GetSqlString(dataReader.GetOrdinal("EntityRelationship")).ToString();
                
                info.PhysicalAddressId = dataReader.GetInt32(dataReader.GetOrdinal("PhysicalAddress"));
                info.MailingAddressId = dataReader.GetInt32(dataReader.GetOrdinal("MailingAddress"));

                info.PhoneNumber = dataReader.GetSqlString(dataReader.GetOrdinal("PhoneNumber")).ToString();
                info.Email = dataReader.GetSqlString(dataReader.GetOrdinal("Email")).ToString();
                info.IsActive = dataReader.GetSqlBoolean(dataReader.GetOrdinal("IsActive")).IsTrue;
                info.IsEntity = dataReader.GetSqlBoolean(dataReader.GetOrdinal("IsEntity")).IsTrue;

                result.Add(info);
            }
        }
        
        foreach (ProjectTabContactInfo contact in result)
        {
            contact.PhysicalAddress = AddressDM.GetById(tran, contact.PhysicalAddressId);
            contact.MailingAddress = AddressDM.GetById(tran, contact.MailingAddressId);
        }

        return result;
    }
    
    #endregion

    #region Properties

    private AddressDataMapper AddressDM
    {
        get
        {
            if (null == m_addressDM)
            {
                m_addressDM = new AddressDataMapper();
            }

            return m_addressDM;
        }
    }

    #endregion
}
}
