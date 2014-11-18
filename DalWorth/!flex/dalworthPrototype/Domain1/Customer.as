
    /*******************************************************************
    * Customer.as
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
        [RemoteClass(alias="Dalworth.WebService.Domain.Customer")]
      public class Customer
        {
          public function Customer(){}
        
          
            public var ID:Number;
          
            public var AddressId:Object;
          
            public var FirstName:String;
          
            public var LastName:String;
          
            public var Phone1:String;
          
            public var Phone2:String;
          
        }
      
      }
      