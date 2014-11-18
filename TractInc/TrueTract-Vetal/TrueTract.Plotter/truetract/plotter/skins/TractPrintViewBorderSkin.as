package truetract.plotter.skins
{
    import mx.skins.RectangularBorder;
    import flash.display.Graphics;
    import mx.core.Container;
    import mx.core.EdgeMetrics;
    import mx.graphics.Stroke;
    import truetract.plotter.utils.GraphicsUtil;
    import flash.display.CapsStyle;
    import flash.display.JointStyle;
    
public class TractPrintViewBorderSkin extends RectangularBorder
{
        
    // Constructor.
     public function TractPrintViewBorderSkin()
     {
     }

     override protected function updateDisplayList(w:Number, h:Number):void
     {
        super.updateDisplayList(w, h);

        if (!parent)
            return;

        var borderColor:Number = getStyle("borderColor");          
		var backgroundColor:Number = getStyle("backgroundColor");
        var borderWidth:Number = 4;

		if (!backgroundColor)
		    backgroundColor = 0xFFFFFF;


        var g:Graphics = graphics;

        g.clear();

		// Fill in the content area
		g.beginFill(backgroundColor, 1);
		g.drawRect(0, 0, w, h);
		g.endFill();

        g.lineStyle(1, borderColor, 1, false, "normal", CapsStyle.NONE, JointStyle.MITER);
        g.drawRoundRect(0, 0, w, h, 2);
        g.drawRect(borderWidth, borderWidth, w - borderWidth * 2, h - borderWidth * 2);

        var indent:Number = 2;

        var pattern:Array = [9];

        var stroke:Stroke = new Stroke(borderColor, borderWidth, 1, false, "normal", 
            CapsStyle.NONE);

        //right line
        GraphicsUtil.drawDashedLine(
            g, stroke, pattern, 
            indent, 0, indent, h);

        // left line
        GraphicsUtil.drawDashedLine(
            g, stroke, pattern, 
            w - indent, 0, w - indent, h);

        //top line
        GraphicsUtil.drawDashedLine(
            g, stroke, pattern, 
            indent, indent, w - indent, indent);
        
        //bottom
        GraphicsUtil.drawDashedLine(
            g, stroke, pattern, 
            indent, h - indent, w, h - indent);
     }

}
}