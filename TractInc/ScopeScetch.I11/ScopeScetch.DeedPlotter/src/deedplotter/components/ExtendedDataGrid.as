package src.deedplotter.components
{
    import mx.controls.DataGrid;
    import mx.controls.dataGridClasses.DataGridColumn;
    import mx.controls.listClasses.IListItemRenderer;
    import mx.controls.scrollClasses.ScrollBar;
    import mx.core.mx_internal;

    use namespace mx_internal;

    /**
    * Extendendet DataGrid provides access to the vertical Scroll Bar and
    * provides a method for calculation optimal column width
    **/ 
    public class ExtendedDataGrid extends DataGrid
    {

        public function getVerticalScrollBar():ScrollBar {
            return verticalScrollBar;
        }
        
        /**
        * Measures a set of column items from the data provider using
        * the current item renderer and returns the maximum width found
        **/
        public function getOptimalColumnWidth(c:DataGridColumn):Number 
        {
            var colNum:Number = columns.indexOf(c);

            if (colNum == -1)
                throw new Error("Column not found");

            var item:IListItemRenderer;
            var w:Number = 0;

            if (headerVisible && listItems && listItems.length > 0)
                w = listItems[0][colNum].measuredWidth; // header width

            for (var i:int = 0; i < collection.length; i++)
            {
               var data:Object = collection[i];

               item = getMeasuringRenderer(c, false);
               setupRendererFromData(c, item, data);
               
               w = Math.max(w, item.measuredWidth);
            }
            
            return w;
        }

    }
}