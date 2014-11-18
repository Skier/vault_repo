
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace MobileTech
{
   public class ActivationMonitor : MarshalByRefObject 
   {
      public void ActivateApplication()
      {
         Form form = SingletonApp.MainForm;
         if(form != null)
         {
            if(!form.IsDisposed)
            {
               //This executes on the thread from the thread pool. Need to marshal to the form
               //Use anonymous method to wrap WindowState property
               if(form.WindowState == FormWindowState.Minimized)
               {
                  GenericEventHandler del = delegate()
                  {
                     form.WindowState = FormWindowState.Normal;
                  };
                  form.Invoke(del,new object[]{});
               }
               EventsHelper.Fire(form.Activate);
            }
         } 
      }
   }
}
