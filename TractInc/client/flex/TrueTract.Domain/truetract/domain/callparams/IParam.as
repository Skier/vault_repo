package truetract.domain.callparams
{
    public interface IParam
    {
        
        function get Value():*;

        function get DBName():String;
        function get DBValue():String;
        
        function get DisplayName():String;
        function get DisplayValue():String;
        
    }
}