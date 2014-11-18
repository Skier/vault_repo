
    /*******************************************************************
    * CustomerType.as
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
        [RemoteClass(alias="Intuit.Sb.Cdm.CustomerType")]
        public class CustomerType extends Intuit.Sb.Cdm.vo.CdmBase
        {
          public function CustomerType(){}
        
        
          public var Name:String;
        
          public var CustomerTypeParentId:Intuit.Sb.Cdm.vo.IdType;
        
          public var CustomerTypeParentName:String;
        
          public var Active:Boolean;
        
          public var ActiveSpecified:Boolean;
        

          public override function toString():String
          {
           return  this.Name + ": " 
                  + this.CustomerTypeParentId + ": " 
                  + this.CustomerTypeParentName + ": " 
                  + this.Active + ": " 
                  + this.ActiveSpecified;
          }
      }
      
      }
      