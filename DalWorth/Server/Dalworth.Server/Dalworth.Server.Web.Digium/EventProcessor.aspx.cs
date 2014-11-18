using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Dalworth.Server.Data;
using Dalworth.Server.Domain;

namespace Dalworth.Server.Web.Digium
{
    public partial class EventProcessor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {            
            SDK.Configuration.ConnectionString = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;

            string requestString = string.Empty;
            foreach (string key in Context.Request.QueryString.AllKeys)
                requestString += string.Format("{0}={1}; ", key, Context.Request.QueryString[key]);

            string eventType = Context.Request.QueryString["EVENT_TYPE"];
            DigiumLog logItem = null;
            if (eventType == "answered" && Context.Request.QueryString["CALLER_ID_NUMBER"].Length > 3)
            {
                logItem = new DigiumLog(0, 
                    Context.Request.QueryString["JOB_ID"], DateTime.Now, 0,
                    Context.Request.QueryString["CALLER_ID_NUMBER"],
                    Context.Request.QueryString["CALLER_ID_NAME"],
                    Context.Request.QueryString["EXTENSION"],
                    Context.Request.QueryString["EXTENSION_TYPE"],
                    Context.Request.QueryString["INCOMING_DID"],
                    false);
            }

            if ((eventType == "outgoing" || eventType == "hangup") && Context.Request.QueryString["JOB_ID"].Length > 0)
            {
                logItem = DigiumLog.FindBy(Context.Request.QueryString["JOB_ID"]);
                if (logItem != null)
                {
                    if (eventType == "outgoing")
                    {
                        logItem.IsIntermediateCall = true;
                        logItem.DurationSec = (int)DateTime.Now.Subtract(logItem.TimeCreated).TotalSeconds;
                    }
                    else if (!logItem.IsIntermediateCall)
                        logItem.DurationSec = (int)DateTime.Now.Subtract(logItem.TimeCreated).TotalSeconds;
                }
            }

            using (IDbConnection connection = Connection.Instance.GetTemporaryDbConnection())
            {
                connection.Open();

                IDbTransaction transaction = connection.BeginTransaction();

                try
                {
                    DigiumLogDraft.Insert(new DigiumLogDraft(0, DateTime.Now, requestString), 
                        connection);

                    if (logItem != null)
                    {
                        if (logItem.ID == 0)
                            DigiumLog.Insert(logItem, connection);
                        else
                            DigiumLog.Update(logItem, connection);                        
                    }                        
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}
