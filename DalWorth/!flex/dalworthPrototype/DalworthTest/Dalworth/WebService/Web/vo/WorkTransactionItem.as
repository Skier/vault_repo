
    /*******************************************************************
    * WorkTransactionItem.as
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
    
        package Dalworth.WebService.Web.vo
        {
        [Bindable]
        [RemoteClass(alias="Dalworth.WebService.Domain.WorkTransactionItem")]
      public class WorkTransactionItem
        {
          public function WorkTransactionItem(){}
        
          
            public var ID:Number;
          
            public var WorkTransactionlId:Number;
          
            public var ItemId:Object;
          
            public var IsLeft:Boolean;
          
            public var IsCaptured:Boolean;
          
            public var CounterValue:Number;
          
            public var CounterName:String;
          
        }
      
      }
      