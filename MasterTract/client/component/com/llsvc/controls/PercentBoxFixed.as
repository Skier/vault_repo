package com.llsvc.controls
{
    import mx.containers.Box;
    import mx.core.mx_internal;
 
    use namespace mx_internal;
 
    public class PercentBoxFixed extends Box
    {
        public function PercentBoxFixed()
        {
            super();
            layoutObject = new PercentBoxFixedLayout();
            layoutObject.target = this;
        }
    }
}