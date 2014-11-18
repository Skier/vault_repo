using System;
using System.Data;
using System.Collections.Generic;

using Dalworth.LeadCentral.Domain;

namespace Dalworth.LeadCentral.Service
{
    public class ActivityService
    {
        public static List<ActivityLog> FindLast(int limit, IDbConnection connection)
        {
                var result = ActivityLog.GetLast(limit, connection);

                foreach (var log in result)
                {
                    if (log.UserId != null)
                        log.RelatedUser = User.FindByPrimaryKey(log.UserId.Value, connection);
                }

                return result;
        }

        public static List<ActivityLog> FindByUserId(int userId, IDbConnection connection)
        {
                var result = ActivityLog.GetByUserId(userId, connection);
                var user = User.FindByPrimaryKey(userId, connection);

                foreach (var log in result)
                {
                    log.RelatedUser = user;
                }

                return result;
        }


        public static void Log(string notes, int? userId, IDbConnection connection)
        {
            var activityLog = new ActivityLog
                                    {
                                        ActivityNotes = notes, 
                                        DateCreated = DateTime.Now,
                                        UserId = userId
                                    };
            ActivityLog.Insert(activityLog, connection);
        }

        public static void Log(string notes, IDbConnection connection)
        {
            var user = ContextHelper.GetCurrentUser();
            var activityLog = new ActivityLog
                                      {
                                          ActivityNotes = notes, 
                                          DateCreated = DateTime.Now,
                                          UserId = user.Id
                                      };

            ActivityLog.Insert(activityLog, connection);
        }
    }
}
