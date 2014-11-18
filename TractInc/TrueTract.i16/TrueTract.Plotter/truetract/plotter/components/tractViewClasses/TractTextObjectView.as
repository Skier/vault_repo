package truetract.plotter.components.tractViewClasses
{
    import flash.events.Event;
    
    import mx.controls.Text;
    import mx.core.UIComponent;
    
    import truetract.plotter.components.AutoResizingTextArea;
    import truetract.plotter.domain.TractTextObject;
    
    public class TractTextObjectView extends UIComponent
    {
        private var model:TractTextObject;
        
        private var textView:Text;
        private var textEditor:AutoResizingTextArea;

        private var viewRotation:Number = 0;
        
        private var modelChanged:Boolean = false;

        public function get Model():TractTextObject
        {
            return model;
        }
        
        public function set Model(value:TractTextObject):void 
        {
            model = value;
            
            modelChanged = true;
            invalidateProperties();
        }
        
        public function get text():String 
        {
            return textView.text;
        }
        
        public function set text(value:String):void 
        {
            textEditor.text = value;
            textView.text = value;
            
            synchronizeViewSize();
        }
        
        override protected function createChildren():void 
        {
            super.createChildren();
            
            if (!textView && !textEditor) 
            {
                textView = new Text();
                textView.selectable = false;
                addChild(textView);

                textEditor = new AutoResizingTextArea();
                textEditor.addEventListener(Event.CHANGE, textEditor_changeHandler);
                addChild(textEditor);

                SwitchToView();
            }
        }
        
        override protected function commitProperties():void 
        {
            super.commitProperties();
            
            if (modelChanged) 
            {
                modelChanged = false;
                
                text = (model) ? model.Text : "";
            }
        }
        
        override public function validateSize(recursive:Boolean=false):void 
        {
            super.validateSize(recursive);
            synchronizeViewSize();
        }
        
        public function SwitchToEditor(setFocus:Boolean = false):void 
        {
            textEditor.visible = true;
            textView.visible = false;
            viewRotation = rotation;

            if (setFocus) {
                rotation = 0;
                textEditor.setFocus();
            }
        }
        
        public function SwitchToView():void 
        {
            rotation = viewRotation;
            textEditor.visible = false;
            textView.visible = true;
            synchronizeViewSize();
        }
        
        private function synchronizeViewSize():void 
        {
            textView.width = textEditor.width;
            textView.height = textEditor.height;
            
            this.width = textEditor.width;
            this.height = textEditor.height;
        }
        
        protected function textEditor_changeHandler(event:Event):void 
        {
            model.Text = textView.text = textEditor.text;
        }
    }
}