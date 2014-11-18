using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;


namespace TractInc.Server.SDK
{
    public abstract class Task:ITask
    {
        private bool m_executing;
        private int m_percenteComplete;

        protected abstract void Main();

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

        protected void AddMessage(String message,int percentComplete)
        {
            if (Messages != null)
                Messages.Invoke(message);

            SetPercent(percentComplete);
        }

        #region ITask Members

        public void Execute()
        {
            if (m_executing)
                throw new TractIncException("Task already running");

            m_executing = true;
            m_percenteComplete = 0;


            try
            {
                Main();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                m_executing = false;

                SetPercent(100);
            }
        }



        public bool IsExecuting
        {
            get { return m_executing; }
        }

        public int PercentComplete
        {
            get { return m_percenteComplete; }
        }

        public event TaskMessageEvent Messages;

        public event TaskProgressEvent Progress;



        #endregion
    }
}
