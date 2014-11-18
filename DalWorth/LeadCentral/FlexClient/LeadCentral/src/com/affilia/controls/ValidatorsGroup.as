package com.affilia.controls
{
    import mx.collections.ArrayCollection;
    import mx.effects.Glow;
    import mx.events.ValidationResultEvent;
    import mx.validators.Validator;
    import mx.styles.StyleManager;
    import flash.display.DisplayObject;
    
    [DefaultProperty("validators")]

    public class ValidatorsGroup
    {
        public function ValidatorsGroup()
        {
            glowEffect = new Glow();
            glowEffect.color = StyleManager.getColorName("red");
        }

        public var validators:Array;

        private var glowEffect:Glow;

        public function validate(useAnimation:Boolean = false):Boolean
        {
            if (!validators || validators.length == 0)
                return true;

            var invalidItemsCount:Number = 0;

            var animatedItems:Array = [];

            for each (var item:* in validators)
            {
                var validator:Validator = item as Validator;
                
                if (validator && validator.enabled && 
                    validator.validate().type == ValidationResultEvent.INVALID)
                {
                    invalidItemsCount++;
                    
                    if (validator.source is DisplayObject)
                        animatedItems.push(validator.source);
                }
            }

            var result:Boolean = (invalidItemsCount == 0);

            if (!result && useAnimation && animatedItems.length > 0)
            {
                glowEffect.end();
                glowEffect.play(animatedItems);
            }

            return result;
        }

    }
}