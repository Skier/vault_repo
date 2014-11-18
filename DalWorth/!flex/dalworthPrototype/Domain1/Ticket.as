
    /*******************************************************************
    * Ticket.as
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
        [RemoteClass(alias="Dalworth.WebService.Domain.Ticket")]
      public class Ticket
        {
          public function Ticket(){}
        
          
            public var ID:Number;
          
            public var JobId:Number;
          
            public var TicketTypeId:Number;
          
            public var TicketStatusId:Object;
          
            public var Number:String;
          
            public var CreateDate:Object;
          
            public var ServiceDate:Object;
          
            public var Description:String;
          
            public var Message:String;
          
            public var Notes:String;
          
            public var TicketStatus:Object;
          
        }
      
      }
      