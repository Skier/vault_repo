package com.ebs.eroof.control
{
   import com.quickbase.idn.control.QuickBaseMSAControl;
   //$$IMPORT_TAG$$ -- Do not remove this comment.  It is used by the event generation wizard!
 
   // Basic front controller for the application, MUST inherit from the 
   // Kingussie framework controller.
   public class eRoofNativeController extends QuickBaseMSAControl
   {
         public function eRoofNativeController() 
         {
               super();  //MUST call base class constructor FIRST.
               //TODO:  Define your business logic event maps here
               //       for example:
               //       this.addCommand(EVENT_SAMPLE, SampleCmd);
               //$$ADD_COMMAND_TAG$$ -- Do not remove this comment.  It is used by the event generation wizard!
        }
         
         //define custom event constants here.  For example:
         //public static const EVENT_SAMPLE:String = "sample";
         //$$EVENT_CONSTANTS_TAG$$ -- Do not remove this comment.  It is used by the event generation wizard!
   }
}
