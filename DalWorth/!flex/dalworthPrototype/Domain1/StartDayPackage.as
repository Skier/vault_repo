
    /*******************************************************************
    * StartDayPackage.as
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
        [RemoteClass(alias="Dalworth.WebService.Domain.StartDayPackage")]
      public class StartDayPackage
        {
          public function StartDayPackage(){}
        
          
            public var Customers:Array;
          
            public var Equipments:Array;
          
            public var Items:Array;
          
            public var Van:Van;
          
            public var Jobs:Array;
          
            public var Tickets:Array;
          
            public var TicketItemDeliveries:Array;
          
            public var Work:Work;
          
            public var WorkDetails:Array;
          
            public var WorkEquipments:Array;
          
        }
      
      }
      