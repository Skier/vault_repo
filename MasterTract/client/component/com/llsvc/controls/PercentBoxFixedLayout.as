package com.llsvc.controls
{
    import mx.containers.utilityClasses.BoxLayout;
    import mx.core.Container;
 
    public class PercentBoxFixedLayout extends BoxLayout
    {
        override public function measure():void
        {
            var target:Container = this.target as Container;
 
            super.measure();
 
            if (!isNaN(target.percentWidth))
            {
                target.measuredMinWidth = 0;
                target.measuredWidth = 0;
            }
            if (!isNaN(target.percentHeight))
            {
                target.measuredMinHeight = 0;
                target.measuredHeight = 0;
            }
        }
    }
}