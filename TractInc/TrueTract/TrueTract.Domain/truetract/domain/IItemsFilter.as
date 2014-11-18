package truetract.domain
{
    import mx.collections.ArrayCollection;
    
public interface IItemsFilter
{
    function applyFilter(items:ArrayCollection):void;

    function isSpecified():Boolean;

    function reset():void;
    
    function getFilterParams():Object;
}
}