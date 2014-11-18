using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using SmartSchedule.Data;
using SmartSchedule.SDK;

namespace SmartSchedule.Domain
{
    enum VisitNotificationReasonEnum
    {
        TechnicianOff,
        WorkingHours,
        Blockouts
    }

    public class EmailSender
    {
        #region SmtpClient

        private static SmtpClient m_smtpClient;
        private static SmtpClient SmtpClient
        {
            get
            {
                if (m_smtpClient == null)
                {
                    m_smtpClient = new SmtpClient(Configuration.SmtpHost, Configuration.SmtpPort);
                    m_smtpClient.Credentials = new NetworkCredential(Configuration.SmtpLogin, Configuration.SmtpPassword);
                    m_smtpClient.UseDefaultCredentials = false;
                }

                return m_smtpClient;
            }
        }

        #endregion

        #region SendBucketNotifications

        public static void SendBucketNotifications(BookingEngine bookingEngine)
        {
            List<Visit> outOfAreaNotifications = Visit.FindPossibleNotificationsOutOfArea();
            List<Visit> possibleNotifications = new List<Visit>();
            possibleNotifications.AddRange(outOfAreaNotifications);
            possibleNotifications.AddRange(Visit.FindPossibleNotificationsBucket());            
            
            List<string> processedTickets = new List<string>();

            foreach (Visit visit in possibleNotifications)
            {
                if (processedTickets.Contains(visit.TicketNumber))
                    continue;
                processedTickets.Add(visit.TicketNumber);

                visit.ConfirmedTimeFrame = TimeFrame.TimeFrames[visit.TimeFrameId];
                VisitNotificationReasonEnum? notificationReason = null;

                Technician technician = Technician.GetTechnician(
                    visit.ScheduleDate, visit.ExclusiveTechnicianDefaultId.Value);

                if (technician == null)
                    notificationReason = VisitNotificationReasonEnum.TechnicianOff;
                else if (visit.GetAllowedInterval(technician) == null)
                    notificationReason = VisitNotificationReasonEnum.WorkingHours;
                else if (IsBlockoutReason(bookingEngine, technician, visit))
                    notificationReason = VisitNotificationReasonEnum.Blockouts;

                bool isOutOfArea = outOfAreaNotifications.Contains(visit);

                if (!notificationReason.HasValue && !isOutOfArea)
                {
                    visit.TechnicianEmailSentDate = DateTime.Now;
                    visit.IsSecondaryEmailSent = true;
                    Visit.Update(visit);
                    continue;
                }

                TechnicianDefault technicianDefault =
                    Technician.GetTechnicianDefault(visit.ExclusiveTechnicianDefaultId.Value);
                if (technicianDefault.Email == string.Empty)
                {
                    Host.Trace("Email", string.Format("Bucket notification {0} is not sent to {1} because of missing email address", 
                        visit.TicketNumber, technicianDefault.Name));
                    continue;
                }

                MailMessage message = new MailMessage(new MailAddress(Configuration.SmtpFromAddress, Configuration.SmtpDisplayName),
                    new MailAddress(technicianDefault.Email, technicianDefault.Name));

                message.Subject = string.Format("Your exclusive appointment is {0}",
                    notificationReason.HasValue ? "out of the schedule" : "outside your normal service area");
                message.IsBodyHtml = false;
                message.CC.Add(new MailAddress("rick@dalworth.com"));

                string bucketReasonText;
                if (notificationReason == VisitNotificationReasonEnum.TechnicianOff)
                    bucketReasonText = "no business hours established on " + visit.ScheduleDate.ToShortDateString();
                else if (notificationReason == VisitNotificationReasonEnum.WorkingHours)
                    bucketReasonText = "required time frame is out of your business hours";
                else
                    bucketReasonText = "blockout time conflicts with appointment time required by client";

                string[] customerNameSeparated = visit.CustomerName.Split(new [] {", ", " ", ","}, StringSplitOptions.RemoveEmptyEntries);
                string customerName;
                if (customerNameSeparated.Length == 2)
                    customerName = string.Format("{0} {1}", customerNameSeparated[1], customerNameSeparated[0]);
                else
                    customerName = visit.CustomerName;

                message.Body = string.Format(@"{0}, placed a {1} appointment for {2} time frame {3}. ",
                    customerName, visit.Cost.ToString("C"), visit.ScheduleDate.ToShortDateString(),
                    visit.ConfirmedTimeFrame.Text);

                if (notificationReason.HasValue)
                {
                    if (isOutOfArea)
                    {
                        message.Body += string.Format(@"The zip code for this appointment, {0}, is outside your normal service area and could not be placed on your schedule because at the time the appointment was made there was no availability on your schedule ({1}). See the Business Scheduling chapter of the Operations Manual for the options and procedures associated with appointments outside your service area and/or {2}. ",
                            visit.Zip, bucketReasonText, notificationReason == VisitNotificationReasonEnum.Blockouts ? "blockout time" : "business hours");                                                
                    }
                    else
                    {
                        message.Body += string.Format(@"The appointment could not be placed on your schedule because at the time the appointment was made there was no availability on your schedule ({0}). The customer was offered an alternative date and time in which you were available but chose instead to take this date and time. \r\n\r\nNOTE: Disregard this email if the online order indicates you placed the appointment.",
                            bucketReasonText);                        
                    }

                }
                else
                {
                    message.Body += string.Format(@"The zip code for this appointment, {0}, is outside your normal service area. See the Business Scheduling chapter of the Operations Manual for the options and procedures associated with appointments outside your service area. ",
                        visit.Zip);                                        
                }

                try
                {
                    SmtpClient.Send(message);
                    Host.Trace("Email", string.Format("Bucket notification {0} successfully sent to {1}",
                        visit.TicketNumber, technicianDefault.Name));
                    visit.TechnicianEmailSentDate = DateTime.Now;
                    Visit.Update(visit);
                }
                catch (Exception e)
                {
                    Host.Trace("Email", string.Format("Bucket notification {0} is not sent to {1}. Exception is: {2}",
                        visit.TicketNumber, technicianDefault.Name, e.Message + e.StackTrace));                    
                }
            }
        }

        private static bool IsBlockoutReason(BookingEngine bookingEngine, Technician technician, Visit bucketVisit)
        {
            IList<Visit> techVisits = bookingEngine.Visits.GetTechnicianVisits(technician.ID);
            DateTime currentIntervalStart = technician.WorkingIntervals[0].TimeStart;
            int blockoutsCount = 0;
            foreach (var techVisit in techVisits)
            {
                if (techVisit.IsBlockout)
                {
                    TimeInterval newInterval = new TimeInterval(currentIntervalStart, techVisit.TimeStart);
                    TimeInterval trimmedInterval =
                        technician.WorkingIntervals[0].GetInterval().GetIntersectingInterval(newInterval);

                    currentIntervalStart = techVisit.TimeEnd;
                    blockoutsCount++;

                    if (bucketVisit.CanFitInterval(technician, trimmedInterval))
                        return false;
                }
            }

            if (blockoutsCount == 0)
                return false;

            TimeInterval trimmedIntervalLast = new TimeInterval(currentIntervalStart, technician.WorkingIntervals[0].TimeEnd);
            if (trimmedIntervalLast.DurationMin > 0 && bucketVisit.CanFitInterval(technician, trimmedIntervalLast))
                return false;
            return true;
        }

        #endregion

        #region SendSecondaryNotifications

        public static void SendSecondaryNotifications(BookingEngine bookingEngine)
        {
            ApplicationSetting setting = ApplicationSetting.Find()[0];
            if ((DateTime.Now.Hour > 8 && DateTime.Now.Date != setting.LastEmailReportDate.Date)
                || (DateTime.Now.Hour > 15 && DateTime.Now.Date == setting.LastEmailReportDate.Date
                        && setting.LastEmailReportDate.Hour < 15))
            {
                setting.LastEmailReportDate = DateTime.Now;
                ApplicationSetting.Update(setting);

                List<Visit> visits = Visit.FindSecondaryNotificationVisits();
                if (visits.Count == 0)
                    return;

                string tickets = string.Empty;
                foreach (var visit in visits)
                {
                    tickets += string.Format("Ticket {0}, sent {1}. Tech - {2}; Customer - {3}\r\n", visit.TicketNumber,
                        visit.TechnicianEmailSentDate.Value, visit.ExclusiveTechnicianDefault.Name, visit.CustomerName);
                }

                MailMessage message = new MailMessage(new MailAddress(Configuration.SmtpFromAddress, Configuration.SmtpDisplayName),
                    new MailAddress("rick@dalworth.com", "Rick Maloney"));

                message.Subject = "Email report past 24 hours";
                message.IsBodyHtml = false;                
                message.Body = tickets;

                try
                {
                    SmtpClient.Send(message);
                    Host.Trace("Email", string.Format("Email report successfully sent {0}", DateTime.Now));
                    Database.Begin();
                    foreach (var visit in visits)
                    {
                        visit.IsSecondaryEmailSent = true;
                        Visit.Update(visit);
                    }
                    Database.Commit();
                }
                catch (Exception e)
                {
                    if (Database.IsUnderTransaction(ConnectionKeyEnum.Default))
                        Database.Rollback();
                    Host.Trace("Email", string.Format("Email report failed {0}", e.Message + e.StackTrace));
                }                
            }
        }

        #endregion

        #region SendErrorNotifications

        public static void SendErrorNotifications()
        {
            MailMessage message = new MailMessage(new MailAddress(Configuration.SmtpFromAddress, Configuration.SmtpDisplayName),
                new MailAddress("sergei.kalashnikov@gmail.com", "Sergei"));
            message.CC.Add(new MailAddress("bfurman@affilia.com", "Boris"));

            message.Subject = "SmartSchedule error";
            message.IsBodyHtml = false;
            message.Body = "SmartSchedule error. See log for details";

            try
            {
                SmtpClient.Send(message);
                Host.Trace("Email", "Error notification email sent");
            }
            catch (Exception e)
            {
                Host.Trace("Email", string.Format("Error notification email is not sent. Exception is: {0}",
                    e.Message + e.StackTrace));
            }
        }

        #endregion
    }
}
