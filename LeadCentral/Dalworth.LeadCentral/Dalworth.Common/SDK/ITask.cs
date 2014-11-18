using System;

namespace Dalworth.Common.SDK
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
