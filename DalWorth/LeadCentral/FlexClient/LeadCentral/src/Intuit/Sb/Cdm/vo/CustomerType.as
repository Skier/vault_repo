
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
        import mx.collections.ArrayCollection;
        import mx.collections.ICollectionView;        

        [Bindable]
        [RemoteClass(alias="Intuit.Sb.Cdm.CustomerType")]
        public class CustomerType extends Intuit.Sb.Cdm.vo.CdmBase
        {
          public function CustomerType()
          {
          	relatedTypes = new ArrayCollection();
          }
        
        
          public var Name:String;
        
          public var CustomerTypeParentId:Intuit.Sb.Cdm.vo.IdType;
        
          public var CustomerTypeParentName:String;
        
          public var Active:Boolean;
        
          public var ActiveSpecified:Boolean;
          
          
          public function parent():Object
          {
          	return parentType;
          }
          
          public function get children():ICollectionView
          {
          	if (relatedTypes.length > 0)
          		return relatedTypes;
          	else 
          		return null;
          }
          
          public var parentType:CustomerType;
          public var relatedTypes:ArrayCollection;
          public function get level():int
          {
          	var result:int = 0;
          	var current:CustomerType = this;
          	while(current.parentType != null)
          	{
          		result++;
          		current = current.parentType;
          	}
          	return result;
          }

		public static function getEmpty():CustomerType 
		{
			var result:CustomerType = new CustomerType();
			result.Name = "";
			result.Id = null;
			return result;
		}        

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
      