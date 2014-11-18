using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TractInc.TrueTract.Entity;

namespace TractInc.TrueTract.Data
{
internal class AddressDataMapper
{

    #region Constants

    private const string SQL_GET_BY_ID = @"
        SELECT a.*, s.[Name] as StateName 
          FROM Address a
            INNER JOIN State s ON s.StateId = a.State
         WHERE a.AddressId = @AddressId
    ";

    private const string SQL_CREATE = @"
        INSERT INTO [Address]
                   ([Address1], [Address2], [City], [State], [Zip])
             VALUES (
                   @Address1,
                   @Address2,
                   @City,
                   @State,
                   @Zip)

        SELECT scope_identity();
    ";

    private const string SQL_UPDATE = @"
        UPDATE [Address] set 
            Address1 = @Address1,
            Address2 = @Address2,
            City = @City,
            State = @State,
            Zip = @Zip
        WHERE AddressId = @AddressId";
    
    private const string SQL_DELETE = @"
        DELETE [Address] WHERE AddressId = @AddressId
    ";
    
    #endregion

    #region Methods

    public AddressInfo GetById(SqlTransaction tran, int addressId)
    {
        List<SqlParameter> paramList = new List<SqlParameter>();

        paramList.Add(new SqlParameter("@AddressId", addressId));
        
        List<AddressInfo> result = Select(tran, SQL_GET_BY_ID, paramList);
        
        return result.Count > 0 ? result[0] : null;
    }

    public AddressInfo Create(SqlTransaction tran, AddressInfo address)
    {
        List<SqlParameter> paramList = new List<SqlParameter>();

        paramList.Add(new SqlParameter("@Address1", address.Address1));
        paramList.Add(new SqlParameter("@Address2", address.Address2));
        paramList.Add(new SqlParameter("@City", address.City));
        paramList.Add(new SqlParameter("@State", address.State));
        paramList.Add(new SqlParameter("@Zip", address.Zip));

        address.AddressId = int.Parse(
            SQLHelper.ExecuteScalar(tran, CommandType.Text, SQL_CREATE, paramList.ToArray()).ToString());

        return address;
    }

    public void Update(SqlTransaction tran, AddressInfo address)
    {
        List<SqlParameter> paramList = new List<SqlParameter>();

        paramList.Add(new SqlParameter("@AddressId", address.AddressId));
        paramList.Add(new SqlParameter("@Address1", address.Address1));
        paramList.Add(new SqlParameter("@Address2", address.Address2));
        paramList.Add(new SqlParameter("@City", address.City));
        paramList.Add(new SqlParameter("@State", address.State));
        paramList.Add(new SqlParameter("@Zip", address.Zip));

        SQLHelper.ExecuteNonQuery(tran, CommandType.Text, SQL_UPDATE, paramList.ToArray());
    }

    public void Delete(SqlTransaction tran, AddressInfo address)
    {
        List<SqlParameter> paramList = new List<SqlParameter>();

        paramList.Add(new SqlParameter("@AddressId", address.AddressId));

        SQLHelper.ExecuteNonQuery(tran, CommandType.Text, SQL_DELETE, paramList.ToArray());
    }

    private List<AddressInfo> Select(SqlTransaction tran, string sql, List<SqlParameter> paramList)
    {
        List<AddressInfo> result = new List<AddressInfo>();

        using (SqlDataReader dataReader = SQLHelper.ExecuteReader(tran, CommandType.Text, sql, paramList.ToArray()))
        {
            while (dataReader.Read())
            {
                AddressInfo info = new AddressInfo();
                info.AddressId = dataReader.GetInt32(dataReader.GetOrdinal("AddressId"));
                info.Address1 = dataReader.GetSqlString(dataReader.GetOrdinal("Address1")).ToString();
                info.Address2 = dataReader.GetSqlString(dataReader.GetOrdinal("Address2")).ToString();
                info.City = dataReader.GetSqlString(dataReader.GetOrdinal("City")).ToString();
                info.State = dataReader.GetInt32(dataReader.GetOrdinal("State"));
                info.Zip = dataReader.GetSqlString(dataReader.GetOrdinal("Zip")).ToString();
                info.StateName = dataReader.GetSqlString(dataReader.GetOrdinal("StateName")).ToString();
                result.Add(info);
            }
        }

        return result;
    }
    
    #endregion

}
}
