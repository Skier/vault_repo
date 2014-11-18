
    /*******************************************************************
    * WebSiteAddress.as
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
        import Intuit.Sb.Cdm.vo.IdType;import defaultPackage.vo.String;import defaultPackage.vo.Date;        

        [Bindable]
        [RemoteClass(alias="Intuit.Sb.Cdm.WebSiteAddress")]
        public class WebSiteAddress
        {
          public function WebSiteAddress(){}
        
        
          public var Id:Intuit.Sb.Cdm.vo.IdType;
        
          public var URI:defaultPackage.vo.String;
        
          public var DateLastVerified:defaultPackage.vo.Date;
        
          public var DateLastVerifiedSpecified:Boolean;
        
          public var Default:Boolean;
        
          public var DefaultSpecified:Boolean;
        
          public var Tag:Array;
        

          public virtual function toString():String
          {
           return  this.Id + ": " 
                  + this.URI + ": " 
                  + this.DateLastVerified + ": " 
                  + this.DateLastVerifiedSpecified + ": " 
                  + this.Default + ": " 
                  + this.DefaultSpecified + ": " 
                  + this.Tag;
          }
      }
      
      }
      