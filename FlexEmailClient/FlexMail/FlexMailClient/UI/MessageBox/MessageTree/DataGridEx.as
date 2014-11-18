package UI.MessageBox.MessageTree
{
    import flash.geom.Point;
    
    import mx.controls.DataGrid;
    import mx.controls.listClasses.IListItemRenderer;
    import flash.events.KeyboardEvent;
    import flash.ui.Keyboard;
    import flash.events.Event;
    import flash.events.MouseEvent;
    
/**
 * The main goal of this grid extending is to allow the content of the first column to overlap content of others column.
 * So, the item renderer width of first column must always be equals DataGrid.width
**/
    public class DataGridEx extends DataGrid 
{

    override protected function makeRowsAndColumns(left:Number, top:Number,
            right:Number, bottom:Number, firstCol:int, firstRow:int,
            byCount:Boolean = false, rowsNeeded:uint = 0):Point
    {

        var pt:Point = super.makeRowsAndColumns(
            left, top, right, bottom, firstCol, firstRow, byCount, rowsNeeded);

        for ( var i:int = 1; i < listItems.length; i++ )
        {
            if ( listItems[i][0] )
            {
                var item:IListItemRenderer = listItems[i][0];
                var itemUid:String = itemToUID(item.data);
                
                if ( visibleData[itemUid] )
                {
                    item.width = this.width;
                    drawItem(item, selectedItem == item.data);
                }
            }
        }

        return pt;
    }

    //After minimizing or switching brousers between other windows,
    //DataGrid losts focus and keyboard navigation doesn't work.
    //Need to reset focus
    override protected function mouseDownHandler(event:MouseEvent):void
    {
        this.setFocus();
        super.mouseDownHandler(event);
    }
    
    //Prevent keyboard navigation when datagrid is disabled
	override protected function keyDownHandler(event:KeyboardEvent):void
	{
        if (!this.enabled)
            return;

        super.keyDownHandler(event);
	}

}
}