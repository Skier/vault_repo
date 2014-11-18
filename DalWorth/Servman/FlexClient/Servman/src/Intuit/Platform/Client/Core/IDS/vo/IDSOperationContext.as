
    /*******************************************************************
    * IDSOperationContext.as
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
    
        package Intuit.Platform.Client.Core.IDS.vo
        {
        import flash.utils.ByteArray;
        import mx.collections.ArrayCollection;
        import defaultPackage.vo.String;import defaultPackage.vo.String;import defaultPackage.vo.String;import defaultPackage.vo.String;import defaultPackage.vo.String;        

        [Bindable]
        [RemoteClass(alias="Intuit.Platform.Client.Core.IDS.IDSOperationContext")]
        public class IDSOperationContext
        {
          public function IDSOperationContext(){}
        
        
          public var Resource:String;
        
          public var RealmId:defaultPackage.vo.String;
        
          public var EntityId:defaultPackage.vo.String;
        
          public var OfferingId:defaultPackage.vo.String;
        
          public var Users:defaultPackage.vo.String;
        
          public var RoleCommand:defaultPackage.vo.String;
        
          public var CompanyParameters:Array;
        
          public var Parameters:Array;
        

          public virtual function toString():String
          {
           return  this.Resource + ": " 
                  + this.RealmId + ": " 
                  + this.EntityId + ": " 
                  + this.OfferingId + ": " 
                  + this.Users + ": " 
                  + this.RoleCommand + ": " 
                  + this.CompanyParameters + ": " 
                  + this.Parameters;
          }
      }
      
      }
      