
    /*******************************************************************
    * Employee.as
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
        [RemoteClass(alias="Dalworth.WebService.Domain.Employee")]
      public class Employee
        {
          public function Employee(){}
        
          
            public var ID:int;
          
            public var EmployeeTypeId:int;
          
            public var AddressId:Object;
          
            public var FirstName:String;
          
            public var LastName:String;
          
            public var HireDate:Object;
          
            public var Phone1:String;
          
            public var Phone2:String;
          
            public var Password:String;
          
            public var DisplayName:String;
          
        }
      
      }
      