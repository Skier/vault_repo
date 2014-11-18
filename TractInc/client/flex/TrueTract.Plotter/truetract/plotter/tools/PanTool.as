package truetract.plotter.tools
{
    import truetract.plotter.Plotter;
    
    public class PanTool extends AbstractTool
    {
        [Bindable]
        [Embed(source="/assets/pan_tool_icon3.png")]
        private var m_toolIcon:Class;
        
        public function PanTool()
        {
            super();
            
            Icon = m_toolIcon;
            Description = "Pan The Canvas";
        }

        override public function Activate():void 
        {
            plotter.drawingSurface.ctrlKeyRequired = false;
        }
        
        override public function Deactivate():void 
        {
            plotter.drawingSurface.ctrlKeyRequired = true;
            plotter.bearingTxt.setFocus();
        }
    }
}