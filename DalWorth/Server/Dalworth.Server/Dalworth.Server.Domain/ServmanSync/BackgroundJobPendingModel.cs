using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.IO;
using Dalworth.Server.SDK;
using Dalworth.Server.Domain.package;
using Database = Dalworth.Server.Data.Database;

namespace Dalworth.Server.Domain.ServmanSync
{
    public class BackgroundJobPendingModel
    {
        private static SmtpClient m_smtpClient;
        private static SmtpClient SmtpClient
        {
            get
            {
                if (m_smtpClient == null)
                {
                    m_smtpClient = new SmtpClient(Configuration.SmtpHost, Configuration.SmtpPort);
                    m_smtpClient.Credentials = new System.Net.NetworkCredential(Configuration.SmtpLogin, Configuration.SmtpPassword);
                    m_smtpClient.UseDefaultCredentials = false;
                }

                return m_smtpClient;
            }
        }

        #region ProcessLateLeadNotification

        public static void ProcessLateLeadNotification()
        {
            Host.Trace("Late Leads", "Process Latest Notification reminders");
            List<Lead> lateLeads = Lead.FindLateLeads(null);

            foreach (Lead lead in lateLeads)
            {
                lead.DateLateNotificationSent = DateTime.Now;

                MailMessage message = new MailMessage(new MailAddress(Configuration.SmtpFromAddress, Configuration.SmtpDisplayName),
                new MailAddress(Configuration.SmtpLateLeadsAddress, Configuration.SmtpLateLeadsDisplayName));

                message.Subject = "Lead not processed for more then 10 minutes";
                message.IsBodyHtml = false;
                message.Body = "";
                message.Body += "Request Detail:\n";
                message.Body += "---------------------------------------------\n";
                message.Body += CreateLeadDetail(lead);
                message.Body += "\n";

                SmtpClient.Send(message);

                Lead.Update(lead);
            }
        }

        #endregion

        #region ProcessCustomerEmailReminders

        public static void ProcessCustomerEmailReminders()
        {
            Host.Trace("Email Remiders", "Started");

            try
            {
                List<ProjectFeedback> feedbacks = ProjectFeedback.FindReminderCandidates();
                foreach (ProjectFeedback feedback in feedbacks)
                {
                    ProcessCustomerReminder(feedback);
                }
            }
            catch (Exception ex)
            {
                Host.Trace("Email Reminders Failed", ex.StackTrace);
                return;
            }

            Host.Trace("Email Remiders", "Finished");
        }

        #endregion
            
        #region ProcessPendingJobs

        public static void ProcessPendingJobs()
        {
            Host.Trace("Pending Jobs", "Start processing");


            List<BackgroundJobPending> jobs = BackgroundJobPending.Find();
            if (jobs.Count == 0)
                Host.Trace("Pending Jobs", "No Pending Jobs found");

            foreach (BackgroundJobPending job in jobs)
            {
                switch (job.BackgroundJobType)
                {
                    case BackgroundJobTypeEnum.LeadRecievedEmail:
                        try
                        {
                            ProcessLeadReceivedEmail(job);
                            BackgroundJobPending.Delete(job);
                            Host.Trace("Pending Jobs", "LeadRecievedEmail job processed, JobID = " + job.ID);
                        }
                        catch (Exception ex)
                        {
                            Host.Trace("Pending Jobs", "LeadRecievedEmail job failed, JobID = " + job.ID + " " + ex.StackTrace);
                        }
                        break;
                    case BackgroundJobTypeEnum.LeadRecievedPrint:
                        try
                        {
                            ProcessLeadRecievedPrint(job);
                            BackgroundJobPending.Delete(job);
                            Host.Trace("Pending Jobs", "LeadRecievedPrint job processed, JobID = " + job.ID);
                        }
                        catch (Exception ex)
                        {
                            Host.Trace("Pending Jobs", "LeadRecievedPrint job failed, JobID = " + job.ID + " " + ex.StackTrace);
                        }
                        break;
                    case BackgroundJobTypeEnum.NotifiyOnCallNewLead:
                        try
                        {
                            NotifiyNewLead(job);
                            BackgroundJobPending.Delete(job);
                            Host.Trace("Pending Jobs", "LeadRecievedPrint job processed, JobID = " + job.ID);
                        }
                        catch (Exception ex)
                        {
                            Host.Trace("Pending Jobs", "LeadRecievedPrint job failed, JobID = " + job.ID + " " + ex.StackTrace);
                        }
                        break;
                    case BackgroundJobTypeEnum.ProjectCompletedEmail:
                        try
                        {
                            ProcessProjectCompletedEmail(job);
                            BackgroundJobPending.Delete(job);
                            Host.Trace("Pending Jobs", "LeadRecievedPrint job processed, JobID = " + job.ID);
                        }
                        catch (WebException ex)
                        {
                            Host.Trace("Pending Jobs", "LeadRecievedPrint job failed, JobID = " + job.ID + " " + ex.StackTrace);
                        }
                        catch (DalworthException ex)
                        {
                            Host.Trace("Pending Jobs", "LeadRecievedPrint job failed, JobID = " + job.ID + " " + ex.StackTrace);
                        }
                        catch (Exception ex)
                        {
                            Host.Trace("Pending Jobs", "LeadRecievedPrint job failed, JobID = " + job.ID + " " + ex.StackTrace);
                        }
                        break;
                    case BackgroundJobTypeEnum.ProjectFeedbackReceived:
                        try
                        {
                            ProcessProjectFeedbackReceived(job);
                            BackgroundJobPending.Delete(job);
                            Host.Trace("Pending Jobs", "FeedbackReceived job processed, JobID = " + job.ID);
                        }
                        catch (WebException ex)
                        {

                            Host.Trace("Pending Jobs", "ProjectFeedbackReceived job failed, JobID = " + job.ID + " " + ex.StackTrace);
                        }
                        catch (DalworthException ex)
                        {
                            Host.Trace("Pending Jobs", "ProjectFeedbackReceived job failed, JobID = " + job.ID + " " + ex.StackTrace);
                        }
                        catch (Exception ex)
                        {
                            Host.Trace("Pending Jobs", "ProjectFeedbackReceived job failed, JobID = " + job.ID + " " + ex.StackTrace);
                        }
                        break;
                    case BackgroundJobTypeEnum.ProjectFeedbackProcessed:
                        try
                        {
                            ProcessProjectFeedbackProcessed(job);
                            BackgroundJobPending.Delete(job);
                        }
                        catch (Exception ex)
                        {
                            Host.Trace("Pending Jobs", "ProjectFeedbackProcessed job failed, JobID = " + job.ID + " " + ex.StackTrace);
                        }
                        break;


                    case BackgroundJobTypeEnum.PartnerSiteInvitation:
                        try
                        {
                            PartnerInvitation invitation = ProcessPartnerSiteInvitation(job);
                            invitation.IsInvitationSent = true;

                            Database.Begin();
                            PartnerInvitation.Update(invitation);
                            BackgroundJobPending.Delete(job);
                            Database.Commit();
                        }
                        catch (Exception ex)
                        {
                            Host.Trace("Pending Jobs", string.Format("PartnerSiteInvitation job failed, JobID = {0}, {1}", job.ID, ex.Message + ex.StackTrace));
                        }
                        break;
                    case BackgroundJobTypeEnum.PartnerSitePasswordReminder:
                        try
                        {
                            
                            PartnerInvitation invitation = ProcessPartnerSitePasswordReminder(job);
                            invitation.IsInvitationSent = true;

                            Database.Begin();
                            PartnerInvitation.Update(invitation);
                            BackgroundJobPending.Delete(job);
                            Database.Commit();
                        }
                        catch (Exception ex)
                        {
                            Database.Rollback();
                            Host.Trace("Pending Jobs", string.Format("PartnerSitePasswordReminder job failed, JobID = {0}, {1}", job.ID, ex.Message + ex.StackTrace));
                        }
                        break;                        
                    case BackgroundJobTypeEnum.PartnerSiteSummaryReport:
                        try
                        {
                            ProcessPartnerSiteSummaryReport();
                            
                            Database.Begin();                            
                            BackgroundJobPending.Delete(job);
                            Database.Commit();
                        }
                        catch (Exception ex)
                        {
                            Database.Rollback();
                            Host.Trace("Pending Jobs", string.Format("PartnerSiteSummaryReport job failed, JobID = {0}, {1}", job.ID, ex.Message + ex.StackTrace));
                        }
                        break;
                }
            }
        }

        #endregion

        #region ProcessCustomerReminder

        private static void ProcessCustomerReminder(ProjectFeedback feedback)
        {
            Project project = Project.FindByPrimaryKey(feedback.ProjectId);
            Customer customer = Customer.FindByPrimaryKey(project.CustomerId.Value);

            if (customer.Email == null)
            {
                feedback.ReminderEmailSentDate = DateTime.Now;
                ProjectFeedback.Update(feedback);
                return;
            }

            ProjectFeedback latestFeedback = ProjectFeedback.FindLatest(project.ID);
            if (latestFeedback.ID > feedback.ID)
            {
                feedback.ReminderEmailSentDate = DateTime.Now;
                ProjectFeedback.Update(feedback);
                return;
            }

            if (!Project.IsLatest(project))
            {
                feedback.ReminderEmailSentDate = DateTime.Now;
                ProjectFeedback.Update(feedback);
                return;
            }

            string feedbackInterval = string.Empty;

            switch (feedback.CallbackPeriod)
            {
                case ProjectFeedback.CallbackPeriodEnum.OneYear:
                    feedbackInterval = "one year";
                    break;
                case ProjectFeedback.CallbackPeriodEnum.OneYearAndHalf:
                    feedbackInterval = "one year and a half";
                    break;
                case ProjectFeedback.CallbackPeriodEnum.SixMonth:
                    feedbackInterval = "six month";
                    break;
                case ProjectFeedback.CallbackPeriodEnum.TwoYears:
                    feedbackInterval = "two years";
                    break;
            }

            Lead lead = new Lead();

            lead.FirstName = customer.FirstName;
            lead.LastName = customer.LastName;
            lead.Phone1 = customer.Phone1;
            lead.Phone2 = customer.Phone2;
            lead.Email = customer.Email;
            lead.CustomerNotes = "THIS IS A REMINDER. Customer has requested to be reminded in " + feedbackInterval + " on " + string.Format("{0:MM/dd/yyyy}", feedback.DateCreated);
            lead.DateCreated = DateTime.Now;
            lead.BusinessPartnerId = 5;
            lead.Company = string.Empty;
            lead.DispatchNotes = String.Empty;
            lead.LeadStatusId = (int)LeadStatusEnum.New;
            lead.ProjectTypeId = project.ProjectTypeId;
            lead.Address1 = string.Empty;
            lead.Address2 = string.Empty;
            lead.City = string.Empty;
            lead.State = string.Empty;
            lead.PreferredTime = string.Empty;


            Lead.Insert(lead);


            String body = string.Empty;
            body = System.IO.File.ReadAllText(Configuration.EmailTemplatesBaseDirectory + "/" + Configuration.EmailTemplatesRugCleaningReminderTemplate);

            body = body.Replace("{FIRST_NAME}", Utils.FormatName(customer.FirstName, "Customer"));
            body = body.Replace("{FEEDBACK_COMPLETION_DATE}", String.Format("{0:ddd, MMM d, yyyy}", feedback.DateCreated));
            body = body.Replace("{FEEDBACK_INTERVAL}", feedbackInterval);
            body = body.Replace("{FEEDBACK_OFFER}", Configuration.EmailTemplatesRugCleaningReminderFeedbackOffer);

            MailMessage message = new MailMessage(new MailAddress(Configuration.SmtpFromAddress, Configuration.SmtpDisplayName),
                new MailAddress(customer.Email, customer.FirstName + " " + customer.LastName));

            message.Subject = "Rug Cleaning Reminder from Dalworth Rug Cleaning";
            message.Body = body;
            message.IsBodyHtml = true;

            SmtpClient.Send(message);

            feedback.ReminderEmailSentDate = DateTime.Now;
            ProjectFeedback.Update(feedback);

            Host.Trace("Rug Cleaning Reminder", "Email sent.  CustomerId" + customer.ID + " Email " + customer.Email);
        }


        #endregion

        #region  ProcessProjectFeedbackReceived

        private static void ProcessProjectFeedbackReceived(BackgroundJobPending job)
        {
            if (!job.ProjectId.HasValue)
                return;

            Project project = Project.FindByPrimaryKey(job.ProjectId.Value);
            Customer customer = Customer.FindByPrimaryKey(project.CustomerId.Value);
            ProjectFeedback projectFeedback = ProjectFeedback.FindByProjectId(job.ProjectId.Value);
            ProjectFeedbackRate rate = ProjectFeedbackRate.FindByPrimaryKey(projectFeedback.RateId);

            DateTime completionDate = Project.GetCompletionDate(project);
            if (completionDate == DateTime.MinValue)
                return;

            String remindPeriod = string.Empty;
            ProjectFeedback.CallbackPeriodEnum callbackPeriod = projectFeedback.CallbackPeriod;

            switch (callbackPeriod)
            {
                case ProjectFeedback.CallbackPeriodEnum.NotSelected:
                    remindPeriod = "Not Selected";
                    break;
                case ProjectFeedback.CallbackPeriodEnum.DoNotRemind:
                    remindPeriod = "Do not remind";
                    break;
                case ProjectFeedback.CallbackPeriodEnum.SixMonth:
                    remindPeriod = "Six Month";
                    break;
                case ProjectFeedback.CallbackPeriodEnum.OneYear:
                    remindPeriod = "One Year";
                    break;
                case ProjectFeedback.CallbackPeriodEnum.OneYearAndHalf:
                    remindPeriod = "One Year and Half";
                    break;
                case ProjectFeedback.CallbackPeriodEnum.TwoYears:
                    remindPeriod = "Two years";
                    break;

            }

            String body = string.Empty;
            body = File.ReadAllText(Configuration.EmailTemplatesBaseDirectory + "/" + Configuration.EmailTemplatesProjectFeedbackReceivedTemplate);
            body = body.Replace("{PROJECT_TYPE}", project.ProjectTypeText); 
            body = body.Replace("{FEEDBACK_CREATE_DATE}", String.Format("{0:ddd, MMM d, yyyy}", projectFeedback.DateCreated));
            body = body.Replace("{FIRST_NAME}", Utils.FormatName(customer.FirstName, "Customer"));
            body = body.Replace("{LAST_NAME}", Utils.FormatName(customer.LastName, "Customer"));
            body = body.Replace("{PHONE_NUMBER}", customer.Phone1Formatted);
            body = body.Replace("{EMAIL}", string.IsNullOrEmpty(customer.Email)? String.Empty: customer.Email);
            body = body.Replace("{RATING}", rate.Rate);
            body = body.Replace("{PROJECT_COMPLETION_DATE}", String.Format("{0:ddd, MMM d, yyyy}", completionDate));
            body = body.Replace("{REMIND_PERIOD}", remindPeriod);
            body = body.Replace("{CUSTOMER_NOTE}", projectFeedback.CustomerNote);

            MailMessage message = new MailMessage(new MailAddress(Configuration.SmtpFromAddress, Configuration.SmtpDisplayName),
               new MailAddress(Configuration.SmtpFeedbackAddress, Configuration.SmtpFeedbackDisplayName));

            message.Subject = "Feedback Received";
            message.Body = body;
            message.IsBodyHtml = true;

            SmtpClient.Send(message);

            Host.Trace("Feedback Received", "Email sent.  Feedback ID = " + projectFeedback.ID);
        }

        #endregion

        #region WordPress Integration 

        private static void UpdateFeedbackInWordpress(string url, ProjectFeedback projectFeedback)
        {
            string postData = "action_name=feedback_update_review&feedback_id=" + projectFeedback.ID + 
                              "&edited_customer_note=" + projectFeedback.EditedCustomerNote + 
                              "&can_be_posted_on_website=" + (projectFeedback.CanBePostedOnWebSite?1:0);

            string responseFromServer = PostToWordpress(url, postData);
            string[] responseArr = responseFromServer.Split('=');

            if (responseArr[0] == "status")
            {
                int status = int.Parse(responseArr[1]);
                if (status == 1)
                    return;
            }

            throw new DalworthException("Invalid response");               
        }

        private static int CreateFeedbackInWordpress(string url, Project project, DateTime completionDate)
        {
            Customer customer = Customer.FindByPrimaryKey(project.CustomerId.Value);
            Address address = Address.FindByPrimaryKey(customer.AddressId.Value);

            string lastName = customer.LastName.Replace('&', ' ').Replace('=', ' ').ToUpper();
            string firstName = customer.FirstName.Replace('&', ' ').Replace('=', ' ').ToUpper();

            string city = address.City.Replace('&', ' ').Replace('=', ' '); 

            string postData = "action_name=feedback_create&project_id=" + project.ID + "&first_name=" +
                              firstName + "&last_name=" + lastName + "&service_date=" + completionDate.ToString("yyyy-MM-dd") + "&city=" +
                              city + "&zip=" + address.Zip;
            string responseFromServer = PostToWordpress(url, postData);
            string[] responseArr = responseFromServer.Split('=');
            
            if (responseArr[0] == "feedback_id")
                return int.Parse(responseArr[1]);
            
            throw new DalworthException("Invalid response");
        }

        private static string PostToWordpress (string url, string postData)
        {
            string responseFromServer;
            WebRequest request = WebRequest.Create(url);
            
            request.Method = "POST";
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteArray.Length;
            using (Stream outDataStream = request.GetRequestStream())
            {
                outDataStream.Write(byteArray, 0, byteArray.Length);
                outDataStream.Close();
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream inDataStream = response.GetResponseStream())
                    {
                        StreamReader reader = new StreamReader(inDataStream);
                        responseFromServer = reader.ReadToEnd();
                        reader.Close();
                        inDataStream.Close();
                    }
                    response.Close();
                }
            }
            return responseFromServer;
        }

        #endregion 

        #region ProcessProjectFeedbackProcessed

        private static void ProcessProjectFeedbackProcessed(BackgroundJobPending job)
        {
            if (!job.ProjectFeedbackId.HasValue)
                return;

            ProjectFeedback projectFeedback = ProjectFeedback.FindByPrimaryKey(job.ProjectFeedbackId.Value);
            Project project = Project.FindByPrimaryKey(projectFeedback.ProjectId);
            Customer customer = Customer.FindByPrimaryKey(project.CustomerId.Value);
            Employee employee = Employee.FindByPrimaryKey(projectFeedback.ReviewedByEmployeeId.Value);
            ProjectFeedbackRate rate = ProjectFeedbackRate.FindByPrimaryKey(projectFeedback.RateId);

            if (project.ProjectType == ProjectTypeEnum.Deflood)
                UpdateFeedbackInWordpress(Configuration.DalworthRestorationCustomerFeedbackUrl, projectFeedback);
    
            String body = string.Empty;
            body = File.ReadAllText(Configuration.EmailTemplatesBaseDirectory + "/" + Configuration.EmailTemplatesProjectFeedbackProcessedTemplate);
            body = body.Replace("{PROJECT_TYPE}", project.ProjectTypeText); 
            body = body.Replace("{FEEDBACK_CREATE_DATE}", projectFeedback.DateCreated.ToString("g"));
            body = body.Replace("{FEEDBACK_PROCESSED_DATE}", projectFeedback.DateReviewed.Value.ToString("g"));
            body = body.Replace("{EMPLOYEE_NAME}", employee.FirstName + " " + employee.LastName);
            body = body.Replace("{IS_PUBLISHED_TO_WEB_SITE}", projectFeedback.CanBePostedOnWebSite?"True":"False");
            body = body.Replace("{FIRST_NAME}", Utils.FormatName(customer.FirstName, "Customer"));
            body = body.Replace("{LAST_NAME}", Utils.FormatName(customer.LastName, "Customer"));
            body = body.Replace("{PHONE_NUMBER}", customer.Phone1Formatted);
            body = body.Replace("{EMAIL}", string.IsNullOrEmpty(customer.Email) ? String.Empty : customer.Email);
            body = body.Replace("{RATING}", rate.Rate);

            body = body.Replace("{DISPATCHER_NOTE}", projectFeedback.DispatcherNote!= null?projectFeedback.DispatcherNote:string.Empty);

            body = body.Replace("{CUSTOMER_NOTE}", projectFeedback.CustomerNote!= null?projectFeedback.CustomerNote:string.Empty);

            MailMessage message = new MailMessage(new MailAddress(Configuration.SmtpFromAddress, Configuration.SmtpDisplayName),
               new MailAddress(Configuration.SmtpFeedbackAddress, Configuration.SmtpFeedbackDisplayName));

            message.Subject = "Feedback Processed";
            message.Body = body;
            message.IsBodyHtml = true;

            SmtpClient.Send(message);

            Host.Trace("Feedback Received", "Email sent.  Feedback ID = " + projectFeedback.ID);
        }

        #endregion

        #region  ProcessProjectCompletedEmail

        private static void ProcessProjectCompletedEmail(BackgroundJobPending job)
        {
            if (!job.ProjectId.HasValue)
                return;

            Project project = Project.FindByPrimaryKey(job.ProjectId.Value);
            if (project.ProjectType != ProjectTypeEnum.RugCleaning && project.ProjectType != ProjectTypeEnum.Deflood)
                return;

            DateTime completionDate = Project.GetCompletionDate(project);
            if (completionDate == DateTime.MinValue)
                return;

            Customer customer = Customer.FindByPrimaryKey(project.CustomerId.Value);

            String body = string.Empty;
            String subject = string.Empty;

            if (project.ProjectType == ProjectTypeEnum.RugCleaning)
            {
                List<Task> rugDeliveries = Task.FindByProjectTypeAndStatus(project,
                TaskTypeEnum.RugDelivery, TaskStatusEnum.Completed);
                List<Item> allItems = new List<Item>();
                foreach (Task task in rugDeliveries)
                {
                    List<Item> items = Item.FindByTask(task);
                    allItems.AddRange(items);
                }

                body = File.ReadAllText(Configuration.EmailTemplatesBaseDirectory + "/" + Configuration.EmailTemplatesRugCleaningCompletedTemplate);
                body = body.Replace("{FIRST_NAME}", Utils.FormatName(customer.FirstName, "Customer"));
                body = body.Replace("{PROJECT_COMPLETION_DATE}", String.Format("{0:ddd, MMM d, yyyy}", completionDate));
                body = body.Replace("{PROJECT_ID}", project.ID.ToString());

                if (allItems.Count == 1)
                    body = body.Replace("{RUGS_INFO}", " rug was");
                else
                    body = body.Replace("{RUGS_INFO}", allItems.Count + " rugs were");

                subject = "Rug delivery confirmation";
            }

            if (project.ProjectType == ProjectTypeEnum.Deflood)
            {
                int wordpressFeedbackId = CreateFeedbackInWordpress(Configuration.DalworthRestorationCustomerFeedbackUrl, project, completionDate); 

                body = File.ReadAllText(Configuration.EmailTemplatesBaseDirectory + "/" + Configuration.EmailTemplatesDefloodCompletedTemplate);
                body = body.Replace("{FIRST_NAME}", Utils.FormatName(customer.FirstName, "Customer"));
                body = body.Replace("{PROJECT_COMPLETION_DATE}", String.Format("{0:ddd, MMM d, yyyy}", completionDate));
                body = body.Replace("{FEEDBACK_ID}", wordpressFeedbackId.ToString());
                subject = "Restoration project completion confirmation";
            }

            if (body == null || body.Length == 0)
            {
                return;
            }

            try
            {
                MailMessage message = new MailMessage(new MailAddress(Configuration.SmtpFromAddress, Configuration.SmtpDisplayName),
                                                      new MailAddress(customer.Email, customer.FirstName + " " + customer.LastName));

                message.Subject = subject;
                message.Body = body;
                message.IsBodyHtml = true;

                SmtpClient.Send(message);
                Host.Trace("Pending Jobs", "Project completed email sent.  Project ID = " + project.ID + " email " + customer.Email);
            }
            catch (Exception ex)
            {
                Host.Trace("Pending Jobs", "ERROR Unable to send email.  Project ID = " + project.ID + " email " + customer.Email);
                Host.Trace("Pending Jobs", ex.ToString());
            }
        }
        #endregion

        #region NotifiyOnCallNewLead

        private static void NotifiyNewLead(BackgroundJobPending job)
        {
            if (!job.LeadId.HasValue)
                throw new DalworthException("LeadReceivedEmail job doesn't have LeadId");

            Lead lead = Lead.FindByPrimaryKey(job.LeadId.Value);

            string emailAddress;
            string displayName;
            if (lead.ProjectType == ProjectTypeEnum.NotSpecified)
            {
                emailAddress = Configuration.SmtpMarketingAddress;
                displayName = Configuration.SmtpMarketingDisplayName;
            }
            else
            {
                emailAddress = Configuration.SmtpOnCallAddress;
                displayName = Configuration.SmtpMarketingDisplayName;
            }

            MailMessage message = new MailMessage(new MailAddress(Configuration.SmtpFromAddress, Configuration.SmtpDisplayName),
                new MailAddress(emailAddress, displayName));

            message.Subject = "New Lead Notification";
            message.IsBodyHtml = false;
            message.Body = "";
            message.Body += "Request Detail:\n";
            message.Body += "---------------------------------------------\n";
            message.Body += CreateLeadDetail(lead);
            message.Body += "\n";

            SmtpClient.Send(message);

            Host.Trace("Pending Jobs", "Notify on call new lead.  LeadId = " + lead.ID + " email " + emailAddress);
        }
        #endregion

        #region ProcessLeadReceivedEmail

        private static void ProcessLeadReceivedEmail(BackgroundJobPending job)
        {
            if (!job.LeadId.HasValue)
                throw new DalworthException("LeadReceivedEmail job doesn't have LeadId");

            String body = string.Empty;
            String subject = string.Empty;

            Lead lead = Lead.FindByPrimaryKey(job.LeadId.Value);

            if (lead.ProjectType != ProjectTypeEnum.NotSpecified && lead.ProjectType != ProjectTypeEnum.RugCleaning && lead.ProjectType != ProjectTypeEnum.Deflood)
                return;

            if (lead.ProjectType == ProjectTypeEnum.NotSpecified)
                body = System.IO.File.ReadAllText(Configuration.EmailTemplatesBaseDirectory + "/" + Configuration.EmailTemplatesLeadReceivedMarketingTemplate);
            else
                body = System.IO.File.ReadAllText(Configuration.EmailTemplatesBaseDirectory + "/" + Configuration.EmailTemplatesLeadReceivedTemplate);

            body = body.Replace("{FIRST_NAME}", Utils.FormatName(lead.FirstName, "Customer"));
            body = body.Replace("{LEAD_CREATE_DATE}", String.Format("{0:ddd, MMM d, yyyy}", lead.DateCreated));
            body = body.Replace("{LAST_NAME}", Utils.FormatName(lead.LastName, String.Empty));
            body = body.Replace("{PHONE_NUMBER}", lead.Phone1);
            body = body.Replace("{COMMENT}", lead.CustomerNotes);

           
            switch (lead.ProjectType)
            {
                case ProjectTypeEnum.RugCleaning:
                    subject = "Rug Pickup Request Confirmation";
                    body = body.Replace("{PROJECT_TYPE}", "Oriental Rug Cleaning");
                    body = body.Replace("{COMPANY}", "Dalworth Rug Cleaning");
                    body = body.Replace("{WEB_SITE_URL}", "http://wwww.dalworthrugcleaning.com");
                    body = body.Replace("{WEB_SITE_NAME}", "DalworthRugCleaning.com");
                    break;
                case ProjectTypeEnum.Deflood:
                    subject = "Water or Fire Damage Request Confirmation";
                    body = body.Replace("{PROJECT_TYPE}", "Water or Fire Damage Restoration");
                    body = body.Replace("{COMPANY}", "Dalworth Restoration");
                    body = body.Replace("{WEB_SITE_URL}", "http://wwww.dalworthrestoration.com");
                    body = body.Replace("{WEB_SITE_NAME}", "DalworthRestoration.com");
                    break;
                case ProjectTypeEnum.NotSpecified:
                    subject = "Contact Request Confirmation";
                    body = body.Replace("{COMPANY}", "Dalworth Restoration");
                    body = body.Replace("{WEB_SITE_URL}", "http://wwww.dalworthrestoration.com");
                    body = body.Replace("{WEB_SITE_NAME}", "DalworthRestoration.com");
                    break;
                    
            }

            MailMessage message = new MailMessage(new MailAddress(Configuration.SmtpFromAddress, Configuration.SmtpDisplayName),
                new MailAddress(lead.Email, lead.FirstName + " " + lead.LastName));

            message.Subject = subject;
            message.IsBodyHtml = true;
            message.Body = body;

            SmtpClient.Send(message);

            Host.Trace("Pending Jobs", "Lead Received Email sent.  LeadId = " + lead.ID + " email " + lead.Email);
        }

        #endregion

        #region ProcessLeadRecievedPrint

        private static void ProcessLeadRecievedPrint(BackgroundJobPending job)
        {
            if (!job.LeadId.HasValue)
                throw new DalworthException("LeadRecievedPrint job doesn't have LeadId");

            Lead lead = Lead.FindByPrimaryKey(job.LeadId.Value);
            LeadPrint leadPrint = new LeadPrint(lead);
            leadPrint.Print();
        }

        #endregion

        #region CreateLeadDetail

        private static string CreateLeadDetail(Lead lead)
        {
            string result = string.Empty;

            result += ("First Name:" + lead.FirstName + "\n");
            result += ("Last Name:" + lead.LastName + "\n");
            if (lead.Company != null && lead.Company != string.Empty)
                result += ("Company:" + lead.Company + "\n");

            result += ("Home Phone:" + lead.Phone1 + "\n");

            if (lead.Phone2 != null && lead.Phone2 != string.Empty)
                result += ("Other Phone:" + lead.Phone2 + "\n");

            result += ("Email:" + lead.Email + "\n");

            if (lead.ContainsAddress)
            {
                result += ("Address:" + lead.Address1 + " " + lead.Address2 + "\n");
                result += ("City:" + lead.City + "\n");
                result += ("State:" + lead.State + "\n");
                result += ("Zip code:" + lead.Zip + "\n");
            }

            if (lead.PreferredServiceDate.HasValue)
                result += ("Prefered Service Date:" + lead.PreferredServiceDate + "\n");

            if (lead.PreferredTime != null && lead.PreferredTime != string.Empty)
                result += ("Prefered Time Period:" + lead.PreferredTime + "\n");

            if (lead.CustomerNotes != null && lead.CustomerNotes != string.Empty)
            {
                result += "---------------------------------------------\n";
                result += ("Comments:\n");
                result += (lead.CustomerNotes + "\n");
                result += "---------------------------------------------\n";
            }

            return result;
        }

        #endregion

        #region ProcessPartnerSitePasswordReminder

        private static PartnerInvitation ProcessPartnerSitePasswordReminder(BackgroundJobPending job)
        {
            PartnerInvitation invitation = PartnerInvitation.FindByPrimaryKey(job.PartnerInvitationKey);
            WebUser user = WebUser.FindByPrimaryKey(invitation.WebUserId.Value);

            string body = System.IO.File.ReadAllText(Configuration.EmailTemplatesBaseDirectory + "/" + Configuration.EmailTemplatesPartnerSitePasswordReminder);
            body = string.Format(body, user.DisplayName,
                Configuration.PartnerSiteUrl + "/Account/SetupPassword?key=" + invitation.InvitationKey);
            
            MailMessage message = new MailMessage(new MailAddress(Configuration.PartnerSiteEmailFrom, string.Empty),
               new MailAddress(invitation.Email, user.DisplayName));

            message.Subject = "Reset your password";
            message.Body = body;
            message.IsBodyHtml = false;

            SmtpClient.Send(message);

            Host.Trace("ProcessPartnerSitePasswordReminder", 
                string.Format("Email sent. Invitation key {0}", invitation.InvitationKey));
            return invitation;
        }

        #endregion

        #region ProcessPartnerSiteInvitation

        private static PartnerInvitation ProcessPartnerSiteInvitation(BackgroundJobPending job)
        {
            PartnerInvitation invitation = PartnerInvitation.FindByPrimaryKey(job.PartnerInvitationKey);
            WebUser user = WebUser.FindByPrimaryKey(invitation.WebUserId.Value);

            string body = System.IO.File.ReadAllText(Configuration.EmailTemplatesBaseDirectory + "/" + Configuration.EmailTemplatesPartnerSiteInvitation);
            body = string.Format(body,
                user.DisplayName != string.Empty ? " " + user.DisplayName : string.Empty,
                Configuration.PartnerSiteUrl + "/Account/SetupPassword?key=" + invitation.InvitationKey);

            MailMessage message = new MailMessage(new MailAddress(Configuration.PartnerSiteEmailFrom, string.Empty),
               new MailAddress(invitation.Email, string.Empty));

            message.Subject = "Dalworth Partner In Profit invitation";
            message.Body = body;
            message.IsBodyHtml = false;

            SmtpClient.Send(message);

            Host.Trace("ProcessPartnerSiteInvitation",
                string.Format("Email sent. Invitation key {0}", invitation.InvitationKey));
            return invitation;
        }

        #endregion

        #region ProcessPartnerSiteInvitation

        private static void ProcessPartnerSiteSummaryReport()
        {
            List<PartnerSummaryReportItem> items = PartnerSummaryReportItem.FindNotSent();

            while(items.Count > 0)
            {
                List<PartnerSummaryReportItem> partnerItems = new List<PartnerSummaryReportItem>();                
                PartnerSummaryReportItem currentReportItem = items[0];
                int currentOrderSourceId = currentReportItem.OrderSourceId;

                foreach (PartnerSummaryReportItem mixedItem in items)
                {
                    if (mixedItem.OrderSourceId == currentOrderSourceId && mixedItem.GenerateDate == currentReportItem.GenerateDate)
                        partnerItems.Add(mixedItem);
                }                

                foreach (PartnerSummaryReportItem partnerItem in partnerItems)
                    items.Remove(partnerItem);

                PartnerSummaryReportItem partnerSummaryRow = new PartnerSummaryReportItem();
                foreach (PartnerSummaryReportItem partnerItem in partnerItems)
                {
                    partnerSummaryRow.CallCount += partnerItem.CallCount;
                    partnerSummaryRow.BookCount += partnerItem.BookCount;
                    partnerSummaryRow.ShopperCount += partnerItem.ShopperCount;
                    partnerSummaryRow.NoActionCount += partnerItem.NoActionCount;
                    partnerSummaryRow.CancelCount += partnerItem.CancelCount;
                    partnerSummaryRow.InProcessCount += partnerItem.InProcessCount;                    
                    partnerSummaryRow.CompletedCount += partnerItem.CompletedCount;
                    partnerSummaryRow.Amount += partnerItem.Amount;                                        
                }

                string body = System.IO.File.ReadAllText(Configuration.EmailTemplatesBaseDirectory + "/PartnerSummaryReport.htm");
                string dataRowTemplate = System.IO.File.ReadAllText(Configuration.EmailTemplatesBaseDirectory + "/PartnerSummaryReportDataRow.htm");
                string summaryRowTemplate = System.IO.File.ReadAllText(Configuration.EmailTemplatesBaseDirectory + "/PartnerSummaryReportSummaryRow.htm");

                string dataRows = string.Empty;
                int counter = 1;
                foreach (PartnerSummaryReportItem partnerItem in partnerItems)
                {                    
                    dataRows += string.Format(dataRowTemplate, counter,
                        partnerItem.SourceText, partnerItem.CallCount, partnerItem.BookCount, partnerItem.ShopperCount,
                        partnerItem.NoActionCount, partnerItem.CancelCount, partnerItem.InProcessCount, 
                        partnerItem.CompletedCount, partnerItem.Amount.ToString("C"));
                    counter++;
                }

                string summaryRow = string.Format(summaryRowTemplate, partnerSummaryRow.CallCount,
                    partnerSummaryRow.BookCount, partnerSummaryRow.ShopperCount, partnerSummaryRow.NoActionCount,
                    partnerSummaryRow.CancelCount, partnerSummaryRow.InProcessCount, partnerSummaryRow.CompletedCount,
                    partnerSummaryRow.Amount.ToString("C"));

                OrderSource orderSource = OrderSource.FindByPrimaryKey(currentOrderSourceId);

                DateTime reportEnd = partnerItems[0].GenerateDate;
                DateTime reportStart = new DateTime(reportEnd.Year, reportEnd.Month, 1);

                body = body.Replace("[PartnerName]", orderSource.Name)
                    .Replace("[ReportDate]", string.Format("{0} - {1}", reportStart.ToShortDateString(), reportEnd.ToShortDateString())) 
                    .Replace("[PartnerSummaryReportDataRow]", dataRows)
                    .Replace("[PartnerSummaryReportSummaryRow]", summaryRow);

                List<WebUser> partnerUsers = WebUser.FindByPartner(currentOrderSourceId);
                foreach (WebUser partnerUser in partnerUsers)
                {
                    MailMessage message = new MailMessage(new MailAddress(Configuration.PartnerSiteEmailFrom, string.Empty),
                       new MailAddress(partnerUser.Email, string.Empty));
                    message.Subject = "Dalworth Partner Summary Report";
                    message.Body = body;
                    message.IsBodyHtml = true;
                    SmtpClient.Send(message);

                    Host.Trace("ProcessPartnerSiteSummaryReport",
                        string.Format("Email sent for Partner ID {0}, email address {1}", currentOrderSourceId,
                        partnerUser.Email));
                }
            }

            items = PartnerSummaryReportItem.FindNotSent();
            foreach (PartnerSummaryReportItem item in items)
            {
                item.IsSent = true;
                PartnerSummaryReportItem.Update(item);
            }            
        }

        #endregion
    }
}
