package UI.Management.ServiceBrowser
{
	import mx.controls.treeClasses.ITreeDataDescriptor;
	import mx.collections.ICollectionView;
	import mx.collections.ArrayCollection;
	import mx.rpc.xml.DataType;
	import mx.utils.ObjectUtil;

	
	public class ServiceTreeDataDescriptor
		implements ITreeDataDescriptor
	{
		
		public function getData(node:Object, model:Object=null):Object
		{
			return node ;
		}
		
		public function hasChildren(node:Object, model:Object=null):Boolean
		{
			/*var serviceNode:ServiceNode = ServiceNode(node);
			
			if(serviceNode.Items.length > 0 )
				return true;
				
			if(serviceNode.IsDataTypeContainer())
			{
				var serviceDataTypeContainer:ServiceDataTypeContainer = ServiceDataTypeContainer(node);
				
				return serviceDataTypeContainer.DataType.IsArray() || serviceDataTypeContainer.DataType.IsComplexType();
			}
				
			return false;*/
			
			return ServiceNode(node).Items.length > 0;
		}
		
		public function addChildAt(parent:Object, newChild:Object, index:int, model:Object=null):Boolean
		{
			return true;
		}
		
		public function isBranch(node:Object, model:Object=null):Boolean
		{
			return !(node is ServiceMethod) && hasChildren(node);
		}
		
		public function removeChildAt(parent:Object, child:Object, index:int, model:Object=null):Boolean
		{
			ServiceNode(parent).Items.slice(index,index);
			
			return true;
		}
		
		public function getChildren(node:Object, model:Object=null):ICollectionView
		{
			
			/*if(ServiceNode(node).IsMethodArg())
			{
				var serviceMethodArg:ServiceMethodArg = ServiceMethodArg(node);
				
				if(serviceMethodArg.DataType.IsComplexType())
					return new ArrayCollection(serviceMethodArg.DataType.Items);
				else
					return new ArrayCollection(new Array(serviceMethodArg.DataType));
			}
			else if(ServiceNode(node).IsDataTypeField())
			{
				var dataTypeField:ServiceDataTypeField = ServiceDataTypeField(node);
				
				if(dataTypeField.DataType.IsComplexType())
				{
					var a:ArrayCollection = new ArrayCollection();
					
					for each(var o:Object in dataTypeField.DataType.Items)
					{
						a.addItem(ObjectUtil.copy(o));
					}
					
					return a;
				}
				else
					return new ArrayCollection(new Array(ServiceDataTypeField(node).DataType));		
			}*/
			
			
			/*if(ServiceNode(node).IsDataTypeContainer())
			{
				var dataType:ServiceDataType = ServiceDataTypeContainer(node).DataType;
				
				if(dataType.IsComplexType())
				{
					var arrayCollection:ArrayCollection = new ArrayCollection();
					
					for each(var o:Object in dataType.Items)
						arrayCollection.addItem(ObjectUtil.copy(o));

					return arrayCollection;
				}
				else if(dataType.IsArray())
				{
					return new ArrayCollection();
				}
				
				return new ArrayCollection(new Array(dataType));
			}*/
			
			
			return new ArrayCollection(ServiceNode(node).Items);
		}
	}
}