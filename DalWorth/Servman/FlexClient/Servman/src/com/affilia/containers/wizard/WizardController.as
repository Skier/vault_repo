package com.affilia.containers.wizard
{
    import mx.effects.Move;
    import mx.effects.Parallel;
    import mx.events.EffectEvent;
    import mx.containers.ViewStack;
    
    public class WizardController
    {
        [Bindable] public var activeStep:AbstractStep;

        public var movementHistoryStack:Array = [];

        public var useAnimation:Boolean = true;

        public var stepLeft:Number = 0;
        public var stepTop:Number = 0;
        public var stepRight:Number = 0;
        public var stepWidth:Number = 0;
        public var stepHeight:Number = 0;

        protected function initAnimationRectangle(vs:ViewStack):void
        {
            var borderWidth:Number = vs.getStyle("borderWidth");
            if (isNaN(borderWidth)) borderWidth = 0;

            var paddingLeft:Number = vs.getStyle("paddingLeft");
            if (isNaN(paddingLeft)) paddingLeft = 0;            
            
            var paddingRight:Number = vs.getStyle("paddingRight");
            if (isNaN(paddingRight)) paddingRight = 0;            
            
            var paddingTop:Number = vs.getStyle("paddingTop");
            if (isNaN(paddingTop)) paddingTop = 0;            
            
            var paddingBottom:Number = vs.getStyle("paddingBottom");
            if (isNaN(paddingBottom)) paddingBottom = 0;

            stepLeft = paddingLeft + borderWidth;
            stepTop = paddingTop + borderWidth;
            stepRight = vs.width - paddingRight - (borderWidth * 2);
            stepWidth = vs.width - paddingRight - paddingLeft - (borderWidth * 2);
            stepHeight = vs.height - paddingTop - paddingBottom - (borderWidth * 2);
        }

        public function setStep(step:AbstractStep, movingForward:Boolean = true):void
        {
            if (movingForward)
                movementHistoryStack.push(activeStep);

            doTransition(activeStep, step, movingForward);
        }

        private function doTransition(sourceStep:AbstractStep, destStep:AbstractStep, movingForward:Boolean):void
        {
            if (!sourceStep || !useAnimation)
            {
                activeStep = destStep;
                activeStep.activate();
                return;
            }

            var vs_width:Number = stepRight + stepLeft;

            var moveSource:Move = new Move(sourceStep);
            moveSource.xFrom = stepLeft;
            moveSource.xTo = movingForward ? -vs_width : vs_width;

            var moveDest:Move = new Move(destStep);
            moveDest.xFrom = movingForward ? vs_width : -vs_width;
            moveDest.xTo = stepLeft;

            if (destStep.width == 0 && destStep.height == 0)
            {
                destStep.setActualSize(stepWidth, stepHeight);
                destStep.validateNow();
            }

            destStep.y = stepTop;
            destStep.visible = true;

            var paralel:Parallel = new Parallel();
            paralel.children.push(moveSource, moveDest);
            paralel.addEventListener(EffectEvent.EFFECT_END, 
                function(event:*):void 
                { 
                    activeStep = destStep;
                    activeStep.activate();
                });

            paralel.play();
        }
        
        public function goToNextStep():Boolean
        {
            return activeStep.validateForm();
        }

        public function goToPreviousStep():Boolean
        {
            if (movementHistoryStack.length > 0) 
            {
                var previousStep:AbstractStep = AbstractStep(movementHistoryStack.pop());

                activeStep.clean();

                setStep(previousStep, false);
                return true;
            }
            
            return false;
        }
    }
}
