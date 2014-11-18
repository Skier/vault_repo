package src.deedplotter.domain.settings
{
    
    import flash.events.Event;
    import flash.net.SharedObject;
    import flash.net.registerClassAlias;
    import flash.text.FontStyle;
    
    import mx.core.Application;
    import mx.events.PropertyChangeEvent;
    import mx.styles.CSSStyleDeclaration;
    import mx.styles.StyleManager;
    
    public class UserSettingsRegistry
    {
        public static const SETTINGS_CHANGED:String = "settings_changed";
        
        private var settingsSO:SharedObject;
        
        private static var instance : UserSettingsRegistry
        
        public static function getInstance() : UserSettingsRegistry {
            if ( instance == null )
                instance = new UserSettingsRegistry( arguments.callee );
                
            return instance;
        }
        
        public function UserSettingsRegistry( caller : Function = null ) 
        {
            if(caller != UserSettingsRegistry.getInstance)
            {
                throw new Error ("UserSettings is a singleton class, use getInstance() instead");
            }
            
            if (UserSettingsRegistry.instance != null)
            {
                throw new Error( "Only one UserSettings instance should be instantiated" ); 
            }
            
            registerClassAlias("src.deedplotter.domain.settings.FontCssStyle", FontCssStyle);
            registerClassAlias("src.deedplotter.domain.settings.TractPointCssStyle", TractPointCssStyle);
            
            settingsSO = SharedObject.getLocal(DEEDPRO_SETTINGS_SO_NAME);
            
            if (settingsSO.data.showArea == null) 
            {
                settingsSO.data.showArea = true;
            }

            if (settingsSO.data.showAnnotations == null)
            {
                settingsSO.data.showAnnotations = true;
            }

            refreshFontStyles();
            refreshColorStyles();
        }

        private const DEEDPRO_SETTINGS_SO_NAME:String = "deedpro_settings";

        public function set ShowArea(value:Boolean):void
        {
            settingsSO.data.showArea = value;
            commit();
        }
        
        public function get ShowArea():Boolean
        {
            return settingsSO.data.showArea;
        }

        public function set ShowAnnotations(value:Boolean):void
        {
            settingsSO.data.showAnnotations = value;
            commit();
        }
        
        public function get ShowAnnotations():Boolean
        {
            return settingsSO.data.showAnnotations;
        }

        public function set PopUpAlpha(value:Number):void
        {
            settingsSO.data.popUpAlpha = value;
            commit();
        }
        
        public function get PopUpAlpha():Number
        {
            return settingsSO.data.popUpAlpha;
        }

        public function get AnnotationFontStyle():FontCssStyle
        {
            return settingsSO.data.annotationStyle;
        }

        public function set AnnotationFontStyle(value:FontCssStyle):void
        {
            settingsSO.data.annotationStyle = value;
            commit();
        }

        public function get TractAreaFontStyle():FontCssStyle 
        {
            return settingsSO.data.tractAreaStyle;
        }

        public function set TractAreaFontStyle(value:FontCssStyle):void 
        {
            settingsSO.data.tractAreaStyle = value;
            commit();
        }

        public function get TextObjectFontStyle():FontCssStyle 
        {
            return settingsSO.data.textObjectStyle;
        }

        public function set TextObjectFontStyle(value:FontCssStyle):void 
        {
            settingsSO.data.textObjectStyle = value;
            commit();
        }

        public function get ControlPointColorStyle():TractPointCssStyle 
        {
            return settingsSO.data.controlPointColorStyle;
        }

        public function set ControlPointColorStyle(value:TractPointCssStyle):void 
        {
            settingsSO.data.controlPointColorStyle = value;
            commit();
        }

        public function get StartPointColorStyle():TractPointCssStyle 
        {
            return settingsSO.data.startPointColorStyle;
        }

        public function set StartPointColorStyle(value:TractPointCssStyle):void 
        {
            settingsSO.data.startPointColorStyle = value;
            commit();
        }

        public function get EndPointColorStyle():TractPointCssStyle 
        {
            return settingsSO.data.endPointColorStyle;
        }

        public function set EndPointColorStyle(value:TractPointCssStyle):void 
        {
            settingsSO.data.endPointColorStyle = value;
            commit();
        }

        public function get CallViewColorStyle():Object 
        {
            return settingsSO.data.callViewColorStyle;
        }

        public function set CallViewColorStyle(value:Object):void 
        {
            settingsSO.data.callViewColorStyle = value;
            commit();
        }

        public function commit():void
        {
            Application.application.dispatchEvent(new Event(SETTINGS_CHANGED));
            settingsSO.flush();
        }

        public function refreshFontStyles():void 
        {
            refreshFontStyle("annotationStyle", ".tractCallAnnotation");
            refreshFontStyle("tractAreaStyle", ".tractTextArea");
            refreshFontStyle("textObjectStyle", ".tractTextObject");
        }

        public function refreshColorStyles():void 
        {
            refreshColorStyle("controlPointColorStyle", ".tractControlPoint");
            refreshColorStyle("startPointColorStyle", ".tractStartPoint");
            refreshColorStyle("endPointColorStyle", ".tractEndPoint");

            var callViewCssStyle:CSSStyleDeclaration = StyleManager.getStyleDeclaration('.tractCall');
            
            if (callViewCssStyle) 
            {
                if (settingsSO.data.callViewColorStyle == null) {
                    settingsSO.data.callViewColorStyle = new Object();
                    settingsSO.data.callViewColorStyle.lineColor = callViewCssStyle.getStyle('lineColor');
                    settingsSO.data.callViewColorStyle.lineRollOverColor = callViewCssStyle.getStyle('lineRollOverColor');
                } else {
                    callViewCssStyle.setStyle('lineColor', settingsSO.data.callViewColorStyle.lineColor);
                    callViewCssStyle.setStyle('lineRollOverColor', settingsSO.data.callViewColorStyle.lineRollOverColor);
                }
            }
        }

        private function refreshFontStyle(propName:String, selector:String):void 
        {
            if (settingsSO.data[propName] == null) {
                settingsSO.data[propName] = FontCssStyle.createFromCss(selector);
            } else {
                FontCssStyle(settingsSO.data[propName]).applyToCssStyle(selector);
            }
        }

        private function refreshColorStyle(propName:String, selector:String):void 
        {
            if (settingsSO.data[propName] == null) {
                settingsSO.data[propName] = TractPointCssStyle.createFromCss(selector);
            } else {
                TractPointCssStyle(settingsSO.data[propName]).applyToCssStyle(selector);
            }
        }
        
    }
}