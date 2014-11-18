using System;
using System.Data;
using SmartSchedule.Data;

namespace SmartSchedule.Domain
{
    public partial class BucketConstraint
    {
        public BucketConstraint(){ }

        #region IsBucketFull

        public static bool IsBucketFull(string zip, TimeFrame frame)
        {
            //temporary disabled
            return false;

//            int dummy;
//            if (GetFreeBucketCapacity(zip, frame, out dummy) <= 0)
//                return true;
//            return false;
        }

        #endregion

        #region GetFreeBucketCapacity

        private const string SqlGetFreeBucketCapacity =
            @"select MaxBucketCapacity, MaxBucketCapacity - (SELECT count(*) FROM Visit v
                where v.TechnicianId is null and v.Zip = ?Zip and v.TimeFrameId = ?TimeFrameId 
                and SnapshotDate is null) 
             from BucketConstraint bc
                where bc.AreaId = (SELECT AreaId from ZipCode where Zip = ?Zip) and bc.TimeFrameId = ?TimeFrameId";

        public static int GetFreeBucketCapacity(string zip, TimeFrame frame, out int totalCapacity)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlGetFreeBucketCapacity))
            {
                Database.PutParameter(dbCommand, "?Zip", zip);
                Database.PutParameter(dbCommand, "?TimeFrameId", frame.ID);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                    {
                        totalCapacity = dataReader.GetInt32(0);
                        return dataReader.GetInt32(1);
                    }
                        
                }
            }

            totalCapacity = 0;
            return 0;
        }

        #endregion
    }
}
      