package src.deedplotter.domain.settings
{
    import mx.styles.CSSStyleDeclaration;
    import mx.styles.StyleManager;
    
    public class TractPointCssStyle
    {
        public var borderColor:Number;
        public var borderRollOverColor:Number;
        public var backgroundColor:Number;
        public var backgroundRollOverColor:Number;
        
        public static function createFromCss(selector:String):TractPointCssStyle {
            var colorStyle:TractPointCssStyle = new TractPointCssStyle();

            var cssStyle:CSSStyleDeclaration = StyleManager.getStyleDeclaration(selector);

            if (cssStyle)
            {
                colorStyle.borderColor = Number(cssStyle.getStyle('borderColor')),
                colorStyle.backgroundColor = Number(cssStyle.getStyle('backgroundColor')),
                colorStyle.borderRollOverColor = Number(cssStyle.getStyle('borderRollOverColor')),
                colorStyle.backgroundRollOverColor = Number(cssStyle.getStyle('backgroundRollOverColor'))
            }

            return colorStyle;
        }
        
        public function applyToCssStyle(selector:String):void {
            var cssStyle:CSSStyleDeclaration = StyleManager.getStyleDeclaration(selector);
            
            if (cssStyle) 
            {
                cssStyle.setStyle('borderColor', borderColor);
                cssStyle.setStyle('backgroundColor', backgroundColor);
                cssStyle.setStyle('borderRollOverColor', borderRollOverColor);
                cssStyle.setStyle('backgroundRollOverColor', backgroundRollOverColor);
            }
        }
        
    }
}