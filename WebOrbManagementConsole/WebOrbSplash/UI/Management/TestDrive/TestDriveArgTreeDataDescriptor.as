package UI.Management.TestDrive
{
	import mx.controls.treeClasses.ITreeDataDescriptor;
	import UI.Management.ServiceBrowser.*;
	import mx.collections.ICollectionView;
	import mx.collections.ArrayCollection;
	import mx.utils.ObjectUtil;

	public class TestDriveArgTreeDataDescriptor implements ITreeDataDescriptor
	{
		public function getData(node:Object, model:Object=null):Object
		{
			return node;
		}
		
		public function hasChildren(node:Object, model:Object=null):Boolean
		{
			var serviceNode:ServiceNode = ServiceNode(node);
			
			if(serviceNode.Items.length > 0 )
				return true;
				
			if(serviceNode.IsDataTypeContainer())
			{
				var serviceDataTypeContainer:ServiceDataTypeContainer = ServiceDataTypeContainer(node);
				
				return serviceDataTypeContainer.DataType.IsArray() || serviceDataTypeContainer.DataType.IsComplexType();
			}
				
			return false;
		}
		
		public function addChildAt(parent:Object, newChild:Object, index:int, model:Object=null):Boolean
		{
			return false;
		}
		
		public function isBranch(node:Object, model:Object=null):Boolean
		{
			return hasChildren(node);
		}
		
		public function removeChildAt(parent:Object, child:Object, index:int, model:Object=null):Boolean
		{
			return false;
		}
		
		public function getChildren(node:Object, model:Object=null):ICollectionView
		{
			if(ServiceNode(node).IsDataTypeContainer())
			{
				var dataType:ServiceDataType = ServiceDataTypeContainer(node).DataType;
				
				if(dataType.IsComplexType() || dataType.IsArray())
				{
					var arrayCollection:ArrayCollection = new ArrayCollection();
					
					for each(var o:Object in dataType.Items)
						arrayCollection.addItem(ObjectUtil.copy(o));

					return arrayCollection;
				}
				
				return new ArrayCollection(new Array(dataType));
			}
			
			
			return new ArrayCollection(ServiceNode(node).Items);
		}
		
	}
}