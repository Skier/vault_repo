
    /*******************************************************************
    * WorkDetail.as
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
        [RemoteClass(alias="Dalworth.WebService.Domain.WorkDetail")]
      public class WorkDetail
        {
          public function WorkDetail(){}
        
          	public var WorkTicket:Ticket;
          	
            public var ID:int;
          
            public var WorkId:int;
          
            public var TicketId:int;
          
            public var Sequence:Object;
          
            public var WorkDetailStatusId:Object;
          
            public var CounterValue:int;
          
            public var CounterName:String;
          
        }
      
      }
      