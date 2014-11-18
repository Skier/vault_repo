using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.InteropServices;
using SmartSchedule.Data;

namespace SmartSchedule.Domain
{
    public partial class Company
    {
        public Company(){ }

        #region GetCompany

        private static Dictionary<int, Company> m_companies;        
        public static Company GetCompany(int id)
        {
            if (m_companies == null)
            {
                m_companies = new Dictionary<int, Company>();
                List<Company> companies = Find();
                foreach (Company company in companies)
                    m_companies.Add(company.ID, company);
            }

            return m_companies[id];
        }

        #endregion        

        #region FindTechniciansCompanies

        private const string SqlFindTechniciansCompanies =
            @"select distinct c.* from Technician t
                inner join Company c on c.ID = t.CompanyId";

        public static List<Company> FindTechniciansCompanies()
        {
            List<Company> result = new List<Company>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindTechniciansCompanies))
            {
                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                        result.Add(Load(dataReader));
                }
            }

            return result;
        }

        #endregion
    }
}
      