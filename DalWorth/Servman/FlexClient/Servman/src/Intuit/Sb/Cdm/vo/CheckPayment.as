
    /*******************************************************************
    * CheckPayment.as
    * Copyright (C) 2006-2010 Midnight Coders, Inc.
    *
    * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
    * EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
    * MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
    * NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
    * LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
    * OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
    * WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
    ********************************************************************/
    
        package Intuit.Sb.Cdm.vo
        {
        import flash.utils.ByteArray;
        import mx.collections.ArrayCollection;
                

        [Bindable]
        [RemoteClass(alias="Intuit.Sb.Cdm.CheckPayment")]
        public class CheckPayment
        {
          public function CheckPayment(){}
        
        
          public var CheckNum:String;
        
          public var Status:String;
        
          public var NameOnAcct:String;
        
          public var AcctNum:String;
        
          public var RoutingNum:String;
        
          public var BankName:String;
        

          public virtual function toString():String
          {
           return  this.CheckNum + ": " 
                  + this.Status + ": " 
                  + this.NameOnAcct + ": " 
                  + this.AcctNum + ": " 
                  + this.RoutingNum + ": " 
                  + this.BankName;
          }
      }
      
      }
      