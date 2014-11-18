using System;
using System.Collections.Generic;
using System.Data;
using Dalworth.Server.Data;

namespace Dalworth.Server.Domain
{
    public partial class AdvertisingSource
    {
        public AdvertisingSource(){ }

        #region AdvertisingTechnician

        private Employee m_advertisingTechnician;
        public Employee AdvertisingTechnician
        {
            get { return m_advertisingTechnician; }
            set { m_advertisingTechnician = value; }
        }

        #endregion

        #region FindBy Area Active

        public static List<AdvertisingSource> FindByAreaActive(int? areaId)
        {
            string SqlFindByAreaActive =
                @"SELECT * FROM AdvertisingSource WHERE";
            
           SqlFindByAreaActive += " IsActive = 1";
           SqlFindByAreaActive += " and isRestoration = true";
          
            if (areaId.HasValue)
                SqlFindByAreaActive += " and AreaId = " + areaId;

            SqlFindByAreaActive += " order by Name";

            List<AdvertisingSource> result = new List<AdvertisingSource>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByAreaActive))
            {
                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        result.Add(Load(dataReader));
                    }
                }
            }

            return result;
        }

        #endregion

        #region FindByAcronim

        private const string SqlFindByAcronym =
            @"select * from advertisingSource             
             where UPPER(acronym) = ?Acronym";

        public static List<AdvertisingSource> FindByAcronym(string acronym, IDbConnection connection)
        {
            List<AdvertisingSource> result = new List<AdvertisingSource>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByAcronym, connection))
            {
                Database.PutParameter(dbCommand, "?Acronym", acronym.ToUpper());
               
                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        result.Add(Load(dataReader));
                    }
                }
            }

            return result;
        }

        #endregion 

        #region FindBy Visit

        private const string SqlFindByVisit =
            @"select distinct ad.*, e.* From VisitTask vt
                inner join Task t on t.ID = vt.TaskId
                inner join Project p on p.ID = t.ProjectId
                inner join AdvertisingSource ad on ad.ID = p.AdvertisingSourceId
                left join Employee e on e.ID = p.AdvertisingTechnicianId
                where vt.VisitId = ?VisitId";

        public static List<AdvertisingSource> FindByVisit(Visit visit)
        {
            List<AdvertisingSource> result = new List<AdvertisingSource>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByVisit))
            {
                Database.PutParameter(dbCommand, "?VisitId", visit.ID);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        AdvertisingSource adSource = Load(dataReader);

                        if (!dataReader.IsDBNull(FieldsCount))
                            adSource.AdvertisingTechnician = Employee.Load(dataReader, FieldsCount);

                        result.Add(adSource);
                    }                        
                }
            }

            return result;
        }

        #endregion

        #region FindByPartner

        private const string SqlFindByPartner =
            @"SELECT ads.* FROM OrderSourceAdvertisingSource osas
                inner join AdvertisingSource ads on ads.ID = osas.AdvertisingSourceId
                where osas.OrderSourceId = ?OrderSourceId";

        public static List<AdvertisingSource> FindByPartner(int partnerId, IDbConnection connection)
        {
            List<AdvertisingSource> result = new List<AdvertisingSource>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByPartner, connection))
            {
                Database.PutParameter(dbCommand, "?OrderSourceId", partnerId);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                        result.Add(Load(dataReader));
                }
            }

            return result;
        }

        #endregion

        #region FindNotAssigned

        private const string SqlFindNotAssigned =
            @"SELECT ads.* FROM AdvertisingSource ads
                left join OrderSourceAdvertisingSource osas on osas.AdvertisingSourceId = ads.ID
                where osas.OrderSourceId is null and ads.IsActive and ads.Name != ''
                order by ads.Name";

        public static List<AdvertisingSource> FindNotAssigned(IDbConnection connection)
        {
            List<AdvertisingSource> result = new List<AdvertisingSource>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindNotAssigned, connection))
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
