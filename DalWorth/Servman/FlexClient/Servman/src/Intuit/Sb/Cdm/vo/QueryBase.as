
    /*******************************************************************
    * QueryBase.as
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
        import defaultPackage.vo.String;import defaultPackage.vo.String;        

        [Bindable]
        [RemoteClass(alias="Intuit.Sb.Cdm.QueryBase")]
        public class QueryBase
        {
          public function QueryBase(){}
        
        
          public var OfferingId:String;
        
          public var OfferingIdSpecified:Boolean;
        
          public var IteratorId:defaultPackage.vo.String;
        
          public var ChunkSize:defaultPackage.vo.String;
        
          public var IncludeTagElements:Array;
        
          public var DeletedObjects:Boolean;
        

          public virtual function toString():String
          {
           return  this.OfferingId + ": " 
                  + this.OfferingIdSpecified + ": " 
                  + this.IteratorId + ": " 
                  + this.ChunkSize + ": " 
                  + this.IncludeTagElements + ": " 
                  + this.DeletedObjects;
          }
      }
      
      }
      