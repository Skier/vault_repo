
    /*******************************************************************
    * PlatformSessionContext.as
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
    
        package Intuit.Platform.Client.Core.vo
        {
        import flash.utils.ByteArray;
        import mx.collections.ArrayCollection;
        import Intuit.Common.Util.vo.WorkNotification;import defaultPackage.vo.String;import defaultPackage.vo.String;import defaultPackage.vo.String;import defaultPackage.vo.String;import defaultPackage.vo.String;import defaultPackage.vo.String;import defaultPackage.vo.String;import defaultPackage.vo.String;import defaultPackage.vo.String;import defaultPackage.vo.String;import defaultPackage.vo.String;        

        [Bindable]
        [RemoteClass(alias="Intuit.Platform.Client.Core.PlatformSessionContext")]
        public class PlatformSessionContext
        {
          public function PlatformSessionContext(){}
        
        
          public var SyncInvoke:Object;
        
          public var WorkNotification:Intuit.Common.Util.vo.WorkNotification;
        
          public var Host:Object;
        
          public var AppToken:defaultPackage.vo.String;
        
          public var HasAppToken:Boolean;
        
          public var LogPlatformRequestsToDisk:Boolean;
        
          public var LogDiagnosticDetailsOnRequestErrorsToNotifier:Boolean;
        
          public var PlatformRequestLoggingToDiskFilePrefix:defaultPackage.vo.String;
        
          public var UserID:defaultPackage.vo.String;
        
          public var Password:defaultPackage.vo.String;
        
          public var Ticket:defaultPackage.vo.String;
        
          public var HasTicket:Boolean;
        
          public var RequestAuthorizer:Object;
        
          public var MaxLenAppName:int;
        
          public var MaxQueryParameters:int;
        
          public var CListAll:defaultPackage.vo.String;
        
          public var QueryComparisonOp_EX:defaultPackage.vo.String;
        
          public var QueryCriteriaCombinationOp_OR:defaultPackage.vo.String;
        
          public var QueryCriteriaCombinationOp_AND:defaultPackage.vo.String;
        
          public var CheckBoxChecked:defaultPackage.vo.String;
        
          public var CheckBoxUnchecked:defaultPackage.vo.String;
        

          public virtual function toString():String
          {
           return  this.SyncInvoke + ": " 
                  + this.WorkNotification + ": " 
                  + this.Host + ": " 
                  + this.AppToken + ": " 
                  + this.HasAppToken + ": " 
                  + this.LogPlatformRequestsToDisk + ": " 
                  + this.LogDiagnosticDetailsOnRequestErrorsToNotifier + ": " 
                  + this.PlatformRequestLoggingToDiskFilePrefix + ": " 
                  + this.UserID + ": " 
                  + this.Password + ": " 
                  + this.Ticket + ": " 
                  + this.HasTicket + ": " 
                  + this.RequestAuthorizer + ": " 
                  + this.MaxLenAppName + ": " 
                  + this.MaxQueryParameters + ": " 
                  + this.CListAll + ": " 
                  + this.QueryComparisonOp_EX + ": " 
                  + this.QueryCriteriaCombinationOp_OR + ": " 
                  + this.QueryCriteriaCombinationOp_AND + ": " 
                  + this.CheckBoxChecked + ": " 
                  + this.CheckBoxUnchecked;
          }
      }
      
      }
      