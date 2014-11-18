
    /*******************************************************************
    * JobType.as
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
        import mx.collections.ArrayCollection;        

        [Bindable]
        [RemoteClass(alias="Intuit.Sb.Cdm.JobType")]
        public class JobType extends Intuit.Sb.Cdm.vo.CdmBase
        {
          public function JobType(){}
        
          public var Name:String;
        
          public var JobTypeParentId:IdType;
        
          public var JobTypeParentName:String;
        
          public var Active:Boolean;
        
          public var ActiveSpecified:Boolean;
          

          public override function toString():String
          {
           return  this.Name + ": " 
                  + this.JobTypeParentId + ": " 
                  + this.JobTypeParentName + ": " 
                  + this.Active + ": " 
                  + this.ActiveSpecified;
          }
      }
      
      }
      