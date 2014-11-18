using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web.UI;
using Dalworth.Server.Data;
using System.Data;
using System.IO;
using Dalworth.Server.Domain;


namespace Dalworth.Server.Web.Restoration
{
    public partial class Service : Page
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            string rawRequest;
            using (StreamReader reader = new StreamReader(Context.Request.InputStream))
            {
                rawRequest = reader.ReadToEnd();
            }

            string[] nameValues = rawRequest.Split('&');

            NameValueCollection request = new NameValueCollection();

            foreach (string nameValue in nameValues)
            {
                string[] keyValues = nameValue.Split('=');
                request.Add(keyValues[0], keyValues[1]);
            }

            if (request["action_name"] == "feedback_save")
                SaveFeedback(request);
            else if(request["action_name"] == "lead_create")
                CreateLead(request);
        }

        private void SaveFeedback(NameValueCollection request)
        {
            int projectId = int.Parse(request["project_id"]);
            int rateId = int.Parse(request["rate_id"]);
            string customerNote = request["customer_note"];

            ProjectFeedback feedback = new ProjectFeedback();

            feedback.Submit(projectId, rateId, customerNote, ProjectFeedback.CallbackPeriodEnum.DoNotRemind);

            string response = "{status=1&feedback_id=" + feedback.ID + "}";
            Context.Response.Write(response);
        }

        private void CreateLead(NameValueCollection request)
        {
            int projectTypeId = int.Parse(request["project_type_id"]);
            int businessPartnerId = int.Parse(request["business_partner_id"]);
            string lastName = request["last_name"];
            string firstName = request["first_name"];
            string phone1 = request["phone"];
            string email = request["email"];
            string customerNote = request["comment"];

            using (IDbConnection connection = Connection.Instance.GetTemporaryDbConnection())
            {
                connection.Open();
                Lead lead = new Lead();
                Dictionary<string, string> errors = lead.Submit(projectTypeId, businessPartnerId, firstName, lastName,
                                                                phone1, email, customerNote, "", null, null, connection);
                string response;
                if (errors.Count == 0)
                {
                    response = "{status=1&lead_id=" + lead.ID + "}";
                    Context.Response.Write(response);
                }
                else
                {
                    response = "{status=0";

                    foreach (KeyValuePair<string, string> errorMessage in errors)
                    {
                        response += "&" + errorMessage.Key + "=" + errorMessage.Value;
                    }

                    response += "}";
                    Context.Response.Write(response);
                }
            }
        }
    }
}