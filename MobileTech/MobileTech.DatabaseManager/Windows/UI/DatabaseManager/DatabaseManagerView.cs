using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MobileTech.ServiceLayer;
using MobileTech.Domain;
using MobileTech.Data;
using System.IO;
using System.Threading;

namespace MobileTech.Windows.UI.DatabaseManager
{
    public partial class DatabaseManagerView : BaseForm
    {
        public DatabaseManagerView()
        {
            InitializeComponent();
        }

        private void OnPopulateItemsClick(object sender, EventArgs e)
        {
            OnPopulateItems();
        }

        private void OnPopulateItems()
        {

            PopulateItemsTask task = new PopulateItemsTask();

            AttachTask(task);

            task.ItemsCount = int.Parse(m_txItemsCount.Text);

            try
            {
                m_btPopulateItems.Enabled = false;
                
                Refresh();

                using (WaitCursor cursor = new WaitCursor())
                {
                    task.Execute();
                }
            }
            catch (Exception ex)
            {
                EventService.AddEvent(new MobileTechException(ex));
            }
            finally
            {
                DetachTask(task);

                m_btPopulateItems.Enabled = true;
            }
        }

        private void CreateDatabase()
        {
            try
            {
#if WINCE
                if (Database.IsDatabaseExist())
                {
                    if (MessageBox.Show("Database file already exists, do you want create new ?\nWarning: all data will be missed.",
                        "MobileTech",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button2) == DialogResult.No)
                    {
                        return;
                    }

                    Refresh();

                    Database.Close();

                    int attempt = 0;

                    while (true)
                    {
                        try
                        {
                            File.Delete(Configuration.DBFullPath);

                            break;
                        }
                        catch (Exception ex)
                        {
                            if (++attempt < 5)
                                Thread.Sleep(1000);
                            else
                                throw ex;
                        }
                    }
                }
#endif
                using (WaitCursor waitCursor = new WaitCursor())
                {
                    Database.CreateDatabase();
                }

                if (MessageBox.Show("Database was successfully created.\nInsert initional data ?",
                    "MobileTech",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    Refresh();

                    using (WaitCursor waitCursor = new WaitCursor())
                    {
#if WINCE
                        Database.ExecuteScript(Path.GetDirectoryName(MobileTech.Configuration.AppNameFullPath) 
                            + @"\Database\dbInitialize.sql");
#else
                        Database.ExecuteScript(@"Database\dbInitialize.sql");
#endif
                    }
                }



            }
            catch (MobileTechException ex)
            {
                EventService.AddEvent(ex);
            }
            catch (Exception ex)
            {
                EventService.AddEvent(new MobileTechException(ex));
            }
        }

        void OnTaskProgress(int percentComplete)
        {
            m_progress.Value = percentComplete;
        }

        void OnTaskMessages(string message)
        {
            m_lbProgressMessage.Text = message;
            m_lbProgressMessage.Refresh();
        }

        private void OnIndexItemsClick(object sender, EventArgs e)
        {
            int affectedCount = 0;

            using (WaitCursor cursor = new WaitCursor())
            {
                affectedCount = Item.UpdateIndex();
            }

            MessageBox.Show(String.Format("Affected row count : {0}", affectedCount));

        }

        private void OnCreateRestoreClick(object sender, EventArgs e)
        {
            CreateDatabase();
        }

        private void m_linkExport_Click(object sender, EventArgs e)
        {
            OnExport();
        }

        private void OnExport()
        {
            using (WaitCursor waitCursor = new WaitCursor())
            {
                Export export = new Export(Host.GetPath("Database\\Import"));

                try
                {
                    AttachTask(export);

                    export.Execute();

                }
                catch (Exception ex)
                {
                    EventService.AddEvent(new MobileTechException(ex));
                }
                finally
                {
                    DetachTask(export);
                }
            }
        }

        private void m_linkImport_Click(object sender, EventArgs e)
        {
            OnImport();
        }

        private void OnImport()
        {
            using (WaitCursor waitCursor = new WaitCursor())
            {
                Import import = new Import(Host.GetPath("Database\\Import"));

                try
                {
                    AttachTask(import);

                    import.Clear = m_chClear.Checked;

                    import.Execute();
                }
                catch (Exception ex)
                {
                    EventService.AddEvent(new MobileTechException(ex));
                }
                finally
                {
                    DetachTask(import);
                }
            }
        }

        private void DetachTask(Task task)
        {
            task.Messages -= new TaskMessageEvent(OnTaskMessages);
            task.Progress -= new TaskProgressEvent(OnTaskProgress);
        }

        private void AttachTask(Task task)
        {
            task.Messages += new TaskMessageEvent(OnTaskMessages);
            task.Progress += new TaskProgressEvent(OnTaskProgress);
        }
    }
}