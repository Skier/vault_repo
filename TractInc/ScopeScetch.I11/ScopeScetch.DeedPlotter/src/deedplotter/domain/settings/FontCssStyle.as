package src.deedplotter.domain.settings
{
    import mx.styles.CSSStyleDeclaration;
    import mx.styles.StyleManager;
    
    public class FontCssStyle
    {
        public var color:Number;
        public var fontFamily:String;
        public var fontSize:Number;
        public var bold:Boolean;
        public var italic:Boolean;
        public var underline:Boolean;

        public static function createFromCss(selector:String):Object 
        {
            var result:FontCssStyle = new FontCssStyle();

            var cssStyle:CSSStyleDeclaration = StyleManager.getStyleDeclaration(selector);

            if (cssStyle) 
            {
                result.color = cssStyle.getStyle('color');
                result.fontFamily = cssStyle.getStyle('fontFamily');
                result.fontSize = cssStyle.getStyle('fontSize');
                result.bold = cssStyle.getStyle('fontWeight') == "bold";
                result.italic = cssStyle.getStyle('fontStyle') == "italic";
                result.underline = cssStyle.getStyle('textDecoration') == "underline";
            }

            return result;
        }

        public function applyToCssStyle(selector:String):void 
        {
            var cssStyle:CSSStyleDeclaration = StyleManager.getStyleDeclaration(selector);
            
            if (cssStyle) {
                cssStyle.setStyle('color', color);
                cssStyle.setStyle('fontFamily', fontFamily);
                cssStyle.setStyle('fontSize', fontSize);
                cssStyle.setStyle('fontWeight', bold ? "bold" : "normal");
                cssStyle.setStyle('fontStyle', italic ? "italic" : "none");
                cssStyle.setStyle('textDecoration', underline ? "underline" : "none");
            }
        }
        
    }
}