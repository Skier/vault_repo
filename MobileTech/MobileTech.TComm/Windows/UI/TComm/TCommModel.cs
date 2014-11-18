using System;
using System.Collections.Generic;
using System.Text;
using MobileTech.Domain;
using System.Collections;
using System.Runtime.InteropServices;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Reflection;
using System.Xml;
using MobileTech.Data;
using MobileTech.ServiceLayer.VCHost;
#if WINCE
using System.Data.SqlServerCe;
#endif
using System.Diagnostics;

namespace MobileTech.Windows.UI.TComm
{
    public delegate void TCommProgressEvent(int percentComplete);
    public delegate void TCommMessageEvent(String message);
    public delegate void TCommDetailProgressEvent(int detailpercentComplete);
    public delegate void TCommDetailMessageEvent(String detailmessage);

    public class TCommModel : IModel
    {
        public event TCommMessageEvent Messages;
        public event TCommProgressEvent Progress;
        public event TCommDetailMessageEvent DetailMessages;
        public event TCommDetailProgressEvent DetailProgress;

        private bool m_executing = false;
        private int m_percenteComplete;
        private int m_percenteDetailComplete;

        public TCommModel()
        {
        }

        public bool DoTCom()
        {

            AddMessage(Resources.Message_TComm);
            bool bRetVal = true;
             SyncData syncData = new SyncData();
            syncData.Messages += new SyncMessageEvent(syncData_Messages);
            syncData.Progress += new SyncProgressEvent(syncData_Progress);
            #region AppStart
            if (Configuration.AppStart)
            {
                bRetVal = CheckVersion();
                bRetVal = UpdateVersion();

                if (!bRetVal)
                {
                    return false;
                }
                
#if WINCE
                if (Configuration.Sync)
                {
                    AddMessage(Resources.Message_Sync_Begin);
                    if (syncData.ReplSync(Configuration.InitDB))
                    {
                        Configuration.AppStart = false;

                        Route.ChangeStatus(RouteStatusEnum.IDL_TCOM_DONE);
                    }
                    else
                    {
                        return false;
                    }
                    AddMessage(Resources.Message_Sync_End);
                }
                else
                {
                    try
                    {
                        
                        AddMessage(Resources.Message_Sync_Emulator);
                        if (syncData.SyncEmulate())
                        {
                            Configuration.AppStart = false;
                            Route.ChangeStatus(RouteStatusEnum.IDL_TCOM_DONE);
                        }
                        else
                        {
                            return false;
                        }
                    }
                    catch (Exception ex)
                    {
                        bRetVal = false;
                        throw new MobileTechException(ex);
                    }
                    AddMessage(Resources.Message_Sync_Emulator_End);
                }

#else
                    AddMessage(Resources.Message_Sync_Emulator);
                    try
                    {
                        
                        if (syncData.SyncEmulate())
                        {
                            Configuration.AppStart = false;
                            Route.ChangeStatus(RouteStatusEnum.IDL_TCOM_DONE);
                        }
                        else
                        {
                            return false;
                        }
                    }
                    catch (Exception ex)
                    {
                        bRetVal = false;
                        throw new MobileTechException(ex);
                    }
                    AddMessage(Resources.Message_Sync_Emulator_End);
#endif
                Configuration.VCRestart = false;
                AddMessage(Resources.Message_TComm_End);
                return bRetVal;
            }
            #endregion

            if (!Configuration.VCRestart)
            {
                bRetVal = CheckVersion();

                if (!bRetVal)
                {
                    return false;
                }

                    
                    bool rv = false;
                    RouteStatusEnum routeStatus = Route.FindCurrent().Status;
                    rv = routeStatus == RouteStatusEnum.EOP_DONE;
                    if (rv)
                    {
                        if ((Configuration.TransExportXML) || (!Configuration.Sync))
                        {
                            AddMessage(Resources.Message_Sync_Export);
                            try
                            {
                                if (syncData.ExportTransactions())
                                {

                                }
                                else
                                {
                                    return false;
                                }
                            }
                            catch (Exception ex)
                            {
                                bRetVal = false;
                                throw new MobileTechException(ex);
                            }
                            AddMessage(Resources.Message_Sync_Export_End);
                        }
                        if (Configuration.Sync)
                        {
                            //Upload Transactions
                            try
                            {

                                //RDA
                                //syncData.SyncTransactions(SyncData.SyncTransactionType.Pull);
                                //Replication
                                //syncData.ReplTransSync();
                            }
                            catch (Exception ex)
                            {
                                bRetVal = false;
                                throw new MobileTechException(ex);
                            }

                        }
                        //Clear Transactions
                        AddMessage(Resources.Message_Sync_Clearing_Trans);
                        try
                        {
                            syncData.ClearTransactions();
                        }
                        catch (Exception ex)
                        {
                            bRetVal = false;
                            throw new MobileTechException(ex);
                        }
                        AddMessage(Resources.Message_Sync_Clearing_Trans_End);
                    }

                    //Run Database Script if needed
                    //AddMessage("Running Database Script", 20);
                    //Install Assemblies
                    
                    bRetVal = UpdateVersion();
                    
                    if (Configuration.VCUpdate)
                    {
                        return bRetVal;
                    }
            }

            //Two way Sync of Master Data
            if (Configuration.Sync)
            {
                AddMessage(Resources.Message_Sync_Master);
                try
                {

#if WINCE
                        if (syncData.ReplMasterSync())
                        {
                            Route.ChangeStatus(RouteStatusEnum.EOP_TCOM_DONE);
                        }
                        else
                        {

                        }

#else
                        //syncData.ReplMasterSync();
                        Route.ChangeStatus(RouteStatusEnum.EOP_TCOM_DONE);
#endif


                }
                catch (Exception ex)
                {
                    bRetVal = false;
                    throw new MobileTechException(ex);
                }
                AddMessage(Resources.Message_Sync_Master_End);
            }
            else
            {
                AddMessage(Resources.Message_Sync_Emulator);
                //syncData.ReplMasterSync();
                Route.ChangeStatus(RouteStatusEnum.EOP_TCOM_DONE);

                AddMessage(Resources.Message_Sync_Emulator_End);

            }

            Configuration.VCRestart = false;
            Item.UpdateIndex();
            AddMessage(Resources.Message_TComm_End);
  
            return bRetVal;

        }

        #region IModel Members


        public void Init()
        {

        }

        #endregion
        #region TCommEvents
        public bool IsExecuting
        {
            get { return m_executing; }
        }

        public int PercentComplete
        {
            get { return m_percenteComplete; }
        }
		protected void SetPercent(int percentComplete)
		{
			m_percenteComplete = percentComplete;

			if (Progress != null)
				Progress.Invoke(percentComplete);
		}

		protected void AddMessage(String message)
		{
			if (Messages != null)
				Messages.Invoke(message);
		}

        #endregion
        #region TCommDetailEvents
        public int DetailPercentComplete
        {
            get { return m_percenteDetailComplete; }
        }
        protected void DetailSetPercent(int detailpercentComplete)
        {
            m_percenteDetailComplete = detailpercentComplete;

            if (DetailProgress != null)
                DetailProgress.Invoke(detailpercentComplete);
        }

        protected void DetailAddMessage(String detailmessage)
        {
            if (DetailMessages != null)
                DetailMessages.Invoke(detailmessage);
        }

        #endregion
        #region SyncData Events
        void syncData_Progress(int percentComplete)
        {
            DetailSetPercent(percentComplete);
        }

        void syncData_Messages(string message)
        {
            DetailAddMessage(message);
        }
        #endregion
        protected bool CheckVersion()
        {
            VersionControlAgt vcAgt = new VersionControlAgt();
            bool bRetVal = true; 
            //MGS VC info and Get Database scripts if needed
            if (Configuration.VC)
            {
                AddMessage(Resources.Message_VC_Begin);
                try
                {
                    bRetVal = vcAgt.Start();
                }
                catch (Exception ex)
                {
                    bRetVal = false;
                    throw new MobileTechException(ex);
                }

                AddMessage(Resources.Message_VC_End);

            }
            return bRetVal;
        }
        protected bool UpdateVersion()
        {
            VersionControlAgt vcAgt = new VersionControlAgt();
            bool bRetVal = true;
            if (Configuration.VC && Configuration.VCUpdate)
            {
                AddMessage(Resources.Message_VC_Update);
                try
                {
                    vcAgt.ExecuteUpdate();
                }
                catch (Exception ex)
                {
                    bRetVal = false;
                    throw new MobileTechException(ex);
                }
                AddMessage(Resources.Message_VC_Update_End);
            }
            return bRetVal;
        }
    }
}

