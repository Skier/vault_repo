
    /*******************************************************************
    * ModificationMetaData.as
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
        [RemoteClass(alias="Intuit.Sb.Cdm.ModificationMetaData")]
        public class ModificationMetaData
        {
          public function ModificationMetaData(){}
        
        
          public var CreatedBy:String;
        
          public var CreatedById:String;
        
          public var CreateTime:Date;
        
          public var CreateTimeSpecified:Boolean;
        
          public var LastModifiedBy:String;
        
          public var LastModifiedById:String;
        
          public var LastUpdatedTime:Date;
        
          public var LastUpdatedTimeSpecified:Boolean;
        

          public virtual function toString():String
          {
           return  this.CreatedBy + ": " 
                  + this.CreatedById + ": " 
                  + this.CreateTime + ": " 
                  + this.CreateTimeSpecified + ": " 
                  + this.LastModifiedBy + ": " 
                  + this.LastModifiedById + ": " 
                  + this.LastUpdatedTime + ": " 
                  + this.LastUpdatedTimeSpecified;
          }
      }
      
      }
      