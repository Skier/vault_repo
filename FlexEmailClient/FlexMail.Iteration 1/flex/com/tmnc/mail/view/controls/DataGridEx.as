package com.tmnc.mail.view.controls
{
    import mx.controls.DataGrid;
    import mx.controls.listClasses.IListItemRenderer;
    import flash.geom.Point;
    import mx.collections.ArrayCollection;
    import mx.controls.dataGridClasses.DataGridListData;
    import mx.controls.dataGridClasses.DataGridColumn;
    import flash.display.DisplayObject;
    import flash.events.MouseEvent;
    import flash.display.Sprite;
    import mx.core.IFlexDisplayObject;
    import mx.core.FlexSprite;
    import flash.display.Graphics;
    import mx.events.DataGridEvent;
    
    public class DataGridEx extends DataGrid 
{

    private var sortArrow:IFlexDisplayObject;    
    private var lastSortIndex:int;
    
    override protected function makeRowsAndColumns(left:Number, top:Number,
            right:Number, bottom:Number, firstCol:int, firstRow:int,
            byCount:Boolean = false, rowsNeeded:uint = 0):Point
    {

        var pt:Point = super.makeRowsAndColumns(
            left, top, right, bottom, firstCol, firstRow, byCount, rowsNeeded);

        for (var i:int=1; i < listItems.length;i++){
            if (listItems[i][0]){
                
                var item:IListItemRenderer = listItems[i][0];
                var itemUid:String = itemToUID(item.data);
                
                if (visibleData[itemUid]){
                    item.width = this.width;
                    drawItem(item, selectedItem == item.data);
                }
            }
        }

        return pt;
    }

    override protected function updateDisplayList(unscaledWidth:Number,
                                                  unscaledHeight:Number):void {
        super.updateDisplayList(unscaledWidth, unscaledHeight);
        
        if (lastSortIndex){
            placeSortArrowManually(lastSortIndex);
        }
    }
    
    public function placeSortArrowManually(sortIndex:int):void {

        var sortArrowHitArea:Sprite =
            Sprite(listContent.getChildByName("sortArrowHitArea"));

        if (!showHeaders) {
            if (sortArrow)
                sortArrow.visible = false;
            if (sortArrowHitArea)
                sortArrowHitArea.visible = false;
            return;
        }

        if (!sortArrow) {
            var sortArrowClass:Class = getStyle("sortArrowSkin");
            sortArrow = new sortArrowClass();
            DisplayObject(sortArrow).name = "sortArrow";
            listContent.addChild(DisplayObject(sortArrow));
        }
        
        var xx:Number;
        var n:Number;
        var i:Number = sortIndex
        if (listItems && listItems.length && listItems[0]) {

            if (columns[i]) {
                xx = listItems[0][i].x + columns[i].width;
                listItems[0][i].setActualSize(columns[i].width - sortArrow.measuredWidth - 8, listItems[0][i].height);

                if (!isNaN(listItems[0][i].explicitWidth))
                    listItems[0][i].explicitWidth = listItems[0][i].width;

                // Create hit area to capture mouse clicks behind arrow.
                if (!sortArrowHitArea)
                {
                    sortArrowHitArea = new FlexSprite();
                    sortArrowHitArea.name = "sortArrowHitArea";
                    listContent.addChild(sortArrowHitArea);
                }
                else
                    sortArrowHitArea.visible = true;

                sortArrowHitArea.x = listItems[0][i].x + listItems[0][i].width;
                sortArrowHitArea.y = listItems[0][i].y;

                var g:Graphics = sortArrowHitArea.graphics;
                g.clear();
                g.beginFill(0, 0);
                g.drawRect(0, 0, sortArrow.measuredWidth + 8,
                        listItems[0][i].height);
                g.endFill();

            }
        }
        
        if (isNaN(xx)) {
            sortArrow.visible = false;
            return;
        }
        
        sortArrow.visible = true;
        if (lastSortIndex >= 0 && lastSortIndex != sortIndex)
            if (1 <= lastSortIndex && lastSortIndex <= columns.length - 1) {

                if (columns[lastSortIndex]) {
                    listItems[0][lastSortIndex].setActualSize(columns[lastSortIndex].width, listItems[0][lastSortIndex].height);
                }
            }

        var d:Boolean = !columns[sortIndex].sortDescending;
        sortArrow.width = sortArrow.measuredWidth;
        sortArrow.height = sortArrow.measuredHeight;
        DisplayObject(sortArrow).scaleY = (d) ? -1.0 : 1.0;
        sortArrow.x = xx - sortArrow.measuredWidth - 8;
        var hh:Number = rowInfo.length ? rowInfo[0].height : headerHeight
        sortArrow.y = (hh - sortArrow.measuredHeight) / 2 + ((d) ? sortArrow.measuredHeight: 0);

        if (sortArrow.x < listItems[0][i].x)
            sortArrow.visible = false;

        if (!sortArrow.visible && sortArrowHitArea)
            sortArrowHitArea.visible = false;
            
        lastSortIndex = sortIndex;
    }    

    override protected function mouseDownHandler(event:MouseEvent):void {
        
        var s:Sprite = Sprite(listContent.getChildByName("sortArrowHitArea"));

        if (event.target == s) {
            s = Sprite(selectionLayer.getChildByName("headerSelection"));
            
            if (!s) {
                s = new FlexSprite();
                s.name = "headerSelection";
                selectionLayer.addChild(s);
            }

            var g:Graphics = s.graphics;
            g.clear();
            g.beginFill(getStyle("selectionColor"));
            g.drawRect(0, 0, columns[lastSortIndex].width, rowInfo[0].height - 0.5);
            g.endFill();

            s.x = listItems[0][lastSortIndex].x;
            s.y = rowInfo[0].y;
        }

        super.mouseDownHandler(event);
    }
    
    override protected function mouseUpHandler(event:MouseEvent):void {
        var dataGridEvent:DataGridEvent;
        var s:Sprite;

        // find out if we hit the sort arrow
        s = Sprite(listContent.getChildByName("sortArrowHitArea"));

        if (event.target == s) {
            dataGridEvent =    new DataGridEvent(DataGridEvent.HEADER_RELEASE, false, true);
            dataGridEvent.columnIndex = lastSortIndex;
            dataGridEvent.dataField = columns[lastSortIndex].dataField;
            dataGridEvent.itemRenderer = listItems[0][lastSortIndex];
            dispatchEvent(dataGridEvent);
        } else {
            super.mouseUpHandler(event);            
        }
    }
    
}
}