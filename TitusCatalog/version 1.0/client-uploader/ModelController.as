package
{

    [Bindable]
    public class ModelController
    {
    	
    	private var view:ModelView;
    	
    	public var modelInfo:ModelDataObject;
    	
    	public var parentController:PDFUploaderController;
        
        public function ModelController(parentController:PDFUploaderController, view:ModelView, modelInfo:ModelDataObject)
        {
        	this.parentController = parentController;
            this.view = view;
            this.modelInfo = modelInfo;
        }
        
    }

}
