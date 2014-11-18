using System;


namespace Dalworth.Common.SDK
{
    public abstract class Task:ITask
    {
        private bool Executing;
        private int PercenteComplete;

        protected abstract void Main();

        protected void SetPercent(int percentComplete)
        {
            PercenteComplete = percentComplete;

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
            if (Executing)
                throw new DalworthException("Task already running");

            Executing = true;
            PercenteComplete = 0;


            try
            {
                Main();
            }
            finally
            {
                Executing = false;

                SetPercent(100);
            }
        }



        public bool IsExecuting
        {
            get { return Executing; }
        }

        public int PercentComplete
        {
            get { return PercenteComplete; }
        }

        public event TaskMessageEvent Messages;

        public event TaskProgressEvent Progress;



        #endregion
    }
}
