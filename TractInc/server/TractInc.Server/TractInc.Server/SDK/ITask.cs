using System;
using System.Collections.Generic;
using System.Text;

namespace TractInc.Server.SDK
{
    public delegate void TaskProgressEvent(int percentComplete);
    public delegate void TaskMessageEvent(String message);


    public interface ITask
    {
        void Execute();

        bool IsExecuting { get; }

        int PercentComplete { get;}

        event TaskMessageEvent Messages;
        event TaskProgressEvent Progress; 

    }
}
