using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace TestGmailSpeed
{
    public partial class Form1 : Form
    {

        public Form1() {
            InitializeComponent();
        }

        APop3Client pop3Client;

        private void btnConnect_Click(object sender, EventArgs e)
        {
            DateTime operationStartTime;

            if (pop3Client == null)
            {
                pop3Client = new Pop3Client1();
            }
           

            btnConnect.Enabled = false;
            Cursor = Cursors.WaitCursor;

            try
            {
                if (pop3Client.IsConnected == false)
                {
                    operationStartTime = DateTime.Now;
                    pop3Client.Connect1("pop.gmail.com", 995, txtLogin.Text, txtPassword.Text);
                    lblConnectTime.Text = (DateTime.Now - operationStartTime).TotalMilliseconds.ToString() + " ms";
                }

                int messagesOnServer = pop3Client.GetMessagesCount1();
                int messagesCountForRetrieving = int.Parse(txtMsgCount.Text);

                if (messagesOnServer < messagesCountForRetrieving)
                {
                    throw new Exception("There are no so many letters in the mail box");
                }

                List<int> messageNumbers = new List<int>();

                for (int i = 0; i < messagesCountForRetrieving; i++)
                {
                    messageNumbers.Add(messagesOnServer - i);
                }

                operationStartTime = DateTime.Now;
                List<string> messages = RetrieveMessages1(messageNumbers.ToArray());
                lblRetrieveTime.Text = (DateTime.Now - operationStartTime).TotalMilliseconds.ToString() + " ms";

               // pop3Client.Disconnect();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally {
             
                //if (pop3Client != null) {
                //    pop3Client.Disconnect1();
                //}
                
            }

            btnConnect.Enabled = true;
            Cursor = Cursors.Default;


        }
        
        private void btnConnect_Click1(object sender, EventArgs e) {
            DateTime operationStartTime;
            
            if (rbA1.Checked)
                pop3Client = new Pop3Client1();
            else 
                pop3Client = new Pop3Client2();
            
            btnConnect.Enabled = false;
            Cursor = Cursors.WaitCursor;
            
            try {
                operationStartTime = DateTime.Now;
                pop3Client.Connect("pop.gmail.com", 995);
                lblConnectTime.Text = (DateTime.Now - operationStartTime).TotalMilliseconds.ToString() + " ms";
                
                operationStartTime = DateTime.Now;
                pop3Client.Login(txtLogin.Text, txtPassword.Text);
                lblLoginTime.Text = (DateTime.Now - operationStartTime).TotalMilliseconds.ToString() + " ms";

                
                int messagesOnServer = pop3Client.GetMessagesCount();
                int messagesCountForRetrieving = int.Parse(txtMsgCount.Text);
                
                if (messagesOnServer < messagesCountForRetrieving) {
                    throw new Exception("There are no so many letters in the mail box");
                }
                
                List<int> messageNumbers = new List<int>();
                
                for (int i = 0; i < messagesCountForRetrieving; i++) {
                    messageNumbers.Add(messagesOnServer - i);
                }
                
                operationStartTime = DateTime.Now;
                List<string> messages = RetrieveMessages(messageNumbers.ToArray());
                lblRetrieveTime.Text = (DateTime.Now - operationStartTime).TotalMilliseconds.ToString() + " ms";
                
                pop3Client.Disconnect();
                
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            } finally {
                
                if (null != pop3Client) {
                    pop3Client.Disconnect();
                }
                
            }
            
            btnConnect.Enabled = true;
            Cursor = Cursors.Default;
        }

        private List<string> RetrieveMessages1(int[] messageNumbers)
        {

            List<string> messages = new List<string>();

            foreach (int number in messageNumbers)
            {
                messages.Add(pop3Client.GetMessage1(number));
            }

            return messages;
        }

        private List<string> RetrieveMessages(int[] messageNumbers) {
            
            List<string> messages = new List<string>();

            foreach (int number in messageNumbers) {
                messages.Add(pop3Client.GetMessage(number));
            }
            
            return messages;
        }
    }
}