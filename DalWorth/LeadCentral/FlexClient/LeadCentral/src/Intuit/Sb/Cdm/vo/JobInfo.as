
    /*******************************************************************
    * JobInfo.as
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
        import Intuit.Sb.Cdm.vo.IdType;        

        [Bindable]
        [RemoteClass(alias="Intuit.Sb.Cdm.JobInfo")]
        public class JobInfo
        {
          public function JobInfo(){}
        
        
          public var Status:String;
        
          public var StatusSpecified:Boolean;
        
          public var StartDate:Date;
        
          public var StartDateSpecified:Boolean;
        
          public var ProjectedEndDate:Date;
        
          public var ProjectedEndDateSpecified:Boolean;
        
          public var EndDate:Date;
        
          public var EndDateSpecified:Boolean;
        
          public var Description:String;
        
          public var JobTypeId:IdType;
        
          public var JobTypeName:String;
        

          public virtual function toString():String
          {
           return  this.Status + ": " 
                  + this.StatusSpecified + ": " 
                  + this.StartDate + ": " 
                  + this.StartDateSpecified + ": " 
                  + this.ProjectedEndDate + ": " 
                  + this.ProjectedEndDateSpecified + ": " 
                  + this.EndDate + ": " 
                  + this.EndDateSpecified + ": " 
                  + this.Description + ": " 
                  + this.JobTypeId + ": " 
                  + this.JobTypeName;
          }
      }
      
      }
      