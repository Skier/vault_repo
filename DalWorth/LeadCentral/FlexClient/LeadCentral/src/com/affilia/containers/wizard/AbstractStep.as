package com.affilia.containers.wizard
{
    import mx.containers.Box;
    
    import truetract.plotter.validators.ValidatorsGroup;
    
    public class AbstractStep extends Box
    {
        [Bindable] public var stepTitle:String = "";
        [Bindable] public var formValid:Boolean = true;
        [Bindable] public var formValidator:ValidatorsGroup;
        [Bindable] public var hasNextStep:Boolean;

        public function activate():void
        {
        }

        public function validateForm():Boolean
        {
            var result:Boolean = true;
            
            if (null != formValidator)
                result = formValidator.validate(true);

            return result;
        }

        public function clean():void
        {
        }

    }
}