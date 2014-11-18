using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;

using DPI.Interfaces;
using DPI.ClientComp;
using DPI.Services;

namespace DPI.Ordering.Training
{
	public class Certification_quiz_Slingshot : System.Web.UI.Page
	{
		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
			LoadQuestions();
		}
		
		private void InitializeComponent()
		{    
			this.btnSubmit.Click += new System.Web.UI.ImageClickEventHandler(this.btnSubmit_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}

		#region Data
		protected System.Web.UI.WebControls.Image Image1;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.TextBox txtCoWorkerID;
		protected System.Web.UI.WebControls.RequiredFieldValidator RequiredFieldValidator1;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.TextBox txtName;
		protected System.Web.UI.WebControls.RequiredFieldValidator RequiredFieldValidator2;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.Label Label16;
		protected System.Web.UI.WebControls.RequiredFieldValidator RequiredFieldValidator3;
		protected System.Web.UI.WebControls.RadioButtonList Radiobuttonlist1;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.RequiredFieldValidator RequiredFieldValidator4;
		protected System.Web.UI.WebControls.RadioButtonList Radiobuttonlist2;
		protected System.Web.UI.WebControls.Label Label8;
		protected System.Web.UI.WebControls.RequiredFieldValidator RequiredFieldValidator5;
		protected System.Web.UI.WebControls.RadioButtonList Radiobuttonlist3;
		protected System.Web.UI.WebControls.Label Label9;
		protected System.Web.UI.WebControls.RequiredFieldValidator RequiredFieldValidator6;
		protected System.Web.UI.WebControls.RadioButtonList Radiobuttonlist4;
		protected System.Web.UI.WebControls.Label Label10;
		protected System.Web.UI.WebControls.RequiredFieldValidator RequiredFieldValidator7;
		protected System.Web.UI.WebControls.RadioButtonList Radiobuttonlist5;
		protected System.Web.UI.WebControls.Label Label11;
		protected System.Web.UI.WebControls.RequiredFieldValidator RequiredFieldValidator8;
		protected System.Web.UI.WebControls.RadioButtonList Radiobuttonlist6;
		protected System.Web.UI.WebControls.Label Label14;
		protected System.Web.UI.WebControls.RequiredFieldValidator RequiredFieldValidator11;
		protected System.Web.UI.WebControls.RadioButtonList Radiobuttonlist9;
		protected System.Web.UI.WebControls.Label Label15;
		protected System.Web.UI.WebControls.RequiredFieldValidator RequiredFieldValidator12;
		protected System.Web.UI.WebControls.RadioButtonList Radiobuttonlist10;
		protected System.Web.UI.WebControls.ValidationSummary ValidationSummary1;
		protected System.Web.UI.WebControls.Label lblError;
		protected System.Web.UI.WebControls.RequiredFieldValidator RequiredFieldValidator9;
		protected System.Web.UI.WebControls.Label Label12;
		protected System.Web.UI.WebControls.RequiredFieldValidator RequiredFieldValidator10;
		protected System.Web.UI.WebControls.Label Label13;
		protected System.Web.UI.WebControls.RadioButtonList RadioButtonList7;
		protected System.Web.UI.WebControls.RadioButtonList RadioButtonList8;
		protected System.Web.UI.WebControls.ImageButton btnSubmit;
		#endregion
		#endregion

		#region Question Struct
		Questions[] questions;		
		struct Questions
		{
			public readonly RadioButtonList rbl;
			public readonly int ind;
			public Questions(RadioButtonList rbl, int ind)
			{
				this.rbl = rbl;
				this.ind = ind;
			}
		}		

		#endregion

		#region Events
		void Page_Load(object sender, System.EventArgs e)
		{
		}

		void btnSubmit_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			IUser user = null;
			float score = 0f;

			StringBuilder sb = new StringBuilder();
			try
			{
				user = (IUser)Session["User"];
							
				for (int i = 0; i < questions.Length; i++)
				{
					if (questions[i].rbl.SelectedIndex == questions[i].ind)
						score++;
					else
					{
						sb.Append((i+ 1).ToString());
						sb.Append(",");
					}
				}

				StoreSvc.SaveCertResult(IMapFactory.getIMap(), txtCoWorkerID.Text,  score / questions.Length > 0.69,
					txtName.Text, user.LoginStoreCode, (int)CertType.Slingshot);
				
				string result = "Congratulations!";

				if (sb.Length == 0)
					sb.Append(",");	 //Add one charecter when sb's length is zero to avoid error for sb.ToString().Substring(0, sb.Length - 1) 

				if(score < 7)
					result = "We're Sorry";

				Server.Transfer("CertResultsSlingshot.aspx?wrong=" + 
					sb.ToString().Substring(0, sb.Length - 1) + 
					"&result=" + result + 
					"&name=" + txtName.Text , false);
				
			}
			catch(Exception ex)
			{
				ErrLogSvc.LogError(this.ToString(), user.ClerkId, ex.Message + ", " + ex.StackTrace);
				lblError.Text = ex.Message ;
				lblError.Visible = true;
				//Mel, please add error message field and populated with error message here 
			}		
		}
		#endregion

		#region Implementations
		void LoadQuestions()
		{
			ArrayList ar = new ArrayList();
		
			// specify correct answers for each question
					
			Questions q = new Questions(Radiobuttonlist1, 3);
			ar.Add(q);
			
			q = new Questions(Radiobuttonlist2, 0);
			ar.Add(q);
            
			q = new Questions(Radiobuttonlist3, 3);
			ar.Add(q);
			
			q = new Questions(Radiobuttonlist4, 0);
			ar.Add(q);
			
			q = new Questions(Radiobuttonlist5, 0);
			ar.Add(q);
			
			q = new Questions(Radiobuttonlist6, 1);
			ar.Add(q);
			
			q = new Questions(RadioButtonList7, 1);
			ar.Add(q);
			
			q = new Questions(RadioButtonList8, 3);
			ar.Add(q);
			
			q = new Questions(Radiobuttonlist9, 3);
			ar.Add(q);
			
			q = new Questions(Radiobuttonlist10, 0);
			ar.Add(q);

			questions = new Questions[ar.Count];
			ar.CopyTo(questions);
		}

		#endregion
		
	}
}
