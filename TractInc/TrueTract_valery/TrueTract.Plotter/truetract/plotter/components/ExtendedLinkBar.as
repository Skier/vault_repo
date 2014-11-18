package truetract.plotter.components
{
import mx.controls.Button;
import mx.controls.LinkBar;
import mx.core.EdgeMetrics;
import mx.core.IFlexDisplayObject;

[Style(name="buttonTextAlign", type="String", enumeration="left,center,right", inherit="yes")]

public class ExtendedLinkBar extends LinkBar
{
    override protected function createNavItem(label:String, icon:Class = null):IFlexDisplayObject
    {
        var newLink:Button = Button(super.createNavItem(label, icon));
        
        var vm:EdgeMetrics = viewMetricsAndPadding;

        newLink.width = this.width - vm.left - vm.right;
        newLink.setStyle("textAlign", getStyle("buttonTextAlign") );
        
        return newLink;
    }
}
}