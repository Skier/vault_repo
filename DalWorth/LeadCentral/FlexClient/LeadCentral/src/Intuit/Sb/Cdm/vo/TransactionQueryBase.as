
    /*******************************************************************
    * TransactionQueryBase.as
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
        import defaultPackage.vo.Date;import Intuit.Sb.Cdm.vo.IdSet;import defaultPackage.vo.Date;import defaultPackage.vo.Date;import defaultPackage.vo.Date;import defaultPackage.vo.Date;import defaultPackage.vo.Date;import defaultPackage.vo.Date;        

        [Bindable]
        [RemoteClass(alias="Intuit.Sb.Cdm.TransactionQueryBase")]
        public class TransactionQueryBase extends Intuit.Sb.Cdm.vo.QueryBase
        {
          public function TransactionQueryBase(){}
        
        
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
        
          public var Item:Intuit.Sb.Cdm.vo.IdSet;
        
          public var ItemElementName:String;
        
          public var StartCreatedTMS:defaultPackage.vo.Date;
        
          public var StartCreatedTMSSpecified:Boolean;
        
          public var EndCreatedTMS:defaultPackage.vo.Date;
        
          public var EndCreatedTMSSpecified:Boolean;
        
          public var StartModifiedTMS:defaultPackage.vo.Date;
        
          public var StartModifiedTMSSpecified:Boolean;
        
          public var EndModifiedTMS:defaultPackage.vo.Date;
        
          public var EndModifiedTMSSpecified:Boolean;
        
          public var StartTransactionDate:defaultPackage.vo.Date;
        
          public var StartTransactionDateSpecified:Boolean;
        
          public var EndTransactionDate:defaultPackage.vo.Date;
        
          public var EndTransactionDateSpecified:Boolean;
        
          public var ThinObject:Boolean;
        
          public var ThinObjectSpecified:Boolean;
        
          public var TemplateRefMapEnable:Boolean;
        
          public var TemplateRefMapEnableSpecified:Boolean;
        
          public var ErroredObjectsOnly:Boolean;
        
          public var IncludeGroupMemberFlag:Boolean;
        
          public var IncludeGroupMemberFlagSpecified:Boolean;
        

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
                  + this.Item + ": " 
                  + this.ItemElementName + ": " 
                  + this.StartCreatedTMS + ": " 
                  + this.StartCreatedTMSSpecified + ": " 
                  + this.EndCreatedTMS + ": " 
                  + this.EndCreatedTMSSpecified + ": " 
                  + this.StartModifiedTMS + ": " 
                  + this.StartModifiedTMSSpecified + ": " 
                  + this.EndModifiedTMS + ": " 
                  + this.EndModifiedTMSSpecified + ": " 
                  + this.StartTransactionDate + ": " 
                  + this.StartTransactionDateSpecified + ": " 
                  + this.EndTransactionDate + ": " 
                  + this.EndTransactionDateSpecified + ": " 
                  + this.ThinObject + ": " 
                  + this.ThinObjectSpecified + ": " 
                  + this.TemplateRefMapEnable + ": " 
                  + this.TemplateRefMapEnableSpecified + ": " 
                  + this.ErroredObjectsOnly + ": " 
                  + this.IncludeGroupMemberFlag + ": " 
                  + this.IncludeGroupMemberFlagSpecified;
          }
      }
      
      }
      