using System;
using System.Data;
using System.Collections.Generic;
using Dalworth.Server.Data;
  
namespace Dalworth.Server.Domain
{
    public partial class QbCustomer
    {
        #region ProjectInsurance

        public ProjectInsurance ProjectInsurance { get; set; }

        #endregion

        #region Properties

        public string FirstLastName
        {
            get
            {
                string customerName = string.Empty;

                if (!string.IsNullOrEmpty(FirstName))
                    customerName += FirstName + " ";

                customerName += LastName;
                return customerName;
            }
        }

        #endregion

        #region Constructor

        public QbCustomer()
        {
            ID = -1;
        }

        #endregion

        #region Find

        private const String SqlSelectByCustomerId = SqlSelectAll + 
          @" where sublevel = ?SublevelId and customerId = ?CustomerId";

        public static QbCustomer FindParent(int customerId, IDbConnection connection)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByCustomerId, connection))
            {
                Database.PutParameter(dbCommand, "?CustomerId", customerId);
                Database.PutParameter(dbCommand, "?SublevelId", 0);

                using(IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    if(dataReader.Read())
                    return Load(dataReader);
                }
            }

            throw new DataNotFoundException("QbCustomer not found, search by primary key");
        }

        public static List<QbCustomer> FindProjects(int customerId, IDbConnection connection)
        {
            List<QbCustomer> result = new List<QbCustomer>();
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByCustomerId, connection))
            {
                Database.PutParameter(dbCommand, "?CustomerId", customerId);
                Database.PutParameter(dbCommand, "?SublevelId", 1);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        QbCustomer customer = Load(dataReader);
                        result.Add(customer);
                    }
                }
            }
            return result;
        }

        private const String SqlSelectByProjectId = SqlSelectAll +
         @" where projectId = ?ProjectId";

        public static QbCustomer FindByProjectId(int projectId, IDbConnection connection)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByProjectId, connection))
            {
                Database.PutParameter(dbCommand, "?ProjectId", projectId);
              
                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                        return Load(dataReader);
                }
            }

            throw new DataNotFoundException("QbCustomer not found, search by primary key");
        }
            
        private const String SqlFindByListId = SqlSelectAll +
            @" where listid = ?ListId";

        public static QbCustomer FindByListId(string listId, IDbConnection connection)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByListId, connection))
            {
                Database.PutParameter(dbCommand, "?ListId", listId);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                    {
                        return Load(dataReader);
                    }
                }

                throw new DataNotFoundException("Customer Not Found");
            }
        }

        #endregion

        #region InitializeNonQbFields

        public void InitializeNonQbFields(QbCustomer qbCustomer)
        {
            this.CustomerId = qbCustomer.CustomerId;
            this.ID = qbCustomer.ID;
            this.ProjectId = qbCustomer.ProjectId;
        }

        #endregion

        #region Fill

        public void Fill(Customer customer, Address address)
        {
            CustomerId = customer.ID;
            SubLevel = 0;

            if (string.IsNullOrEmpty(FullName))
            {
                string firstName = customer.FirstName.Trim();
                if (firstName != string.Empty)
                    firstName = ", " + firstName;

                Name = customer.LastName.Trim() + firstName;
                FullName = customer.LastName.Trim() + firstName;
            }

            FirstName = customer.FirstName.Trim();
            LastName = customer.LastName.Trim(); ;
            Phone1 = customer.Phone1;
            Phone2 = customer.Phone2;
            Email = customer.Email;
            BillingAddressAddr1 = address.Address1;
            BillingAddressAddr2 = address.Address2;
            BillingAddressCity = address.City;
            BillingAddressState = address.State;
            BillingAddressPostalCode = address.Zip.ToString();
            IsActive = true;
        }

        public void Fill(Customer customer, Address address, Project project)
        {
            CustomerId = customer.ID;
            ProjectId = project.ID;
            SubLevel = 1;
            
            if (string.IsNullOrEmpty(FullName))
            {
                if (project.ProjectType == ProjectTypeEnum.BasementSystems ||
                    project.ProjectType == ProjectTypeEnum.Construction || 
                    project.ProjectType == ProjectTypeEnum.Content
                    )
                {
                    ProjectConstructionDetail constructionDetail = ProjectConstructionDetail.FindByPrimaryKey(project.ID,null);
                    Name = constructionDetail.JobNumber;
                }
                else
                    Name = project.ID.ToString();
            }

            FirstName = customer.FirstName.Trim();
            LastName = customer.LastName.Trim(); ;
            Phone1 = customer.Phone1;
            Phone2 = customer.Phone2;
            Email = customer.Email;

            BillingAddressAddr1 = address.Address1;
            BillingAddressAddr2 = address.Address2;
            BillingAddressCity = address.City;
            BillingAddressState = address.State;
            BillingAddressPostalCode = address.Zip.ToString();

            ShippingAddressAddr1 = address.Address1;
            ShippingAddressAddr2 = address.Address2;
            ShippingAddressCity = address.City;
            ShippingAddressState = address.State;
            ShippingAddressPostalCode = address.Zip.ToString();
            QbCustomerTypeListId = project.QbCustomerTypeListId;
            QbSalesRepListId = project.QbSalesRepListId;
            
            IsActive = true;
        }

        #endregion
    }
}
      