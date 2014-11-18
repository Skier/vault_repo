
    /*******************************************************************
    * WorkTransactionEquipment.as
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
    
        package Domain
        {
        [Bindable]
        [RemoteClass(alias="Dalworth.WebService.Domain.WorkTransactionEquipment")]
      public class WorkTransactionEquipment
        {
          public function WorkTransactionEquipment(){}
        
          
            public var CounterValue:Number;
          
            public var CounterName:String;
          
            public var ID:Number;
          
            public var WorkTransactionId:Number;
          
            public var EquipmentId:Number;
          
            public var IsLeft:Boolean;
          
            public var IsCaptured:Boolean;
          
        }
      
      }
      