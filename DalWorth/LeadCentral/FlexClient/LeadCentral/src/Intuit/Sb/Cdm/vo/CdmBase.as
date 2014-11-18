
    /*******************************************************************
    * CdmBase.as
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
        [RemoteClass(alias="Intuit.Sb.Cdm.CdmBase")]
        public class CdmBase extends Intuit.Sb.Cdm.vo.CdmObject
        {
          public function CdmBase(){}
        
        
          public var Id:IdType;
          public function get IdStr():String { return (Id != null) ? Id.toString() : ""; }
        
          public var SyncToken:String;
        
          public var MetaData:ModificationMetaData;
        
          public var ExternalKey:IdType;
          public function get ExternalKeyStr():String { return Id.toString(); }
        
          public var Synchronized:Boolean;
        
          public var SynchronizedSpecified:Boolean;
        
          public var AlternateId:Array;
        
          public var CustomField:Array;
        
          public var Draft:Boolean;
        
          public var DraftSpecified:Boolean;
        

          public override function toString():String
          {
           return  this.Id + ": " 
                  + this.SyncToken + ": " 
                  + this.MetaData + ": " 
                  + this.ExternalKey + ": " 
                  + this.Synchronized + ": " 
                  + this.SynchronizedSpecified + ": " 
                  + this.AlternateId + ": " 
                  + this.CustomField + ": " 
                  + this.Draft + ": " 
                  + this.DraftSpecified;
          }
      }
      
      }
      