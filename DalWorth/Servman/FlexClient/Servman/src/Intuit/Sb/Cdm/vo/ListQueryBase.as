
    /*******************************************************************
    * ListQueryBase.as
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
        import defaultPackage.vo.Date;        

        [Bindable]
        [RemoteClass(alias="Intuit.Sb.Cdm.ListQueryBase")]
        public class ListQueryBase extends Intuit.Sb.Cdm.vo.QueryBase
        {
          public function ListQueryBase(){}
        
        
          public var CDCAsOf:defaultPackage.vo.Date;
        
          public var CDCAsOfSpecified:Boolean;
        
          public var SynchronizedFilter:String;
        
          public var SynchronizedFilterSpecified:Boolean;
        
          public var DraftFilter:String;
        
          public var DraftFilterSpecified:Boolean;
        
          public var CustomFieldEnable:Boolean;
        
          public var CustomFieldEnableSpecified:Boolean;
        
          public var CustomFieldQueryParam:Array;
        
          public var CustomFieldFilter:String;
        
          public var CustomFieldFilterSpecified:Boolean;
        
          public var CustomFieldDefinitionIdSet:Array;
        
          public var Items:Array;
        
          public var ItemsElementName:Array;
        
          public var ErroredObjectsOnly:Boolean;
        
          public var ActiveOnly:Boolean;
        

          public override function toString():String
          {
           return  this.CDCAsOf + ": " 
                  + this.CDCAsOfSpecified + ": " 
                  + this.SynchronizedFilter + ": " 
                  + this.SynchronizedFilterSpecified + ": " 
                  + this.DraftFilter + ": " 
                  + this.DraftFilterSpecified + ": " 
                  + this.CustomFieldEnable + ": " 
                  + this.CustomFieldEnableSpecified + ": " 
                  + this.CustomFieldQueryParam + ": " 
                  + this.CustomFieldFilter + ": " 
                  + this.CustomFieldFilterSpecified + ": " 
                  + this.CustomFieldDefinitionIdSet + ": " 
                  + this.Items + ": " 
                  + this.ItemsElementName + ": " 
                  + this.ErroredObjectsOnly + ": " 
                  + this.ActiveOnly;
          }
      }
      
      }
      