
    /*******************************************************************
    * Work.as
    * Copyright (C) 2006 Midnight Coders, LLC
    *
    * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
    * EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
    * MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
    * NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
    * LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
    * OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
    * WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
    ********************************************************************/
    
        package Domain1
        {
        [Bindable]
        [RemoteClass(alias="Dalworth.WebService.Domain.Work")]
      public class Work
        {
          public function Work(){}
        
          
            public var ID:Number;
          
            public var DispatchEmployeeId:Number;
          
            public var TechnicianEmployeeId:Number;
          
            public var VanId:Number;
          
            public var StartDate:Object;
          
            public var WorkStatusId:Object;
          
            public var StartMessage:String;
          
            public var EndMessage:String;
          
            public var EquipmentNotes:String;
          
            public var CounterValue:Number;
          
            public var CounterName:String;
          
            public var WorkStatus:Object;
          
        }
      
      }
      